using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZedGraph;
using System.Drawing.Drawing2D;
using System.Net;
using System.Windows;

namespace perSONA
{
    public class TonalAudiometryTest
    {
        public double[] Freqs { get; set; } = {};
        public double[] dB { get; set; } = { };
        public string AudiometryType { get; set; }
        public string Via { get; set; }
        public string Prosthesis { get; set; }
        public string Side { get; set; }
        public bool[] Masker { get; set; } = { };
        public bool[] NoReply { get; set; } = { };


        public ZedGraph.SymbolType getSymbol()
        {
            return ZedGraph.SymbolType.Star;
        }


        public static void drawSymbol(ZedGraphControl zgc, double Freq, double dB, bool Masker, bool NoReply, string Side, string Via)
        {                 
            if (Side == "Right")
            {
                if (Via == "Air")
                {
                    if (NoReply == false)
                    {
                        if (Masker == false) ViaAirREUnmasked(zgc, Freq, dB); //Function Via Air Right Ear Unmarked
                        else ViaAirREMasked(zgc, Freq, dB);
                    }
                    else
                    {
                        if (Masker == false) ViaAirREUnmaskedNoReply(zgc, Freq, dB);
                        else ViaAirREMaskedNoReply(zgc, Freq, dB);
                    }
                }
                else if (Via == "Bone (mastoid)")
                {
                    if (NoReply == false)
                    {
                        if (Masker == false) ViaBoneMREUnmasked(zgc, Freq, dB);
                        else ViaBoneMREMasked(zgc, Freq, dB);
                    }
                    else
                    {
                        if (Masker == false) ViaBoneMREUnmaskedNoReply(zgc, Freq, dB);
                        else ViaBoneMREMaskedNoReply(zgc, Freq, dB);
                    }
                }
                else if (Via == "Bone (forehead)")
                {
                    if (NoReply == false)
                    {
                        if (Masker == false) ViaBoneFREUnmasked(zgc, Freq, dB);
                        else ViaBoneFREMasked(zgc, Freq, dB);
                    }
                    else
                    {
                        if (Masker == false) ViaBoneFREUnmaskedNoReply(zgc, Freq, dB);
                        else ViaBoneFREMaskedNoReply(zgc, Freq, dB);
                    }
                }
                else
                {
                    if (NoReply == false)
                    {
                        if (Masker == false) FFRE(zgc, Freq, dB);
                        else FFREUnspecifiedResponsePresence(zgc, Freq, dB);
                    }
                    else
                    {
                        if (Masker == false) FFRENoReply(zgc, Freq, dB);
                        else FFREUnspecifiedResponseAusence(zgc, Freq, dB);
                    }
                }
            }
            else
            {
                if (Via == "Air")
                {
                    if (NoReply == false)
                    {
                        if (Masker == false) ViaAirLEUnmasked(zgc, Freq, dB);
                        else ViaAirLEMasked(zgc, Freq, dB);
                    }
                    else
                    {
                        if (Masker == false) ViaAirLEUnmaskedNoReply(zgc, Freq, dB);
                        else ViaAirLEMaskedNoReply(zgc, Freq, dB);
                    }
                }
                else if (Via == "Bone (mastoid)")
                {
                    if (NoReply == false)
                    {
                        if (Masker == false) ViaBoneMLEUnmasked(zgc, Freq, dB);
                        else ViaBoneMLEMasked(zgc, Freq, dB);
                    }
                    else
                    {
                        if (Masker == false) ViaBoneMLEUnmaskedNoReply(zgc, Freq, dB);
                        else ViaBoneMLEMaskedNoReply(zgc, Freq, dB);
                    }
                }
                else if (Via == "Bone (forehead)")
                {
                    if (NoReply == false)
                    {
                        if (Masker == false) ViaBoneFLEUnmasked(zgc, Freq, dB);
                        else ViaBoneFLEMasked(zgc, Freq, dB);
                    }
                    else
                    {
                        if (Masker == false) ViaBoneFLEUnmaskedNoReply(zgc, Freq, dB);
                        else ViaBoneFLEMaskedNoReply(zgc, Freq, dB);
                    }
                }
                else
                {
                    if (NoReply == false)
                    {
                        if (Masker == false) FFLE(zgc, Freq, dB);
                        else FFLEUnspecifiedResponsePresence(zgc, Freq, dB);
                    }
                    else
                    {
                        if (Masker == false) FFLENoReply(zgc, Freq, dB);
                        else FFLEUnspecifiedResponseAusence(zgc, Freq, dB);
                    }
                }
            }
        }

