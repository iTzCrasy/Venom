using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Venom.Game.Resources;

namespace Venom.Game
{
    public class ResourceHandler
    {
        private readonly Server _server;
        private readonly Profile _profile;
        private readonly ResourcePlayer _resourcePlayer;
        private readonly ResourceAlly _resourceAlly;
        private readonly ResourceConquer _resourceConquer;
        private readonly ResourceTroup _resourceTroup;
        private readonly ResourceVillage _resourceVillage;
        private readonly ResourceBashpointPlayer _resourceBashpointPlayer;
        private readonly ResourceBashpointAlly _resourceBashpointAlly;
        private readonly GroupHandler _groupHandler;

        public ResourceHandler( 
            Server server,
            Profile profile,
            ResourcePlayer resourcePlayer,
            ResourceAlly resourceAlly,
            ResourceConquer resourceConquer,
            ResourceTroup resourceTroup,
            ResourceVillage resourceVillage,
            ResourceBashpointPlayer resourceBashpointPlayer,
            ResourceBashpointAlly resourceBashpointAlly,
            GroupHandler groupHandler )
        {
            _server = server;
            _profile = profile;
            _resourcePlayer = resourcePlayer;
            _resourceAlly = resourceAlly;
            _resourceConquer = resourceConquer;
            _resourceTroup = resourceTroup;
            _resourceVillage = resourceVillage;
            _resourceBashpointPlayer = resourceBashpointPlayer;
            _resourceBashpointAlly = resourceBashpointAlly;
            _groupHandler = groupHandler;
        }

        /// <summary>
        /// Generate Player Ranking with join resources, combine them.
        /// </summary>
        /// <returns>object / IEnumerable</returns>
        public object CreatePlayerRanking() =>
            from p in _resourcePlayer.GetPlayerList( )
            join a in _resourceAlly.GetAllyList( ) on p.Ally equals a.Id
            join att in _resourceBashpointPlayer.GetBashpointAttList() on p.Id equals att.Id
            join def in _resourceBashpointPlayer.GetBashpointDefList() on p.Id equals def.Id
            join all in _resourceBashpointPlayer.GetBashpointAllList() on p.Id equals all.Id
            join con in _resourceConquer.GetConquerList() on p.Id equals con.NewOwner into conquerList
            orderby p.Points descending
            select new
            {
                p.Rank,
                p.Name,
                a.Tag,
                p.Villages,
                p.Points,
                p.PointsVillage,
                BashpointAtt = att.Kills,
                BashpointDef = def.Kills,
                BashpointAll = all.Kills,
                BashpointSup = all.Kills - ( att.Kills + def.Kills ),
                Barbs = conquerList.Where( p => p.OldOwner.Equals( 0 ) ).Count()
            };

        /// <summary>
        /// Generate Ally ranking with join resources, combine them.
        /// </summary>
        /// <returns>object / IEnumerable</returns>
        public object CreateAllyRanking( ) =>
            from a in _resourceAlly.GetAllyList()
            join att in _resourceBashpointAlly.GetBashpointAttList() on a.Id equals att.Id
            join def in _resourceBashpointAlly.GetBashpointDefList() on a.Id equals def.Id
            join all in _resourceBashpointAlly.GetBashpointAllList() on a.Id equals all.Id
            orderby a.AllPoints descending
            select new
            {
                a.Rank,
                a.Name,
                a.Tag,
                a.Members,
                a.Villages,
                a.AllPoints,
                BashpointAtt = att.Kills,
                BashpointDef = def.Kills,
                BashpointAll = all.Kills
            };

        /// <summary>
        /// Generate Troup ranking with join resources, combine them.
        /// </summary>
        /// <returns></returns>
        public object CreateTroupList( ) =>
            from v in _resourceVillage.GetVillageList( ).Where( x => x.Owner.Equals( _resourcePlayer.GetPlayerByName( _profile.Local.Name ).Id ) )
            from g in _groupHandler.GetGroupList().Where( x => x.Village.Id.Equals( v.Id ) ).DefaultIfEmpty( new GroupData() ) 
            //join a in _groupHandler.GetGroupList( ) on v.Id equals a.Village.Id
            select new
            {
                g.Groups,
                v.Name,
                v.CoordString,
                v.Points
            };


        // prodGroup.DefaultIfEmpty(new Product { Name = String.Empty, CategoryID = 0 })
        public ResourcePlayer Player => _resourcePlayer;
    }
}
