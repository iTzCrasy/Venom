using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Venom.Data.Utility
{
    internal class CsvBuffer
    {
        private readonly byte[] _buffer;

        private readonly byte[] _lastBuffer;

        private readonly int _safeLength;

        /// <summary>
        /// Current position of the cursor. The cursor position can be negative
        /// if the data is located in _lastBuffer.
        /// Note: Buffer mapping is from (-(_lastBufferLength + 1) >=< _bufferLength)
        /// </summary>
        private int _cursorPos = 0;

        private int _endPos = 0;

        private bool _isPrepared = false;

        /// <summary>
        ///
        /// </summary>
        public bool HasMoreData => _cursorPos < _endPos;

        public int CursorPos => _cursorPos;

        /// <summary>
        ///
        /// </summary>
        /// <param name="buffer">new data buffer</param>
        /// <param name="lastBuffer">previous data buffer</param>
        /// <param name="safeLength">readable buffer length (received from ReadAsync)</param>
        public CsvBuffer( byte[] buffer, byte[] lastBuffer, int safeLength )
        {
            _buffer = buffer;
            _lastBuffer = lastBuffer;
            _safeLength = safeLength;
        }

        /// <summary>
        /// Prepares the current buffer for reading.
        /// (spit from ctor so that the worker thread can call this function)
        /// </summary>
        public void PrepareBuffer( )
        {
            _cursorPos = FindCursorBeginPos( );
            _endPos = FindCursorEndPos( );

            _isPrepared = true;
        }


        /// <summary>
        /// Finds the cursor begin position
        /// </summary>
        /// <returns>aligned cursor position (-1 <=> first element)</returns>
        private int FindCursorBeginPos( )
        {
            // first buffer
            if( _lastBuffer == null )
            {
                return 0;
            }

            for( var i = ( _lastBuffer.Length - 1 ); i >= 0; i-- )
            {
                if( _lastBuffer[i] == '\n' )
                {
                    // normalize offset
                    return -( ( _lastBuffer.Length - 1 ) - i );
                }
            }

            return -_lastBuffer.Length;
        }

        /// <summary>
        /// Finds the end of the last readable line in the current buffer.
        /// </summary>
        /// <returns></returns>
        private int FindCursorEndPos( )
        {
            for( var i = ( _safeLength - 1 ); i >= 0; i-- )
            {
                if( _buffer[i] == '\n' )
                {
                    return i;
                }
            }

            return 0;
        }


        /// <summary>
        /// Reads the token at the current cursor position and increments it afterwards
        /// </summary>
        /// <returns></returns>
        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        private byte ReadToken( )
        {
            if( !_isPrepared )
            {
                throw new Exception( "Buffer is not initialized!" );
            }

            byte token;
            if( _cursorPos < 0 )
            {
                // read local buffer space aligned
                token = _lastBuffer[_cursorPos + _lastBuffer.Length];
            }
            else
            {
                token = _buffer[_cursorPos];
            }

            // increment cursor
            _cursorPos++;

            return token;
        }

        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        private byte GetToken( )
        {
            if( !_isPrepared )
            {
                throw new Exception( "Buffer is not initialized!" );
            }

            if( _cursorPos >= _safeLength )
            {
                throw new IndexOutOfRangeException( );
            }

            if( _cursorPos < 0 )
            {
                // read local buffer space aligned
                return _lastBuffer[_cursorPos + _lastBuffer.Length];
            }

            return _buffer[_cursorPos];
        }

        private bool IsValidToken( )
        {
            if( _cursorPos < _safeLength )
            {
                if( GetToken( ) != ',' )
                {
                    if( GetToken( ) != '\n' )
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public int ReadInt( )
        {
            var number = 0;

            while( IsValidToken( ) )
            {
                number = ( number * 10 ) + ( ReadToken( ) - '0' );
            }

            // increment cursor
            _cursorPos++;

            return number;
        }

        public long ReadLong( )
        {
            long number = 0;
            while( IsValidToken( ) )
            {
                number = number * 10 + ( ReadToken( ) - '0' );
            }
            _cursorPos++;
            return number;
        }

        public string ReadString( )
        {
            // store cursor position
            var cursorPos = _cursorPos;

            // determinate string length
            var count = 0;
            while( IsValidToken( ) )
            {
                ReadToken( );
                count++;
            }

            string text;
            if( cursorPos >= 0 )
            {
                text = Encoding.ASCII.GetString( _buffer, cursorPos, count );
            }
            else
            {
                // string is split between both arrays
                if( _cursorPos >= 0 )
                {
                    text = Encoding.ASCII.GetString( _lastBuffer, cursorPos + _lastBuffer.Length, -cursorPos );

                    text += Encoding.ASCII.GetString( _buffer, 0, count - ( -cursorPos ) );
                }
                else
                {
                    text = Encoding.ASCII.GetString( _lastBuffer, cursorPos + _lastBuffer.Length, count );
                }
            }

            _cursorPos++;

            return text;
        }
    }

    internal static class CsvReader
    {
        private const int BufferSize = 1024;

        public delegate T CreateEntry< out T>( CsvBuffer buffer );


        public static async Task<IReadOnlyList<T>> DownloadFileAsync<T>( Uri uri, CreateEntry<T> creator )
        {
            var items = new ConcurrentBag<T>( );

            using( var client = new HttpClient( ) )
            {
                var stream = await client.GetStreamAsync( uri )
                    .ConfigureAwait( false );

                await ReadStream( stream, creator, items )
                    .ConfigureAwait( false );
            }

            return items.ToArray( );
        }

        public static async Task<IReadOnlyList<T>> ReadFileAsync<T>( string path, CreateEntry<T> creator )
        {
            var items = new ConcurrentBag<T>( );

            using( var sr = new StreamReader( path ) )
            {
                await ReadStream( sr.BaseStream, creator, items )
                    .ConfigureAwait( false );
            }

            return items.ToArray( );
        }


        private static async Task ReadStream<T>( Stream stream, CreateEntry<T> creator, ConcurrentBag<T> items )
        {
            var readerTasks = new List<Task>( 256 );

            byte[] lastBuffer = null;

            for(; ; )
            {
                var dataBuffer = new byte[BufferSize];

                // read data from source
                var len = await stream.ReadAsync( dataBuffer, 0, BufferSize )
                    .ConfigureAwait( false );

                // break if we got no more data
                if( len == 0 )
                {
                    break;
                }

                // create csv buffer
                var buffer = new CsvBuffer( dataBuffer, lastBuffer, len );

                // read slice
                var task = CreateBufferReadTask( buffer, creator, items );
                readerTasks.Add( task );

                // store last buffer so we can align the rows
                lastBuffer = dataBuffer;
            }

            // wait till all worker tasks are done
            await Task.WhenAll( readerTasks )
                .ConfigureAwait( false );
        }

        private static Task CreateBufferReadTask<T>( CsvBuffer buffer, CreateEntry<T> creator, ConcurrentBag<T> items )
        {
            return Task.Factory.StartNew( ( ) =>
            {
                buffer.PrepareBuffer( );

                // read until buffer is empty
                while( buffer.HasMoreData )
                {
                    items.Add( creator( buffer ) );
                }
            }, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default );
        }
    }
}
