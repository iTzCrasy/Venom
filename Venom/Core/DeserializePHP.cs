using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venom.Core
{
    public class DeserializePHP
    {
        private readonly string _data = "";
        private readonly Encoding _encoding = new UTF8Encoding( );
        private readonly NumberFormatInfo _nfi = new NumberFormatInfo( );


        private int _pos = 0;


        public DeserializePHP( string inputData )
        {
            _data = inputData;
            _nfi.NumberGroupSeparator = "";
            _nfi.NumberDecimalSeparator = ".";
        }

        public object Deserialize( )
        {
            _pos = 0;
            return Deserialize( _data );
        }

        private object Deserialize( string input )
        {
            if( input.Equals( null ) || input.Length <= _pos )
            {
                return new object( );
            }


            string strString;
            string strReturn;
            int start;
            int end;
            int length;

            switch( input[_pos] )
            {
                case 'N':
                    _pos += 2;
                    return null;
                case 'b':
                    char Bool;
                    Bool = input[_pos + 2];
                    _pos += 4;
                    return Bool == '1';
                case 'i':
                    string strInt;
                    start = input.IndexOf( ":", _pos ) + 1;
                    end = input.IndexOf( ";", start );
                    strInt = input.Substring( start, end - start );
                    _pos += 3 + strInt.Length;
                    return int.Parse( strInt, _nfi );
                case 'd':
                    string strDouble;
                    start = input.IndexOf( ":", _pos ) + 1;
                    end = input.IndexOf( ";", start );
                    strDouble = input.Substring( start, end - start );
                    _pos += 3 + strDouble.Length;
                    return double.Parse( strDouble, _nfi );
                case 's':
                    start = input.IndexOf( ":", _pos ) + 1;
                    end = input.IndexOf( ":", start );
                    strString = input.Substring( start, end - start );
                    var ByteLen = int.Parse( strString );
                    length = ByteLen;

                    if( ( end + 2 + length ) >= input.Length )
                    {
                        length = input.Length - 2 - end;
                    }

                    strReturn = input.Substring( end + 2, length );
                    while( _encoding.GetByteCount( strReturn ) > ByteLen )
                    {
                        length--;
                        strReturn = input.Substring( end + 2, length );
                    }

                    _pos += 6 + strString.Length + length;
                    return strReturn;
                case 'a':
                    start = input.IndexOf( ":", _pos ) + 1;
                    end = input.IndexOf( ":", start );
                    strString = input.Substring( start, end - start );
                    length = int.Parse( strString );

                    var htRet = new Hashtable( length );
                    var alRet = new ArrayList( length );
                    _pos += 4 + strString.Length;

                    for( var i = 0; i < length; i++ )
                    {
                        var oKey = Deserialize( input );
                        var oVal = Deserialize( input );

                        if( alRet != null )
                        {
                            if( oKey is int && ( int )oKey == alRet.Count )
                            {
                                alRet.Add( oVal );
                            }
                            else
                            {
                                alRet = null;
                            }
                        }
                        htRet[oKey] = oVal;
                    }

                    _pos++;

                    if( _pos < input.Length && input[_pos] == ';' )
                    {
                        _pos++;
                    }

                    if( alRet != null )
                    {
                        return alRet;
                    }
                    else
                    {
                        return htRet;
                    }
                default:
                    return "";
            }
        }

    }
}