        public Color getColor()
        {
            return Side == "Right" ? Color.Red : Color.Blue;
        }
        public void changeLine(Line line)
        {
            if (Via == "Air" && Side == "Left")
            {
                line.Style = DashStyle.Custom;
                line.DashOn = 5;
                line.DashOff = 10;
            }            
            line.Width = 2;
        }

        //RIGHT EAR
        //AIR
        public static void ViaAirREUnmasked(ZedGraphControl zgc, double freq, double dB)
        {
            var curve = zgc.GraphPane.AddCurve(null, new[] { freq }, new[] { dB }, Color.Red);
            curve.IsX2Axis = true;
            curve.Symbol = new Symbol(SymbolType.Circle, Color.Red);
            curve.Symbol.Size = 15f;
        }

        public static void ViaAirREMasked(ZedGraphControl zgc, double freq, double dB)
        {
            var curve = zgc.GraphPane.AddCurve(null, new[] { freq }, new[] { dB }, Color.Red);
            curve.IsX2Axis = true;
            curve.Symbol = new Symbol(SymbolType.Triangle, Color.Red);
            curve.Symbol.Size = 15f;
        }

        public static void ViaAirREUnmaskedNoReply(ZedGraphControl zgc, double freq, double dB)
        {
            var curve = zgc.GraphPane.AddCurve(null, new[] { freq }, new[] { dB }, Color.Red);
            curve.IsX2Axis = true;
            curve.Symbol = new Symbol(SymbolType.UserDefined, Color.Red);
            curve.Symbol.Size = 15f;
            curve.Symbol.UserSymbol = new GraphicsPath(
                new[]
                    {
            new PointF(0f, 0f), new PointF(-0.8f, 0.8f), new PointF(-0.3f, 0.6f), new PointF(-0.8f, 0.8f), new PointF(-0.6f, 0.3f)
                    },
                new[]
                    {
            (byte)PathPointType.Start, (byte)PathPointType.Line, (byte)PathPointType.Start, (byte)PathPointType.Line, (byte)PathPointType.Line
                    });
            curve.Symbol.UserSymbol.AddEllipse(new RectangleF(-0.1f, -0.9f, 1f, 1f));
        }

        public static void ViaAirREMaskedNoReply(ZedGraphControl zgc, double freq, double dB)
        {
            var curve = zgc.GraphPane.AddCurve(null, new[] { freq }, new[] { dB }, Color.Red);
            curve.IsX2Axis = true;
            curve.Symbol = new Symbol(SymbolType.UserDefined, Color.Red);
            curve.Symbol.Size = 15f;
            curve.Symbol.UserSymbol = new GraphicsPath(
        new[]
            {
            new PointF(0.0f, 0.0f), new PointF(1.0f, 0.0f), new PointF(0.5f, -1.0f), new PointF(0f, 0f),
            new PointF(-0.8f, 0.8f), new PointF(-0.3f, 0.6f), new PointF(-0.8f, 0.8f), new PointF(-0.6f, 0.3f)
            },
        new[]
            {
            (byte) PathPointType.Start, (byte) PathPointType.Line, (byte) PathPointType.Line,
            (byte) PathPointType.Line, (byte) PathPointType.Line, (byte) PathPointType.Line,
            (byte) PathPointType.Line, (byte) PathPointType.Line
            });
        }

            //BONE (MASTOID)
        public static void ViaBoneMREUnmasked(ZedGraphControl zgc, double freq, double dB)
        {
            var curve = zgc.GraphPane.AddCurve(null, new[] { freq }, new[] { dB }, Color.Red);
            curve.IsX2Axis = true;
            curve.Symbol = new Symbol(SymbolType.UserDefined, Color.Red);
            curve.Symbol.Size = 15f;
            curve.Symbol.UserSymbol = new GraphicsPath(
                new[]
                    {
            new PointF(0.5f, -0.5f), new PointF(-0.5f, 0f), new PointF(0.5f, 0.5f)
                    },
                new[]
                    {
            (byte)PathPointType.Start, (byte)PathPointType.Line, (byte)PathPointType.Line
                    });
        }

