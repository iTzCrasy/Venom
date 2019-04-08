using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Venom.Game;
using Venom.Game.Resources;

namespace Venom.ViewModels
{
    public class ViewModelHome
    {
        private readonly Server _server;
        private readonly Profile _profile;
        private readonly ResourceAlly _resourceAlly;
        private readonly ResourcePlayer _resourcePlayer;
        private readonly ResourceVillage _resourceVillage;
        private readonly ResourceConquer _resourceConquer;

        public ViewModelHome( 
            Server server,
            Profile profile,
            ResourceAlly resourceAlly,
            ResourcePlayer resourcePlayer,
            ResourceVillage resourceVillage,
            ResourceConquer resourceConquer )
        {
            _server = server;
            _profile = profile;
            _resourceAlly = resourceAlly;
            _resourcePlayer = resourcePlayer;
            _resourceVillage = resourceVillage;
            _resourceConquer = resourceConquer;
        }

        /// <summary>
        /// Server Statics
        /// </summary>
        public int PlayerCount => _resourcePlayer.GetCount( );
        public int VillageCount => _resourceVillage.GetCount( );
        public int PlayerVillage => 0;
        public int BarbVillage => 0;
        public int BonusVillage => 0;
        public int AllyCount => _resourceAlly.GetCount( );
        public int PlayerInAlly => _resourcePlayer.GetPlayerList( ).Where( x => x.Ally > 0 ).Count( );
        public int PlayerOutAlly => _resourcePlayer.GetPlayerList( ).Where( x => x.Ally == 0 ).Count( );
        public int ConquerCount => _resourceConquer.GetCount( );

        /// <summary>
        /// Server Settings
        /// </summary>
        public string ServerId => _server.Local.Id;
        public string IsArcher => _server.Local.Config.Archer ? "Ja" : "Nein";
    }
}

