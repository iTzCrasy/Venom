using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Venom.Game;
using Venom.Game.Resources;

namespace Venom.ViewModels
{
    public class TroupListViewModel
    {
        private readonly Profile _profile;
        private readonly Server _server;
        private readonly ResourcePlayer _resourcePlayer;
        private readonly ResourceVillage _resourceVillage;

        public TroupListViewModel( 
            Profile profile,
            Server server,
            ResourcePlayer resourcePlayer,
            ResourceVillage resourceVillage )
        {
            _profile = profile;
            _server = server;
            _resourcePlayer = resourcePlayer;
            _resourceVillage = resourceVillage;
        }

        public IEnumerable<VillageData> VillageList
        {
            get => _resourceVillage.GetVillagesByPlayer( _resourcePlayer.GetPlayerByName( _profile.Local.Name ) );
        }

        public Visibility ArcherVisibility
        {
            get => _server.Local.Config.Archer.Equals( 1 ) ? Visibility.Visible : Visibility.Hidden;
        }
    }
}

