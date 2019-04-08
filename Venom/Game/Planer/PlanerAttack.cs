using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venom.Game.Planer
{
    public class PlanerAttack : IPlaner
    {
        private string _path;

        public async Task InitializeAsync()
        {
            _path = "";
        }

        public async Task LoadAsync()
        {
            //=> Save JSON
        }

        public async Task SaveAsync()
        {
            //=> Load JSON
        }

    }
}
