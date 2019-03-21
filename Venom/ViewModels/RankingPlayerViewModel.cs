using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Venom.Domain;
using Venom.Game.Resources;


namespace Venom.ViewModels
{
    public class RankingPlayerViewModel : NotifyPropertyChangedExt
    {
        private readonly ResourcePlayer _resourcePlayer;

        private PlayerData _selectedPlayer;
        private string _editUsername;

        public RankingPlayerViewModel( ResourcePlayer resourcePlayer )
        {
            _resourcePlayer = resourcePlayer;

            SelectedPlayer = _resourcePlayer.GetPlayerByName( "Moralbasher" );
        }

        public ICommand CmdSearchPlayer => new CommandExt( OnSearchPlayer );

        private void OnSearchPlayer( object o )
        {
            var data = _resourcePlayer.GetPlayerByName( EditUsername );
            if( data != null )
            {
                SelectedPlayer = data;
            }
        }

        public IEnumerable<PlayerData> PlayerList
        {
            get => _resourcePlayer.GetPlayerList( ).OrderByDescending( x => x.Points );
        }

        public PlayerData SelectedPlayer
        {
            get => _selectedPlayer;
            set => SetProperty( ref _selectedPlayer, value, "SelectedPlayer" );
        }

        public string EditUsername
        {
            get => _editUsername;
            set => SetProperty( ref _editUsername, value, "EditUsername" );
        }
    }
}