        public static void ViaBoneMREMasked(ZedGraphControl zgc, double freq, double dB)
        {
            var curve = zgc.GraphPane.AddCurve(null, new[] { freq }, new[] { dB }, Color.Red);
            curve.IsX2Axis = true;
            curve.Symbol = new Symbol(SymbolType.UserDefined, Color.Red);
            curve.Symbol.Size = 15f;
            curve.Symbol.UserSymbol = new GraphicsPath(
                new[]
                    {
            new PointF(0.25f, -0.5f), new PointF(-0.25f, -0.5f), new PointF(-0.25f, 0.5f), new PointF(0.25f, 0.5f)
                    },
                new[]
                    {
            (byte)PathPointType.Start, (byte)PathPointType.Line, (byte)PathPointType.Line, (byte)PathPointType.Line
                    });
        }

        public static void ViaBoneMREUnmaskedNoReply(ZedGraphControl zgc, double freq, double dB)
        {
            var curve = zgc.GraphPane.AddCurve(null, new[] { freq }, new[] { dB }, Color.Red);
            curve.IsX2Axis = true;
            curve.Symbol = new Symbol(SymbolType.UserDefined, Color.Red);
            curve.Symbol.Size = 15f;
            curve.Symbol.UserSymbol = new GraphicsPath(
                new[]
                    {
            new PointF(0.0f, -1f), new PointF(-1f, -0.5f), new PointF(0f, 0f),
            new PointF(-0.8f, 0.8f), new PointF(-0.3f, 0.6f), new PointF(-0.8f, 0.8f), new PointF(-0.6f, 0.3f)
                },
                new[]
                    {
            (byte)PathPointType.Start, (byte)PathPointType.Line, (byte)PathPointType.Line,
            (byte)PathPointType.Line, (byte)PathPointType.Line, (byte)PathPointType.Line,
            (byte)PathPointType.Line
                    });
        }

        public static void ViaBoneMREMaskedNoReply(ZedGraphControl zgc, double freq, double dB)
        {
            var curve = zgc.GraphPane.AddCurve(null, new[] { freq }, new[] { dB }, Color.Red);
            curve.IsX2Axis = true;
            curve.Symbol = new Symbol(SymbolType.UserDefined, Color.Red);
            curve.Symbol.Size = 15f;
            curve.Symbol.UserSymbol = new GraphicsPath(
                new[]
                    {
            new PointF(0.5f, -1f), new PointF(0f, -1f), new PointF(0f, 0f), new PointF(0.5f, 0f), new PointF(0f, 0f),
            new PointF(-0.8f, 0.8f), new PointF(-0.3f, 0.6f), new PointF(-0.8f, 0.8f), new PointF(-0.6f, 0.3f)
                    },
                new[]
                    {
            (byte)PathPointType.Start, (byte)PathPointType.Line, (byte)PathPointType.Line, (byte)PathPointType.Line, (byte)PathPointType.Line,
            (byte)PathPointType.Line, (byte)PathPointType.Line, (byte)PathPointType.Line, (byte)PathPointType.Line
                    });
        }

            //BONE (FOREHEAD)
        public static void ViaBoneFREUnmasked(ZedGraphControl zgc, double freq, double dB)
        {
            var curve = zgc.GraphPane.AddCurve(null, new[] { freq }, new[] { dB }, Color.Red);
            curve.IsX2Axis = true;
            curve.Symbol = new Symbol(SymbolType.UserDefined, Color.Red);
            curve.Symbol.Size = 15f;
            curve.Symbol.UserSymbol = new GraphicsPath(
                new[]
                    {
            new PointF(-0.5f, -0.5f), new PointF(0f, 0.5f), new PointF(0.5f, -0.5f)
                    },
                new[]
                    {
            (byte) PathPointType.Start, (byte) PathPointType.Line, (byte) PathPointType.Line
                    });
        }

