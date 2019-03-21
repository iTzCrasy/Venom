using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Venom.Domain;
using Venom.Game.Resources;

namespace Venom.ViewModels
{
    public class RankingAllyViewModel : NotifyPropertyChangedExt
    {
        private readonly ResourceAlly _resourceAlly;

        private AllyData _selectedAlly;
        private string _editAllyname;

        public RankingAllyViewModel( ResourceAlly resourceAlly )
        {
            _resourceAlly = resourceAlly;
        }

        public ICommand CmdSearchAlly => new CommandExt( OnSearchAlly );

        private void OnSearchAlly( object o )
        {
            var data = _resourceAlly.GetAllyByName( _editAllyname );
            if( data != null )
            {
                SelectedAlly = data;
            }
        }

        public IEnumerable<AllyData> AllyList
        {
            get => _resourceAlly.GetAllyList( ).OrderByDescending( x => x.Points );
        }

        public AllyData SelectedAlly
        {
            get => _selectedAlly;
            set => SetProperty( ref _selectedAlly, value, "SelectedAlly" );
        }

        public string EditAllyname
        {
            get => _editAllyname;
            set => SetProperty( ref _editAllyname, value, "EditAllyname" );
        }
    }
}
