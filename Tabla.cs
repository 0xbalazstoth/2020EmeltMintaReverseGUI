using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace _2020EmeltReverseGUI
{
    class Cellak
    { 
        public char Jatekos { get; set; }
        public int Sor { get; set; }
        public int Oszlop { get; set; }
        public string SzinIr { get; set; }

        public Cellak(char jatekos, int sor, int oszlop)
        {
            this.Jatekos = jatekos;
            this.Sor = sor;
            this.Oszlop = oszlop;
        }

        public SolidColorBrush Szinezes()
        {
            SolidColorBrush szin = Brushes.Gray;
            if (Jatekos == 'F')
                szin = Brushes.White;
            else if (Jatekos == 'K')
                szin = Brushes.Blue;
            return szin;
        }

        public string AdottSzin()
        {
            SzinIr = "Szürke";

            if (Jatekos == 'F')
                SzinIr = "FEHÉR";
            else if (Jatekos == 'K')
                SzinIr = "KÉK";
            return SzinIr;
        }
    }
    class Tabla
    {
        public Canvas Can;
        public MainWindow Mw;
        public Cellak ValasztottCella;
        public char[,] Allas;

        public Tabla(string fajl, MainWindow mw, Canvas can)
        {
            this.Can = can;
            this.Mw = mw;

            Allas = new char[8, 8];

            string[] forras = File.ReadAllLines(fajl);
            for (int sor = 0; sor < 8; sor++)
            {
                for (int oszlop = 0; oszlop < 8; oszlop++)
                {
                    Allas[sor, oszlop] = forras[sor][oszlop];
                }
            }
        }

        public void Megjelenit()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int k = 0; k < 8; k++)
                {
                    Ellipse e = new Ellipse();
                    Cellak cella = new Cellak(Allas[i, k], i, k);
                    e.Tag = cella;
                    e.Width = 35;
                    e.Height = 35;
                    e.Fill = cella.Szinezes();
                    e.Stroke = cella.Szinezes();
                    e.MouseDown += E_MouseDown;
                    e.Margin = new Thickness(27 + k * 40, 20 + i * 40, 0, 0);
                    Can.Children.Add(e);
                }
            }
        }

        private void E_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Ellipse ell = sender as Ellipse;
            Cellak cella = (Cellak)ell.Tag;
            if (cella.Jatekos == 'K' || cella.Jatekos == 'F')
            {
                Mw.Title = $"Reversi - {cella.AdottSzin()}";
                ValasztottCella = cella;
            }
            else
            {
                if (ValasztottCella != null)
                {
                    
                    if (SzabalyosLepes(ValasztottCella.Jatekos, cella.Sor, cella.Oszlop))
                    {
                        Mw.Title = $"Reversi - {ValasztottCella.AdottSzin()} > SZABÁLYOS";
                    }
                    else
                    {
                        Mw.Title = $"Reversi - {ValasztottCella.AdottSzin()} > SZABÁLYTALAN";
                    }
                }
            }
        }

        public bool VanForditas(char jatekos, int sor, int oszlop, int iranySor, int iranyOszlop)
        {
            int aktSor = 0;
            int aktOszlop = 0;
            char ellenfel;
            bool nincsEllenfel = false;
            aktSor += sor + iranySor;
            aktOszlop = oszlop + iranyOszlop;
            ellenfel = 'K';

            if (jatekos == 'K')
                ellenfel = 'F';

            nincsEllenfel = true;

            while (aktSor > 0 && aktSor < 8 && aktOszlop > 0 && aktOszlop < 8 && Allas[aktSor, aktOszlop] == ellenfel)
            {
                aktSor = aktSor + iranySor;
                aktOszlop = aktOszlop + iranyOszlop;
                nincsEllenfel = false;
            }

            if (nincsEllenfel || aktSor < 0 || aktSor > 7 || aktOszlop < 0 || aktOszlop > 7 || Allas[aktSor, aktOszlop] != jatekos)
            {
                return false;
            }
            return true;
        }

        public bool SzabalyosLepes(char jatekos, int sor, int oszlop)
        {
            return Allas[sor, oszlop] == '#' && (VanForditas(jatekos, sor, oszlop, -1, -1) || VanForditas(jatekos, sor, oszlop, -1, 0) || VanForditas(jatekos, sor, oszlop, -1, 1) || VanForditas(jatekos, sor, oszlop, 0, -1) || VanForditas(jatekos, sor, oszlop, 0, 1) || VanForditas(jatekos, sor, oszlop, 1, -1) || VanForditas(jatekos, sor, oszlop, 1, 0) || VanForditas(jatekos, sor, oszlop, 1, 1));
        }
    }
}