        public static void ViaBoneFREUnmaskedNoReply(ZedGraphControl zgc, double freq, double dB)
        {
            var curve = zgc.GraphPane.AddCurve(null, new[] { freq }, new[] { dB }, Color.Red);
            curve.IsX2Axis = true;
            curve.Symbol = new Symbol(SymbolType.UserDefined, Color.Red);
            curve.Symbol.Size = 15f;
            curve.Symbol.UserSymbol = new GraphicsPath(
                new[]
                    {
            new PointF(-0.5f, -0.8f), new PointF(0f, 0f), new PointF(0.5f, -0.8f), new PointF(0f, 0f),
            new PointF(0f, 1f), new PointF(0.2f, 0.5f), new PointF(0f, 1f), new PointF(-0.2f, 0.5f)

                    },
                new[]
                    {
            (byte) PathPointType.Start, (byte) PathPointType.Line, (byte) PathPointType.Line, (byte) PathPointType.Line,
             (byte) PathPointType.Line, (byte) PathPointType.Line, (byte) PathPointType.Line, (byte) PathPointType.Line
                    });
        }
        public static void ViaBoneFREMasked(ZedGraphControl zgc, double freq, double dB)
        {
            var curve = zgc.GraphPane.AddCurve(null, new[] { freq }, new[] { dB }, Color.Red);
            curve.IsX2Axis = true;
            curve.Symbol = new Symbol(SymbolType.UserDefined, Color.Red);
            curve.Symbol.Size = 15f;
            curve.Symbol.UserSymbol = new GraphicsPath(
                new[]
                    {
            new PointF(0.25f, -0.5f), new PointF(-0.25f, -0.5f), new PointF(-0.25f, 0.5f)
                    },
                new[]
                    {
            (byte) PathPointType.Start, (byte) PathPointType.Line, (byte) PathPointType.Line
                    });
        }

        public static void ViaBoneFREMaskedNoReply(ZedGraphControl zgc, double freq, double dB)
        {
            var curve = zgc.GraphPane.AddCurve(null, new[] { freq }, new[] { dB }, Color.Red);
            curve.IsX2Axis = true;
            curve.Symbol = new Symbol(SymbolType.UserDefined, Color.Red);
            curve.Symbol.Size = 15f;
            curve.Symbol.UserSymbol = new GraphicsPath(
                new[]
                    {
            new PointF(0.5f, -1f), new PointF(0f, -1f), new PointF(0f, 0f),
            new PointF(-0.8f, 0.8f), new PointF(-0.3f, 0.6f), new PointF(-0.8f, 0.8f), new PointF(-0.6f, 0.3f)
                    },
                new[]
                    {
            (byte)PathPointType.Start, (byte)PathPointType.Line, (byte)PathPointType.Line,
            (byte)PathPointType.Line, (byte)PathPointType.Line, (byte)PathPointType.Line, (byte)PathPointType.Line
                    });
        }

            //FREE FIELD
        public static void FFRE(ZedGraphControl zgc, double freq, double dB)
        {
            var curve = zgc.GraphPane.AddCurve(null, new[] { freq }, new[] { dB }, Color.Red);
            curve.IsX2Axis = true;
            curve.Symbol = new Symbol(SymbolType.UserDefined, Color.Red);
            curve.Symbol.Size = 15f;
            curve.Symbol.UserSymbol = new GraphicsPath(
                new[]
                    {
            new PointF(-0.5f, 0.5f), new PointF(0.5f, -0.5f)
                    },
                new[]
                    {
            (byte)PathPointType.Start, (byte)PathPointType.Line
                    });
            curve.Symbol.UserSymbol.AddEllipse(new RectangleF(-0.5f, -0.5f, 1f, 1f));
        }

        public static void FFRENoReply(ZedGraphControl zgc, double freq, double dB)
        {
            var curve = zgc.GraphPane.AddCurve(null, new[] { freq }, new[] { dB }, Color.Red);
            curve.IsX2Axis = true;
            curve.Symbol = new Symbol(SymbolType.UserDefined, Color.Red);
            curve.Symbol.Size = 15f;
            curve.Symbol.UserSymbol = new GraphicsPath(
                new[]
                    {
            new PointF(1f, -1f), new PointF(0f, 0f),
            new PointF(-0.8f, 0.8f), new PointF(-0.3f, 0.6f), new PointF(-0.8f, 0.8f), new PointF(-0.6f, 0.3f)
                    },
                new[]
                    {
            (byte)PathPointType.Start, (byte)PathPointType.Line,
            (byte)PathPointType.Line, (byte)PathPointType.Line, (byte)PathPointType.Start, (byte)PathPointType.Line
                    });
            curve.Symbol.UserSymbol.AddEllipse(new RectangleF(0f, -1f, 1f, 1f));
        }

