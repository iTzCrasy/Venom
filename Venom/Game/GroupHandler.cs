using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Venom.Game.Resources;

namespace Venom.Game
{
    public class GroupData
    {
        public List<string> Names = new List<string>( );
        public VillageData Village { get; set; }

        public string Groups
        {
            get => Names.Count( ) > 0 ? Names.Aggregate( ( a, b ) => $"{a}; {b}" ) : "";
        }
    }

    public class GroupHandler
    {
        private readonly ResourceVillage _resourceVillage;
        private readonly Dictionary<Tuple<int, int>, GroupData> _groupDataCoord = new Dictionary<Tuple<int, int>, GroupData>( );

        public GroupHandler( ResourceVillage resourceVillage )
        {
            _resourceVillage = resourceVillage;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="group"></param>
        public void HandleSelected( string group, string coord )
        {
            if( string.IsNullOrEmpty( group ) || string.IsNullOrEmpty( coord ) )
            {
                //throw new NotSupportedException();
                return;
            }

            var village = coord.Replace( "(", "" ).Replace( ")", "" ).Split( '|' );
            var villageData = _resourceVillage.GetVillageByCoord( Convert.ToInt32( village[0] ), Convert.ToInt32( village[1] ) );

            if( villageData != null )
            {
                AddGroup( group, villageData );
            }
        }


        public void HandleStatic( string data )
        {

        }

        private void AddGroup( string group, VillageData villageData )
        {
            var groupData = GetGroupByVillage( villageData );
            if( groupData != null )
            {
                if( !groupData.Names.Exists( x => x.Equals( group ) ) )
                {
                    _groupDataCoord[new Tuple<int, int>( villageData.X, villageData.Y )].Names.Add( group );
                }
            }
            else
            {
                groupData = new GroupData( ) { Village = villageData };
                groupData.Names.Add( group );
                _groupDataCoord.Add( new Tuple<int, int>( villageData.X, villageData.Y ), groupData );
            }
        }

        public GroupData GetGroupByVillage( VillageData villageData ) =>
            _groupDataCoord.TryGetValue( new Tuple<int, int>( villageData.X, villageData.Y ), out var group ) ? group : null;

        public List<GroupData> GetGroupList( ) =>
            _groupDataCoord.Values.ToList( );
    }
}
