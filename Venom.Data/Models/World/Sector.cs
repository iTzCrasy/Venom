using System;
using System.Collections.Generic;
using System.Text;

namespace Venom.Data.Models.World
{
    public class Sector : WorldBase
    {
        private Dictionary<int, Field> _fieldList;

        public void GenerateField( )
        {
            _fieldList = new Dictionary<int, Field>( )
            {
                [00] = new Field { },
                [01] = new Field { },
                [02] = new Field { },
                [03] = new Field { },
                [04] = new Field { },
                [05] = new Field { },
                [06] = new Field { },
                [07] = new Field { },
                [08] = new Field { },
            };
        }
    }
}