        public static void FFREUnspecifiedResponsePresence(ZedGraphControl zgc, double freq, double dB)
        {
            var curve = zgc.GraphPane.AddCurve(null, new[] { freq }, new[] { dB }, Color.Red);
            curve.IsX2Axis = true;
            curve.Symbol = new Symbol(SymbolType.UserDefined, Color.Red);
            curve.Symbol.Size = 8f;
            curve.Symbol.UserSymbol = new GraphicsPath();
            curve.Symbol.UserSymbol.AddArc(new Rectangle(0, 0, 1, 1), 130, -180);
            curve.Symbol.UserSymbol.AddArc(new Rectangle(0, -1, 1, 1), 130, 180);
        }

        public static void FFREUnspecifiedResponseAusence(ZedGraphControl zgc, double freq, double dB)
        {
            var curve = zgc.GraphPane.AddCurve(null, new[] { freq }, new[] { dB }, Color.Red);
            curve.IsX2Axis = true;
            curve.Symbol = new Symbol(SymbolType.UserDefined, Color.Red);
            curve.Symbol.Size = 8f;
            curve.Symbol.UserSymbol = new GraphicsPath(
                new[]
                    {
           new PointF(0.4f, 1.0f), new PointF(0.4f, 2.2f), new PointF(0.7f, 1.6f), new PointF(0.4f, 2.2f), new PointF(0.1f, 1.6f)

                    },
                new[]
                    {
            (byte)PathPointType.Start, (byte)PathPointType.Line, (byte)PathPointType.Line, (byte)PathPointType.Line, (byte)PathPointType.Line
                    });
            curve.Symbol.UserSymbol.AddArc(new Rectangle(0, 0, 1, 1), 130, -180);
            curve.Symbol.UserSymbol.AddArc(new Rectangle(0, -1, 1, 1), 130, 180);
        }

        //////////////////////////////////

        //RIGHT EAR

            //AIR
        public static void ViaAirLEUnmasked(ZedGraphControl zgc, double freq, double dB)
        {
            var curve = zgc.GraphPane.AddCurve(null, new[] { freq }, new[] { dB }, Color.Blue);
            curve.IsX2Axis = true;
            curve.Symbol = new Symbol(SymbolType.XCross, Color.Blue);
            curve.Symbol.Size = 15f;
        }

        public static void ViaAirLEMasked(ZedGraphControl zgc, double freq, double dB)
        {
            var curve = zgc.GraphPane.AddCurve(null, new[] { freq }, new[] { dB }, Color.Blue);
            curve.IsX2Axis = true;
            curve.Symbol = new Symbol(SymbolType.Square, Color.Blue);
            curve.Symbol.Size = 15f;
        }

        public static void ViaAirLEUnmaskedNoReply(ZedGraphControl zgc, double freq, double dB)
        {
            var curve = zgc.GraphPane.AddCurve(null, new[] { freq }, new[] { dB }, Color.Blue);
            curve.IsX2Axis = true;
            curve.Symbol = new Symbol(SymbolType.UserDefined, Color.Blue);
            curve.Symbol.Size = 15f;
            curve.Symbol.UserSymbol = new GraphicsPath(
        new[]
            {
            new PointF(-1f, -1f), new PointF(-0.5f, -0.5f), new PointF(0f, -1f), new PointF(-1f, 0f), new PointF(-0.5f, -0.5f), new PointF(0f, 0f),
            new PointF(0.8f, 0.8f), new PointF(0.3f, 0.6f), new PointF(0.8f, 0.8f), new PointF(0.6f, 0.3f)
            },
        new[]
            {
            (byte) PathPointType.Start, (byte) PathPointType.Line, (byte) PathPointType.Line,
            (byte) PathPointType.Line, (byte) PathPointType.Line, (byte) PathPointType.Line,
            (byte) PathPointType.Line, (byte) PathPointType.Line, (byte) PathPointType.Line, (byte) PathPointType.Line
            });
        }

