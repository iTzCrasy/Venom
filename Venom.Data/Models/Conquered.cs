using System;
using System.Collections.Generic;
using System.Text;

namespace Venom.Data.Models
{
    public class Conquered
    {
        public int Id { get; set; }

        public DateTime Time { get; set; }

        public int NewOwner { get; set; }

        public int OldOwner { get; set; }
    }
}
