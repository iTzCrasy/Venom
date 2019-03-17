using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venom.Core;

namespace Venom.Game.Resources
{
    internal class AllyResource : IResource
    {
        private readonly Dictionary<int, AllyData> _allyData = new Dictionary<int, AllyData>( );
        public async Task Initialize( ServerInfo server )
        {
            var allyData = await CSVReader.DownloadFileAsync(
                new Uri( server.Url + "/map/ally.txt" ),
                ( buffer ) => new AllyData
                {
                    Id = buffer.ReadInt( ),
                    Name = Uri.UnescapeDataString( buffer.ReadString( ) ).Replace( '+', ' ' ),
                    Tag = Uri.UnescapeDataString( buffer.ReadString( ) ).Replace( '+', ' ' ),
                    Members = buffer.ReadInt( ),
                    Villages = buffer.ReadInt( ),
                    Points = buffer.ReadInt( ),
                    AllPoints = buffer.ReadInt( ),
                    Rank = buffer.ReadInt( )
                } );

            foreach( var i in allyData )
            {
                AddAlly( i );
            }
        }

        private void AddAlly( AllyData data )
        {
            _allyData.Add( data.Id, data );
        }
    }

    public class AllyData
    {
        //=> $id, $name, $tag, $members, $villages, $points, $all_points, $rank
        public int Id { get; set; }
        public string Name { get; set; }
        public string Tag { get; set; }
        public int Members { get; set; }
        public int Villages { get; set; }
        public int Points { get; set; }
        public int AllPoints { get; set; }
        public int Rank { get; set; }
    }
}