        public static void ViaAirLEMaskedNoReply(ZedGraphControl zgc, double freq, double dB)
        {
            var curve = zgc.GraphPane.AddCurve(null, new[] { freq }, new[] { dB }, Color.Blue);
            curve.IsX2Axis = true;
            curve.Symbol = new Symbol(SymbolType.UserDefined, Color.Blue);
            curve.Symbol.Size = 15f;
            curve.Symbol.UserSymbol = new GraphicsPath(
                new[]
                    {
            new PointF(0f, 0f), new PointF(-1f, 0f), new PointF(-1f, -1f), new PointF(0f, -1f), new PointF(0, 0f),
            new PointF(0.8f, 0.8f), new PointF(0.3f, 0.6f), new PointF(0.8f, 0.8f), new PointF(0.6f, 0.3f)
                    },
                new[]
                    {
            (byte) PathPointType.Start, (byte) PathPointType.Line, (byte) PathPointType.Line,
            (byte) PathPointType.Line, (byte) PathPointType.Line, (byte) PathPointType.Line,
            (byte) PathPointType.Line, (byte) PathPointType.Line, (byte) PathPointType.Line
                    });
        }

            //BONE (MASTOID)
        public static void ViaBoneMLEUnmasked(ZedGraphControl zgc, double freq, double dB)
        {
            var curve = zgc.GraphPane.AddCurve(null, new[] { freq }, new[] { dB }, Color.Blue);
            curve.IsX2Axis = true;
            curve.Symbol = new Symbol(SymbolType.UserDefined, Color.Blue);
            curve.Symbol.Size = 15f;
            curve.Symbol.UserSymbol = new GraphicsPath(
                new[]
                    {
            new PointF(-0.5f, -0.5f), new PointF(0.5f, 0f), new PointF(-0.5f, 0.5f)
                    },
                new[]
                    {
            (byte)PathPointType.Start, (byte)PathPointType.Line, (byte)PathPointType.Line
                    });
        }

        public static void ViaBoneMLEMasked(ZedGraphControl zgc, double freq, double dB)
        {
            var curve = zgc.GraphPane.AddCurve(null, new[] { freq }, new[] { dB }, Color.Blue);
            curve.IsX2Axis = true;
            curve.Symbol = new Symbol(SymbolType.UserDefined, Color.Blue);
            curve.Symbol.Size = 15f;
            curve.Symbol.UserSymbol = new GraphicsPath(
                new[]
                    {
            new PointF(-0.25f, -0.5f), new PointF(0.25f, -0.5f), new PointF(0.25f, 0.5f), new PointF(-0.25f, 0.5f)
                    },
                new[]
                    {
            (byte)PathPointType.Start, (byte)PathPointType.Line, (byte)PathPointType.Line, (byte)PathPointType.Line
                    });
        }


        public static void ViaBoneMLEUnmaskedNoReply(ZedGraphControl zgc, double freq, double dB)
        {
            var curve = zgc.GraphPane.AddCurve(null, new[] { freq }, new[] { dB }, Color.Blue);
            curve.IsX2Axis = true;
            curve.Symbol = new Symbol(SymbolType.UserDefined, Color.Blue);
            curve.Symbol.Size = 15f;
            curve.Symbol.UserSymbol = new GraphicsPath(
                new[]
                    {
            new PointF(0.0f, -1f), new PointF(1f, -0.5f), new PointF(0f, 0f),
            new PointF(0.8f, 0.8f), new PointF(0.3f, 0.6f), new PointF(0.8f, 0.8f), new PointF(0.6f, 0.3f)
                    },
                new[]
                    {
            (byte)PathPointType.Start, (byte)PathPointType.Line, (byte)PathPointType.Line,
            (byte)PathPointType.Line, (byte)PathPointType.Line, (byte)PathPointType.Line,
            (byte)PathPointType.Line
                    });
        }

        public static void ViaBoneMLEMaskedNoReply(ZedGraphControl zgc, double freq, double dB)
        {
            var curve = zgc.GraphPane.AddCurve(null, new[] { freq }, new[] { dB }, Color.Blue);
            curve.IsX2Axis = true;
            curve.Symbol = new Symbol(SymbolType.UserDefined, Color.Blue);
            curve.Symbol.Size = 15f;
            curve.Symbol.UserSymbol = new GraphicsPath(
                new[]
                    {
            new PointF(-0.5f, -1f), new PointF(0f, -1f), new PointF(0f, 0f), new PointF(-0.5f, 0f), new PointF(-0f, 0f),
            new PointF(0.8f, 0.8f), new PointF(0.3f, 0.6f), new PointF(0.8f, 0.8f), new PointF(0.6f, 0.3f)
                    },
                new[]
                    {
            (byte)PathPointType.Start, (byte)PathPointType.Line, (byte)PathPointType.Line, (byte)PathPointType.Line,
            (byte)PathPointType.Line, (byte)PathPointType.Line, (byte)PathPointType.Line,
            (byte)PathPointType.Line, (byte)PathPointType.Line
                    });
        }


