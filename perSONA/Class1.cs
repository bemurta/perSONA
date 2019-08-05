using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace perSONA
{
    public class TonalAudiometryTest
    {
        public double[] Freqs { get; set; } = { 125, 250, 500, 1000, 2000, 4000, 8000 };
        public double[] ThisAudiometry { get; set; } = { };
        public string AudiometryType { get; set; }
        public string Via { get; set; }
        public string Prosthesis { get; set; }
        public string Side { get; set; }
        public bool Masker { get; set; }


        public TonalAudiometryTest()
        {
        }

        public ZedGraph.SymbolType getSymbol()
        {
            if (Masker && Via=="Air" && Side=="Right")
            {
                return ZedGraph.SymbolType.Circle;
            }
            else if (Masker && Via == "Air" && Side == "Left")
            {
                return ZedGraph.SymbolType.XCross;
            }
            else if (!Masker && Via == "Air" && Side == "Left")
            {
                return ZedGraph.SymbolType.Triangle;
            }
            else if (!Masker && Via == "Air" && Side == "Left")
            {
                return ZedGraph.SymbolType.Square;
            }
            else
            {
                return ZedGraph.SymbolType.Star;
            }
        }

        public Color getColor()
        {
            return Side == "Left" ? Color.Red : Color.Blue;
        }



    }
}
