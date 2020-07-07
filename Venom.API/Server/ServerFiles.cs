using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Venom.API.Database.Global.Entities;
using Venom.API.Database.Server.Entities;
using Venom.API.Utility;
using System.Linq;

namespace Venom.API.Server
{
    public class ServerFiles
    {
        private bool IsServerValid( string Id )
        {
            var serverFilter = new string[] { "dep", "dec", "des" };
            foreach( var f in serverFilter )
            {
                if( Id.Contains( f ) )
                {
                    return false;
                }
            }
            return true;
        }

        public async Task<List<ServerData>> FetchServerList( )
        {
            var result = new List<ServerData>( );
            char[] trimId = { 'd', 'e' };

            using( var client = new WebClient( ) )
            {
                var stream = await client.OpenReadTaskAsync( "http://www.die-staemme.de/backend/get_servers.php" )
                    .ConfigureAwait( false );

                var text = new StreamReader( stream )
                    .ReadToEnd( );

                var list = DeserializePhp.Deserialize( text );
                if( list is IEnumerable serverList )
                {
                    foreach( DictionaryEntry i in serverList )
                    {
                        //=> Check Server, only normal server!
                        if( !IsServerValid( i.Key.ToString( ) ) )
                            continue;

                        var data = new ServerData
                        {
                            World = int.Parse( i.Key.ToString( ).Trim( trimId ) ),
                            Url = i.Value.ToString( )
                        };
                        result.Add( data );
                    }
                }
            }

            return result;
        }

        public async Task<IList<Player>> FetchPlayerList( ServerData server )
        {
            var reader = new CsvReader( $"{server.Url}/map/player.txt" );
            if( await reader.LoadFileAsync( ) )
            {
                server.Size += reader.ContentLength;
                
                return await reader.ReadFileAsync( ( buffer ) => new Player( )
                {
                    PlayerId = buffer.ReadInt( ),
                    Name = Uri.UnescapeDataString( buffer.ReadString( ).Replace( '+', ' ' ) ),
                    Ally = buffer.ReadInt( ),
                    Villages = buffer.ReadInt( ),
                    Points = buffer.ReadInt( ),
                    Rank = buffer.ReadInt( ),
                    Server = server.Id,
                    //Dataset = server.Dataset + 1,
                } );
            }
            else
            {
                server.IsValid = false;
            }
            throw new FileNotFoundException( );
        }

        public async Task<IList<Ally>> FetchAllyList( ServerData server )
        {
            var reader = new CsvReader( $"{server.Url}/map/ally.txt" );
            if( await reader.LoadFileAsync( ) )
            {
                server.Size += reader.ContentLength;

                return await reader.ReadFileAsync( ( buffer ) => new Ally( )
                {
                    AllyId = buffer.ReadInt( ),
                    Name = Uri.UnescapeDataString( buffer.ReadString( ).Replace( '+', ' ' ) ),
                    Tag = Uri.UnescapeDataString( buffer.ReadString( ).Replace( '+', ' ' ) ),
                    Member = buffer.ReadInt( ),
                    Village = buffer.ReadInt( ),
                    Points = buffer.ReadInt( ),
                    AllPoints = buffer.ReadInt( ),
                    Rank = buffer.ReadInt( ),
                    Server = server.Id,
                    //Dataset = server.Dataset + 1,
                } );
            }
            else
            {
                server.IsValid = false;
            }
            throw new FileNotFoundException( );
        }

        public async Task<IList<Village>> FetchVillageList( ServerData server )
        {
            var reader = new CsvReader( $"{server.Url}/map/village.txt" );
            if( await reader.LoadFileAsync( ) )
            {
                server.Size += reader.ContentLength;

                return await reader.ReadFileAsync( ( buffer ) => new Village( ) 
                {
                    VillageId = buffer.ReadInt(),
                    Name = Uri.UnescapeDataString( buffer.ReadString( ).Replace( '+', ' ' ) ),
                    X = buffer.ReadInt(),
                    Y = buffer.ReadInt(),
                    Owner = buffer.ReadInt( ),
                    Points = buffer.ReadInt( ),
                    Bonus = buffer.ReadInt( ),
                    Server = server.Id
                } );
            }
            else
            {
                server.IsValid = false;
            }
            throw new FileNotFoundException( );
        }

        //public async Task<IList<Bashpoints>> FetchPlayerAtt( ServerData server )
        //{
        //    return await FetchBashpoints( server, "map/kill_att.txt" );
        //}

        //public async Task<IList<Bashpoints>> FetchPlayerDef( ServerData server )
        //{
        //    return await FetchBashpoints( server, "map/kill_def.txt" );
        //}

        //public async Task<IList<Bashpoints>> FetchPlayerAll( ServerData server )
        //{
        //    return await FetchBashpoints( server, "map/kill_all.txt" );
        //}

        public async Task<Dictionary<int, Bashpoints>> FetchBashpoints( ServerData server, string file )
        {
            var reader = new CsvReader( $"{server.Url}/{file}" );
            if( await reader.LoadFileAsync( ) )
            {
                server.Size += reader.ContentLength;

                var data = await reader.ReadFileAsync( ( buffer ) => new Bashpoints( )
                {
                    Rank = buffer.ReadInt( ),
                    Id = buffer.ReadInt( ),
                    Kills = buffer.ReadLong( )
                } );

                return data.ToDictionary( p => p.Id );
            }
            throw new FileNotFoundException( );
        }
    }
}
