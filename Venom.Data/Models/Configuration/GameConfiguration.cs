using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Venom.Data.Models.Configuration
{
    [Serializable]
    [XmlType( "game" )]
    public class GameConfiguration
    {
        [XmlElement( "archer" )]
        public bool Archer { get; set; }
    }
}
