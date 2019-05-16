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

        [Obsolete]
        public DeserializePHP( string inputData )
        {
            _data = inputData;
        }

        [Obsolete]
        public object Deserialize( )
        {
            return Deserialize( _data );
        }



        public static object Deserialize( string input )
        {
            if( input == null || input.Length <= 0 )
            {
                throw new Exception( "invalid input" );
            }

            var encoding = new UTF8Encoding( );

            var nfi = new NumberFormatInfo
            {
                NumberGroupSeparator = "",
                NumberDecimalSeparator = "."
            };

            var pos = 0;
            return Deserialize( input, ref pos, nfi, encoding );
        }


        private static object Deserialize( string input, ref int pos, NumberFormatInfo nfi, UTF8Encoding encoding )
        {
            string strString;
            string strReturn;
            int start;
            int end;
            int length;

            switch( input[pos] )
            {
                case 'N':
                    pos += 2;
                    return null;
                case 'b':
                    char Bool;
                    Bool = input[pos + 2];
                    pos += 4;
                    return Bool == '1';
                case 'i':
                    string strInt;


                    start = input.IndexOf( ":", pos, StringComparison.CurrentCulture ) + 1;
                    end = input.IndexOf( ";", start, StringComparison.CurrentCulture );

                    strInt = input.Substring( start, end - start );
                    pos += 3 + strInt.Length;
                    return int.Parse( strInt, nfi );
                case 'd':
                    string strDouble;
                    start = input.IndexOf( ":", pos, StringComparison.CurrentCulture ) + 1;
                    end = input.IndexOf( ";", start, StringComparison.CurrentCulture );
                    strDouble = input.Substring( start, end - start );
                    pos += 3 + strDouble.Length;
                    return double.Parse( strDouble, nfi );
                case 's':
                    start = input.IndexOf( ":", pos, StringComparison.CurrentCulture ) + 1;
                    end = input.IndexOf( ":", start, StringComparison.CurrentCulture );
                    strString = input.Substring( start, end - start );

                    var ByteLen = int.Parse( strString, nfi );
                    length = ByteLen;

                    if( ( end + 2 + length ) >= input.Length )
                    {
                        length = input.Length - 2 - end;
                    }

                    strReturn = input.Substring( end + 2, length );
                    while( encoding.GetByteCount( strReturn ) > ByteLen )
                    {
                        length--;
                        strReturn = input.Substring( end + 2, length );
                    }

                    pos += 6 + strString.Length + length;
                    return strReturn;
                case 'a':
                    start = input.IndexOf( ":", pos, StringComparison.CurrentCulture ) + 1;
                    end = input.IndexOf( ":", start, StringComparison.CurrentCulture );
                    strString = input.Substring( start, end - start );
                    length = int.Parse( strString, nfi );

                    var htRet = new Hashtable( length );
                    var alRet = new ArrayList( length );
                    pos += 4 + strString.Length;

                    for( var i = 0; i < length; i++ )
                    {
                        var oKey = Deserialize( input, ref pos, nfi, encoding );
                        var oVal = Deserialize( input, ref pos, nfi, encoding );

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

                    pos++;

                    if( pos < input.Length && input[pos] == ';' )
                    {
                        pos++;
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
