using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    static class Network
    {
        public static string GetUrlRead( string Url )
        {
            using( var Web = new WebClient() )
            {
                return new StreamReader( Web.OpenRead( new Uri( Url ) ) ).ReadToEnd();
            }
        }
    }

    public class DeserializePHP
    {
        public DeserializePHP( string Input )
        {
            _Data = Input;
            _Nfi.NumberGroupSeparator = "";
            _Nfi.NumberDecimalSeparator = ".";
        }

        public object Deserialize()
        {
            _Pos = 0;
            return Deserialize( _Data );
        }

        private object Deserialize( string Input )
        {
            if (Input.Equals( null ) || Input.Length <= _Pos)
                return new object();

            int Start = 0;
            int End = 0;
            int Length = 0;

            string strString;
            string strReturn;

            switch ( Input [ _Pos ] )
            {
                case 'N':
                    _Pos += 2;
                    return null;
                case 'b':
                    char Bool;
                    Bool = Input [ _Pos + 2 ];
                    _Pos += 4;
                    return Bool == '1';
                case 'i':
                    string strInt;
                    Start = Input.IndexOf( ":", _Pos ) + 1;
                    End = Input.IndexOf( ";", Start );
                    strInt = Input.Substring( Start, End - Start );
                    _Pos += 3 + strInt.Length;
                    return int.Parse( strInt, _Nfi );
                case 'd':
                    string strDouble;
                    Start = Input.IndexOf( ":", _Pos ) + 1;
                    End = Input.IndexOf( ";", Start );
                    strDouble = Input.Substring( Start, End - Start );
                    _Pos += 3 + strDouble.Length;
                    return double.Parse( strDouble, _Nfi );
                case 's':
                    Start = Input.IndexOf( ":", _Pos ) + 1;
                    End = Input.IndexOf( ":", Start );
                    strString = Input.Substring( Start, End - Start );
                    int ByteLen = int.Parse( strString );
                    Length = ByteLen;

                    if( ( End + 2 + Length ) >= Input.Length )
                    {
                        Length = Input.Length - 2 - End;
                    }

                    strReturn = Input.Substring( End + 2, Length );
                    while( _Encoding.GetByteCount( strReturn ) > ByteLen )
                    {
                        Length--;
                        strReturn = Input.Substring( End + 2, Length );
                    }

                    _Pos += 6 + strString.Length + Length;
                    return strReturn;
                case 'a':
                    Start = Input.IndexOf( ":", _Pos ) + 1;
                    End = Input.IndexOf( ":", Start );
                    strString = Input.Substring( Start, End - Start );
                    Length = int.Parse( strString );

                    var htRet = new Hashtable( Length );
                    var alRet = new ArrayList( Length );
                    _Pos += 4 + strString.Length;
                    for( int i = 0; i < Length; i++ )
                    {
                        object oKey = Deserialize( Input );
                        object oVal = Deserialize( Input );

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
                        htRet[ oKey ] = oVal;
                    }
                    _Pos++;
                    if( _Pos < Input.Length && Input[ _Pos ] == ';' )
                    {
                        _Pos++;
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

        private int _Pos = 0;
        private string _Data = "";
        private Encoding _Encoding = new UTF8Encoding();
        private readonly NumberFormatInfo _Nfi = new NumberFormatInfo();
    }
}