            //BONE (FOREHEAD)
        public static void ViaBoneFLEUnmasked(ZedGraphControl zgc, double freq, double dB)
        {
            var curve = zgc.GraphPane.AddCurve(null, new[] { freq }, new[] { dB }, Color.Blue);
            curve.IsX2Axis = true;
            curve.Symbol = new Symbol(SymbolType.UserDefined, Color.Blue);
            curve.Symbol.Size = 15f;
            curve.Symbol.UserSymbol = new GraphicsPath(
                new[]
                    {
            new PointF(-0.5f, -0.5f), new PointF(0f, 0.5f), new PointF(0.5f, -0.5f)
                    },
                new[]
                    {
            (byte) PathPointType.Start, (byte) PathPointType.Line, (byte) PathPointType.Line
                    });
        }

        public static void ViaBoneFLEUnmaskedNoReply(ZedGraphControl zgc, double freq, double dB)
        {
            var curve = zgc.GraphPane.AddCurve(null, new[] { freq }, new[] { dB }, Color.Blue);
            curve.IsX2Axis = true;
            curve.Symbol = new Symbol(SymbolType.UserDefined, Color.Blue);
            curve.Symbol.Size = 15f;
            curve.Symbol.UserSymbol = new GraphicsPath(
                new[]
                    {
            new PointF(-0.5f, -0.8f), new PointF(0f, 0f), new PointF(0.5f, -0.8f), new PointF(0f, 0f),
            new PointF(0f, 1f), new PointF(0.2f, 0.5f), new PointF(0f, 1f), new PointF(-0.2f, 0.5f)

                    },
                new[]
                    {
            (byte) PathPointType.Start, (byte) PathPointType.Line, (byte) PathPointType.Line, (byte) PathPointType.Line,
             (byte) PathPointType.Line, (byte) PathPointType.Line, (byte) PathPointType.Line, (byte) PathPointType.Line
                    });
        }
        public static void ViaBoneFLEMasked(ZedGraphControl zgc, double freq, double dB)
        {
            var curve = zgc.GraphPane.AddCurve(null, new[] { freq }, new[] { dB }, Color.Blue);
            curve.IsX2Axis = true;
            curve.Symbol = new Symbol(SymbolType.UserDefined, Color.Blue);
            curve.Symbol.Size = 15f;
            curve.Symbol.UserSymbol = new GraphicsPath(
                new[]
                    {
            new PointF(-0.25f, -0.5f), new PointF(0.25f, -0.5f), new PointF(0.25f, 0.5f)
                    },
                new[]
                    {
            (byte) PathPointType.Start, (byte) PathPointType.Line, (byte) PathPointType.Line
                    });
        }


        public static void ViaBoneFLEMaskedNoReply(ZedGraphControl zgc, double freq, double dB)
        {
            var curve = zgc.GraphPane.AddCurve(null, new[] { freq }, new[] { dB }, Color.Blue);
            curve.IsX2Axis = true;
            curve.Symbol = new Symbol(SymbolType.UserDefined, Color.Blue);
            curve.Symbol.Size = 15f;
            curve.Symbol.UserSymbol = new GraphicsPath(
                new[]
                    {
            new PointF(-0.5f, -1f), new PointF(0f, -1f), new PointF(0f, 0f),
            new PointF(0.8f, 0.8f), new PointF(0.3f, 0.6f), new PointF(0.8f, 0.8f), new PointF(0.6f, 0.3f)
                    },
                new[]
                    {
            (byte)PathPointType.Start, (byte)PathPointType.Line, (byte)PathPointType.Line,
            (byte)PathPointType.Line, (byte)PathPointType.Line, (byte)PathPointType.Line, (byte)PathPointType.Line
                    });
        }


