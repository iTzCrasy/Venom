using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        private readonly ResourceTroup _resourceTroup;
        private readonly ResourceVillage _resourceVillage;
        public ClipboardHandler( 
            ResourcePlayer resourcePlayer,
            ResourceTroup resourceTroup,
            ResourceVillage resourceVillage )
        {
            _resourcePlayer = resourcePlayer;
            _resourceTroup = resourceTroup;
            _resourceVillage = resourceVillage;
        }

        public void Parse()
        {
            if( Clipboard.ContainsText( TextDataFormat.Html ) )
            {
                var watch = new Stopwatch( );
                var Text = Clipboard.GetText( TextDataFormat.Html );
                var htmlDoc = new HtmlDocument( );
                htmlDoc.LoadHtml( Text );

                var group = htmlDoc.DocumentNode.SelectNodes( "//strong[@class=\"group-menu-item\"]" );
                if( group != null )
                {
                    var test = group.ElementAt( 0 );

                    //=> &gt;45 Off Voll&lt;
                    Console.WriteLine( test.InnerText.
                        Replace( "&gt;", "" ).
                        Replace( "&lt;", "" ) );
                }

                //=> Parse Troups
                var unitsTable = htmlDoc.DocumentNode.SelectNodes( "//table[@id=\"units_table\"]" );
                if( unitsTable != null && group != null)
                {
                    watch.Start( );

                    foreach( var node in unitsTable.Elements() )
                    {
                        var troupData = "";
                        var matchCoord = Regex.Match( node.InnerText, @"\(\d+\|\d+\)" );
                        if( matchCoord.Success )
                        {
                            troupData += matchCoord.Value + " ";
                        }

                        foreach( var villageNode in node.ChildNodes )
                        {
                            foreach( var troupNode in villageNode.ChildNodes )
                            {
                                if( !troupNode.InnerHtml.Contains( "href" ) )
                                {
                                    troupData += troupNode.InnerText + " ";
                                }
                            }
                        }

                        _resourceTroup.Parse( troupData );
                    }

                    _resourceTroup.Save( );

                    watch.Stop( );
                    App.Instance.TrayIcon.ShowInfo( "Loading", $"Loading Troups Finished in {watch.ElapsedMilliseconds}ms" );
                }

                //=> Parse Groups
                var groupsTable = htmlDoc.DocumentNode.SelectNodes( "//table[@id=\"group_assign_table\"]" );
                if( groupsTable != null )
                {
                    foreach( var node in groupsTable.Elements() )
                    {
                        Console.WriteLine( node.InnerHtml );
                    }
                }
            }
        }
    }
}

//=> group_assign_table
