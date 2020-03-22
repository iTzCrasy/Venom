using System;
using System.Collections.Generic;
using System.Text;


//=> Bonus IDs.:
//      0   = No Bonus
//      1	= Bonus => 100% Holz
//      2	= Bonus => 100% Lehm
//      3	= Bonus => 100% Eisen
//      4	= Bonus => 10% Bauernhof
//      5	= Bonus => 33% Kaserne
//      6	= Bonus => 33% Stall
//      7 	= Bonus => 50% Werkstatt
//      8	= Bonus => 30% Rohstoffe
//      9 	= Bonus => 50% Speicher / Händler
//      11  = Belagerung => -25% Verteidigungsstärke. 7 Siegpunkte täglich.
//      12  = Belagerung => -30% Verteidigungsstärke. 9 Siegpunkte täglich.
//      13  = Belagerung => -35% Verteidigungsstärke. 10 Siegpunkte täglich.
//      14  = Belagerung => -40% Verteidigungsstärke. 11 Siegpunkte täglich.
//      15  = Belagerung => -45% Verteidigungsstärke. 13 Siegpunkte täglich.
//      16  = Belagerung => -50% Verteidigungsstärke. 15 Siegpunkte täglich.
//      17  = Belagerung => -55% Verteidigungsstärke. 17 Siegpunkte täglich.
//      18  = Belagerung => -60% Verteidigungsstärke. 19 Siegpunkte täglich.
//      19  = Belagerung => -65% Verteidigungsstärke. 21 Siegpunkte täglich.
//      20  = Belagerung => -70% Verteidigungsstärke. 23 Siegpunkte täglich.
//      21  = Belagerung => -75% Verteidigungsstärke. 25 Siegpunkte täglich.
//      22  = Festungen => Stammesfestung Stufe 0
//      23  = Festungen => Stammesfestung Stufe 1
//      24  = Festungen => Stammesfestung Stufe 2
//      25  = Festungen => Stammesfestung Stufe 3
//      26  = Festungen => Stammesfestung Stufe 4
//      27  = Festungen => Stammesfestung Stufe 5
//      28  = Festungen => Stammesfestung Stufe 6
//      29  = Festungen => Stammesfestung Stufe 7
//      30  = Festungen => Stammesfestung Stufe 8
//      31  = Festungen => Stammesfestung Stufe 9
//      32  = Festungen => Stammesfestung Stufe 10
//      33  = Festungen => Universitätsstadt (Speicher Boost)



namespace Venom.Data.Models
{
    public class Village
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public int Owner { get; set; }

        public int Points { get; set; }

        public int Bonus { get; set; }
    }
}
