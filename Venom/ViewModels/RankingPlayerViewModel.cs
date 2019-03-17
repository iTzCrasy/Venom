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
        protected DataRowView _selectedRow;
        public IEnumerable<PlayerData> PlayerList { get; set; }

        public RankingPlayerViewModel()
        {
        }

        public ICommand CmdSearchPlayer => new CommandExt( OnSearchPlayer );

        private void OnSearchPlayer( object o )
        {
            var data = Global.PlayerResource.GetPlayerByName( "Moralbasher" );
            if( data != null )
            {
                
            }
        }

        public DataRowView selectedRow
        {
            get => _selectedRow;
            set => SetProperty( ref _selectedRow, value );
        }
    }
}