            //FREE FIELD
        public static void FFLE(ZedGraphControl zgc, double freq, double dB)
        {
            var curve = zgc.GraphPane.AddCurve(null, new[] { freq }, new[] { dB }, Color.Blue);
            curve.IsX2Axis = true;
            curve.Symbol = new Symbol(SymbolType.UserDefined, Color.Blue);
            curve.Symbol.Size = 15f;
            curve.Symbol.UserSymbol = new GraphicsPath(
                new[]
                    {
            new PointF(-0.5f, -0.2f), new PointF(0.5f, -0.2f), new PointF(0.25f, -0.2f), new PointF(0.5f, -0.5f),
            new PointF(-0.5f, 0.5f), new PointF(-0.25f, 0.2f), new PointF(-0.5f, 0.2f), new PointF(0.5f, 0.2f)
                    },
                new[]
                    {
            (byte)PathPointType.Start, (byte)PathPointType.Line, (byte)PathPointType.Line, (byte)PathPointType.Line,
            (byte)PathPointType.Line, (byte)PathPointType.Line, (byte)PathPointType.Line, (byte)PathPointType.Line
                    });
        }


        public static void FFLENoReply(ZedGraphControl zgc, double freq, double dB)
        {
            var curve = zgc.GraphPane.AddCurve(null, new[] { freq }, new[] { dB }, Color.Blue);
            curve.IsX2Axis = true;
            curve.Symbol = new Symbol(SymbolType.UserDefined, Color.Blue);
            curve.Symbol.Size = 15f;
            curve.Symbol.UserSymbol = new GraphicsPath(
                new[]
                    {
            new PointF(-1f, -0.4f), new PointF(0f, -0.4f), new PointF(-0.25f, -0.4f), new PointF(0f, -0.7f),
            new PointF(-1f, 0.3f), new PointF(-0.75f, 0f), new PointF(-1f, 0f), new PointF(0f, 0f),
            new PointF(0.8f, 0.8f), new PointF(0.3f, 0.6f), new PointF(0.8f, 0.8f), new PointF(0.6f, 0.3f)

                    },
                new[]
                    {
            (byte)PathPointType.Start, (byte)PathPointType.Line, (byte)PathPointType.Line, (byte)PathPointType.Line,
            (byte)PathPointType.Line, (byte)PathPointType.Line, (byte)PathPointType.Line, (byte)PathPointType.Line,
            (byte)PathPointType.Line, (byte)PathPointType.Line, (byte)PathPointType.Line, (byte)PathPointType.Line
                    });
        }

        public static void FFLEUnspecifiedResponsePresence(ZedGraphControl zgc, double freq, double dB)
        {
            var curve = zgc.GraphPane.AddCurve(null, new[] { freq }, new[] { dB }, Color.Blue);
            curve.IsX2Axis = true;
            curve.Symbol = new Symbol(SymbolType.UserDefined, Color.Blue);
            curve.Symbol.Size = 8f;
            curve.Symbol.UserSymbol = new GraphicsPath();
            curve.Symbol.UserSymbol.AddArc(new Rectangle(0, 0, 1, 1), 130, -180);
            curve.Symbol.UserSymbol.AddArc(new Rectangle(0, -1, 1, 1), 130, 180);
        }

        public static void FFLEUnspecifiedResponseAusence(ZedGraphControl zgc, double freq, double dB)
        {
            var curve = zgc.GraphPane.AddCurve(null, new[] { freq }, new[] { dB }, Color.Blue);
            curve.IsX2Axis = true;
            curve.Symbol = new Symbol(SymbolType.UserDefined, Color.Blue);
            curve.Symbol.Size = 8f;
            curve.Symbol.UserSymbol = new GraphicsPath(
                new[]
                    {
           new PointF(0.4f, 1.0f), new PointF(0.4f, 2.2f), new PointF(0.7f, 1.6f), new PointF(0.4f, 2.2f), new PointF(0.1f, 1.6f)

                    },
                new[]
                    {
            (byte)PathPointType.Start, (byte)PathPointType.Line, (byte)PathPointType.Line, (byte)PathPointType.Line, (byte)PathPointType.Line
                    });
            curve.Symbol.UserSymbol.AddArc(new Rectangle(0, 0, 1, 1), 130, -180);
            curve.Symbol.UserSymbol.AddArc(new Rectangle(0, -1, 1, 1), 130, 180);
        }


        //Pass line object to dash line
        public static void DrawDashLine(Line line)
        {
            line.Style = DashStyle.Custom;
            line.Width = 1;
            line.DashOn = 5;
            line.DashOff = 10;
        }
    }
}
