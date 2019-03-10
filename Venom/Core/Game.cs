using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Core
{
    public class Game : Singleton<Game>
    {
        public Game()
        {
            _Players.Clear();
            _Allys.Clear();
            _Villages.Clear();
            _Conquers.Clear();

            _BashpointsAtt.Clear();
            _BashpointsDef.Clear();
            _BashpointsAll.Clear();


        }

        public Task Load( bool Full )
        {
            if (SelectedServer.Equals( default( ServerInfo ) ))
            {
                throw new Exception( "Loading Server Player Failed, invalid Server!" );
            }

            var Tasks = new List<Task>()
            {
                LoadPlayer()
            };

            if( Full )
            {
                Tasks.Add( LoadAllys() );
                Tasks.Add( LoadVillages() );
                Tasks.Add( LoadConquers() );
                Tasks.Add( LoadBashpointsPlayer() );
                Tasks.Add( LoadBashpointsAllys() );
            }

            return Task.WhenAll( Tasks );
        }

        public async Task LoadPlayer()
        {
            var Watch = new Stopwatch();
            Watch.Start();

            var Player = await CSVReader.DownloadFileAsync(
                new Uri( SelectedServer.Url + "/map/player.txt" ),
                (buffer) => new GamePlayers
                {
                    ID = buffer.ReadInt(),
                    Name = Uri.UnescapeDataString( buffer.ReadString() ).Replace( '+', ' ' ),
                    Ally = buffer.ReadInt(),
                    Villages = buffer.ReadInt(),
                    Points = buffer.ReadInt(),
                    Rank = buffer.ReadInt()
                } );
            _Players = Player.ToList( );

            Watch.Stop();
            Debug.WriteLine( "Loaded Data: " + Watch.ElapsedMilliseconds + "ms" );
        }

        public async Task LoadAllys()
        {
            var Watch = new Stopwatch();
            Watch.Start();

            var Allys = await CSVReader.DownloadFileAsync(
                new Uri( SelectedServer.Url + "/map/ally.txt" ),
                (buffer) => new GameAllys
                {
                    ID = buffer.ReadInt(),
                    Name = Uri.UnescapeDataString( buffer.ReadString() ).Replace( '+', ' ' ),
                    Tag = Uri.UnescapeDataString( buffer.ReadString() ).Replace( '+', ' ' ),
                    Members = buffer.ReadInt(),
                    Villages = buffer.ReadInt(),
                    Points = buffer.ReadInt(),
                    AllPoints = buffer.ReadInt(),
                    Rank = buffer.ReadInt()
                } );
            _Allys = Allys.ToList( );

            Watch.Stop();
            Debug.WriteLine( "Loaded Data: " + Watch.ElapsedMilliseconds + "ms" );
        }

        public async Task LoadVillages()
        {
            var Watch = new Stopwatch();
            Watch.Start();

            var Villages = await CSVReader.DownloadFileAsync(
                new Uri( SelectedServer.Url + "/map/village.txt" ),
                (buffer) => new GameVillages
                {
                    ID = buffer.ReadInt(),
                    Name = buffer.ReadString(),
                    X = buffer.ReadInt(),
                    Y = buffer.ReadInt(),
                    Owner = buffer.ReadInt(),
                    Points = buffer.ReadInt(),
                    Bonus = buffer.ReadInt()
                } );
            _Villages = Villages.ToList( );

            Watch.Stop();
            Debug.WriteLine( "Loaded Data: " + Watch.ElapsedMilliseconds + "ms" );
        }

        public async Task LoadConquers()
        {
            var Watch = new Stopwatch();
            Watch.Start();

            var Conquers = await CSVReader.DownloadFileAsync(
                new Uri( SelectedServer.Url + "/map/conquer.txt" ),
                (buffer) => new GameConquers
                {
                    ID = buffer.ReadInt(),
                    Time = buffer.ReadInt(),
                    NewOwner = buffer.ReadInt(),
                    OldOwner = buffer.ReadInt()
                } );
            _Conquers = Conquers.ToList( );

            Watch.Stop();
            Debug.WriteLine( "Loaded Data: " + Watch.ElapsedMilliseconds + "ms" );
        }

        public async Task LoadBashpointsPlayer()
        {
            var Watch = new Stopwatch();
            Watch.Start();

            //=> Loading Bashpoints Att
            var BashpointsAtt = await CSVReader.DownloadFileAsync(
                new Uri( SelectedServer.Url + "/map/kill_att.txt" ),
                (buffer) => new GameBashpoints
                {
                    Rank = buffer.ReadInt(),
                    ID = buffer.ReadInt(),
                    Kills = buffer.ReadLong( )
                } );
            _BashpointsAtt = BashpointsAtt.ToDictionary( x => x.ID, y => y );

            //=> Loading Bashpoints Def
            var BashpointsDef = await CSVReader.DownloadFileAsync(
                new Uri( SelectedServer.Url + "/map/kill_def.txt" ),
                (buffer) => new GameBashpoints
                {
                    Rank = buffer.ReadInt(),
                    ID = buffer.ReadInt(),
                    Kills = buffer.ReadLong( )
                } );
            _BashpointsDef = BashpointsDef.ToDictionary( x => x.ID, y => y );

            //=> Loading Bashpoints Att
            var BashpointsAll = await CSVReader.DownloadFileAsync(
                new Uri( SelectedServer.Url + "/map/kill_all.txt" ),
                (buffer) => new GameBashpoints
                {
                    Rank = buffer.ReadInt(),
                    ID = buffer.ReadInt(),
                    Kills = buffer.ReadLong( )
                } );
            _BashpointsAll = BashpointsAll.ToDictionary( x => x.ID, y => y );

            Watch.Stop();
            Debug.WriteLine( "Loaded Data: " + Watch.ElapsedMilliseconds + "ms" );
        }

        public async Task LoadBashpointsAllys()
        {
            var Watch = new Stopwatch();
            Watch.Start();

            //=> Loading Bashpoints Att
            var BashpointsAllysAtt = await CSVReader.DownloadFileAsync(
                new Uri( SelectedServer.Url + "/map/kill_att_tribe.txt" ),
                (buffer) => new GameBashpoints
                {
                    Rank = buffer.ReadInt(),
                    ID = buffer.ReadInt(),
                    Kills = buffer.ReadLong( )
                } );
            _BashpointsAllysAtt = BashpointsAllysAtt.ToList( );

            //=> Loading Bashpoints Def
            var BashpointsAllysDef = await CSVReader.DownloadFileAsync(
                new Uri( SelectedServer.Url + "/map/kill_def_tribe.txt" ),
                (buffer) => new GameBashpoints
                {
                    Rank = buffer.ReadInt(),
                    ID = buffer.ReadInt(),
                    Kills = buffer.ReadLong()
                } );
            _BashpointsAllysDef = BashpointsAllysDef.ToList( );

            //=> Loading Bashpoints Att
            var BashpointsAllysAll = await CSVReader.DownloadFileAsync(
                new Uri( SelectedServer.Url + "/map/kill_all_tribe.txt" ),
                (buffer) => new GameBashpoints
                {
                    Rank = buffer.ReadInt(),
                    ID = buffer.ReadInt(),
                    Kills = buffer.ReadLong( )
                } );
            _BashpointsAllysAll = BashpointsAllysAll.ToList( );

            Watch.Stop();
            Debug.WriteLine( "Loaded Data: " + Watch.ElapsedMilliseconds + "ms" );
        }
       


        /// <summary>
        /// Player Data Section
        /// </summary>
        public GamePlayers GetPlayer( int ID ) => _Players.FirstOrDefault( p => p.ID.Equals( ID ) );
        public GamePlayers GetPlayer( string Name ) => _Players.FirstOrDefault( p => p.Name.Equals( Name ) );
        public List<GamePlayers> GetPlayerByAlly( int Ally ) => _Players.FindAll( p => p.Ally.Equals( Ally ) );
        public List<GamePlayers> GetPlayerAllyAll( ) => _Players.FindAll( p => p.Ally > 0 );
        public List<GamePlayers> GetPlayerList( ) => _Players;

        public List<GamePlayers> _Players = new List<GamePlayers>();

        protected GamePlayers SelectedPlayer
        {
            get;
            set;
        } = default( GamePlayers );

        public void SetSelectedPlayer( string Name ) => SelectedPlayer = GetPlayer( Name );
        public GamePlayers GetSelectedPlayer() => SelectedPlayer;

        /// <summary>
        /// Ally Data Section
        /// </summary>
        public GameAllys GetAlly( int ID ) => _Allys.FirstOrDefault( p => p.ID.Equals( ID ) );
        public GameAllys GetAllyByName( string Name ) => _Allys.FirstOrDefault( p => p.Name.Equals( Name ) );
        public GameAllys GetAllyByTag( string Tag ) => _Allys.FirstOrDefault( p => p.Tag.Equals( Tag ) );
        public List<GameAllys> GetAllyList( ) => _Allys;
        
        protected List<GameAllys> _Allys = new List<GameAllys>();

        /// <summary>
        /// Village Data Section
        /// </summary>
        public GameVillages GetVillage( int ID ) => _Villages.FirstOrDefault( p => p.ID.Equals( ID ) ); 
        public GameVillages GetVillage( string Name ) => _Villages.FirstOrDefault( p => p.Name.Equals( Name ) ); 
        public List<GameVillages> GetVillagesByPlayer( int ID ) => _Villages.FindAll( p => p.Owner.Equals( ID ) );
        public List<GameVillages> GetVillagesByBonus( int Bonus ) => _Villages.FindAll( p => p.Bonus.Equals( Bonus ) );
        public List<GameVillages> GetVillagesBonusAll( ) => _Villages.FindAll( p => p.Bonus > 0 );
        public List<GameVillages> GetVillageList( ) => _Villages;

        protected List<GameVillages> _Villages = new List<GameVillages>();

        /// <summary>
        /// Conquer Data Section
        /// </summary>
        public List<GameConquers> GetConquerList( ) => _Conquers;
        protected List<GameConquers> _Conquers = new List<GameConquers>();

        /// <summary>
        /// Bashpoint Data Section
        /// </summary>
        public GameBashpoints GetBashpointsPlayer( int ID, int Type )
        {
            switch (Type)
            {
                case 0:
                    return _BashpointsAtt.FirstOrDefault( p => p.Value.ID.Equals( ID ) ).Value;
                case 1:
                    return _BashpointsDef.FirstOrDefault( p => p.Value.ID.Equals( ID ) ).Value;
                case 2:
                    return _BashpointsAll.FirstOrDefault( p => p.Value.ID.Equals( ID ) ).Value;
            }
            return default( GameBashpoints );
        }
        public GameBashpoints GetBashpointsAlly( int ID, int Type )
        {
            switch( Type )
            {
                case 0:
                    return _BashpointsAllysAtt.FirstOrDefault( p => p.ID.Equals( ID ) );
                case 1:
                    return _BashpointsAllysDef.FirstOrDefault( p => p.ID.Equals( ID ) );
                case 2:
                    return _BashpointsAllysAll.FirstOrDefault( p => p.ID.Equals( ID ) );
            }
            return default( GameBashpoints );
        }
        public Dictionary<int, GameBashpoints> _BashpointsAtt = new Dictionary<int, GameBashpoints>();
        public Dictionary<int, GameBashpoints> _BashpointsDef = new Dictionary<int, GameBashpoints>();
        public Dictionary<int, GameBashpoints> _BashpointsAll = new Dictionary<int, GameBashpoints>();
        public List<GameBashpoints> _BashpointsAllysAtt = new List<GameBashpoints>();
        public List<GameBashpoints> _BashpointsAllysDef = new List<GameBashpoints>();
        public List<GameBashpoints> _BashpointsAllysAll = new List<GameBashpoints>();

        /// <summary>
        /// Server Base
        /// </summary>
        protected ServerInfo SelectedServer
        {
            get;
            set;
        } = default( ServerInfo );

        public void SetSelectedServer( string ID ) => SelectedServer = _ServerList.FirstOrDefault( p => p.ID.Equals( ID ) );
        public ServerInfo GetSelectedServer() => SelectedServer;

        public void LoadServerList()
        {
            if( _ServerList.Count.Equals( 0 ) ) //=> Check Empty list, if empty LOAD!
            {
                var ObjectList = new DeserializePHP( Network.GetUrlRead( "http://www.die-staemme.de/backend/get_servers.php" ) ).Deserialize();
                if (ObjectList is IEnumerable Enum)
                {
                    foreach (DictionaryEntry Item in Enum)
                    {
                        _ServerList.Add( new ServerInfo() { ID = Item.Key.ToString(), Url = Item.Value.ToString() } );
                    }
                }
            }
        }

        public List<ServerInfo> GetServerList() => _ServerList; 
        protected List<ServerInfo> _ServerList = new List<ServerInfo>();
    }

    public struct GamePlayers
    {
        //=> $id, $name, $ally, $villages, $points, $rank
        public int ID { get; set; }
        public string Name { get; set; }
        public int Ally { get; set; }
        public int Villages { get; set; }
        public int Points { get; set; }
        public int Rank { get; set; }

        //=> Custom
        public int PointsVillage => Points / Villages;
        public string AllyString => Game.GetInstance.GetAlly( Ally ).Tag;
        public long BashpointsAtt => Game.GetInstance.GetBashpointsPlayer( ID, 0 ).Kills;
        public long BashpointsDef => Game.GetInstance.GetBashpointsPlayer( ID, 1 ).Kills;
        public long BashpointsAll => Game.GetInstance.GetBashpointsPlayer( ID, 2 ).Kills;
        public long BashpointsSup => Game.GetInstance.GetBashpointsPlayer( ID, 2 ).Kills - ( Game.GetInstance.GetBashpointsPlayer( ID, 0 ).Kills + Game.GetInstance.GetBashpointsPlayer( ID, 1 ).Kills );
    }

    public struct GameVillages
    {
        //=> $id, $name, $x, $y, $player, $points, $bonus
        public int ID { get; set; }
        public string Name { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Owner { get; set; }
        public int Points { get; set; }
        public int Bonus { get; set; }
    }

    public struct GameAllys
    {
        //=> $id, $name, $tag, $members, $villages, $points, $all_points, $rank
        public int ID { get; set; }
        public string Name { get; set; }
        public string Tag { get; set; }
        public int Members { get; set; }
        public int Villages { get; set; }
        public int Points { get; set; }
        public int AllPoints { get; set; }
        public int Rank { get; set; }

        //=> Custom
        public long BashpointsAtt => Game.GetInstance.GetBashpointsAlly( ID, 0 ).Kills;
        public long BashpointsDef => Game.GetInstance.GetBashpointsAlly( ID, 1 ).Kills;
        public long BashpointsAll => Game.GetInstance.GetBashpointsAlly( ID, 2 ).Kills;
    }

    public struct GameConquers
    {
        //=> $village_id, $unix_timestamp, $new_owner, $old_owner
        public int ID { get; set; }
        public int Time { get; set; }
        public int NewOwner { get; set; }
        public int OldOwner { get; set; }
    }

    public struct GameBashpoints
    {
        //=> $rank, $id, $kills
        public int ID { get; set; }
        public long Kills { get; set; }
        public int Rank { get; set; }
    }

    public struct ServerInfo
    {
        public string ID { get; set; }
        public string Url { get; set; }
    }
}
