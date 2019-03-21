using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using HtmlAgilityPack;
using Venom.Game.Resources;

namespace Venom.Domain
{
    public class ClipboardHandler
    {
        private readonly ResourcePlayer _resourcePlayer;
        public ClipboardHandler( ResourcePlayer resourcePlayer )
        {
            _resourcePlayer = resourcePlayer;
        }

        public void Parse()
        {
            if( Clipboard.ContainsText( TextDataFormat.Html ) )
            {
                var Text = Clipboard.GetText( TextDataFormat.Html );
                var htmlDoc = new HtmlDocument( );
                htmlDoc.LoadHtml( Text );

                var nodes = htmlDoc.DocumentNode.SelectNodes( "//table[@id=\"units_table\"]" );
                if( nodes != null )
                {
                    foreach( var node in nodes.Elements() )
                    {
                        foreach( var villageNode in node.ChildNodes )
                        {
                            var testData = "";
                            foreach( var troupNode in villageNode.ChildNodes )
                            {
                                //=> eigene 0 0 0 0 0 0 0 0 0 0 0 Befehle
                                //=> im Dorf 0 0 0 0 0 0 0 0 0 0 0 Truppen
                                //=> auswärts 0 0 0 0 0 0 0 0 0 0 0 Truppen
                                //=> unterwegs 0 0 0 0 0 0 0 0 0 0 0 Befehle
                                //=> 11 Troups --> 13 if archer is enabled

                                if( int.TryParse( troupNode.InnerHtml, out var v ) )
                                {
                                    testData += v + " ";
                                }

                                //testData += troupNode.InnerText + "/";
                                Console.WriteLine( "InnerHtml: " + troupNode.InnerHtml );
                            }
                            Console.WriteLine( "TestData: " + testData );
                        }
                    }
                }
            }
        }
    }
}
