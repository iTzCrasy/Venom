using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Venom.API.Utility
{
    public static class SessionExtension
    {
        public static T GetData<T>( this ISession session, string key )
        {
            var data = session.GetString( key );
            return data == null ? default : JsonConvert.DeserializeObject<T>( data );
        }
        public static void SetData( this ISession session, string key, object data )
        {
            //=> TODO: Maybe override protection
            session.SetString( key, JsonConvert.SerializeObject( data ) );
        }
    }
}
