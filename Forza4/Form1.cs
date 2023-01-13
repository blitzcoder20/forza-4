using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Forza4.Properties;

namespace Forza4
{
    public partial class Form1 : Form
    {
        int indiceSelezione = 0;
        string[,] celle = new string[6,7];
        bool chi = true;
        int indice;
       
        int contamosse=0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PianoGioco();
            PianoSelezione();

        }
        void PianoGioco()
        {
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    PictureBox pcb = new PictureBox();
                    pcb.SetBounds(j * 50 + 5, i * 50 + 5, 40, 40);
                    pcb.BackColor = Color.Transparent;
                    pcb.Image = Resources.Grigio;

                    pcb.SizeMode = PictureBoxSizeMode.StretchImage;
                    panel1.Controls.Add(pcb);
                    celle[i,j] = null;

                }
            }
            panel1.BackColor = Color.Blue;
        }
        void PianoSelezione()
        {
            for (int i = 0; i < 7; i++)
            {
                PictureBox pcb = new PictureBox();
                pcb.SetBounds(i * 50 + 5, 10, 40, 40);
                pcb.BackColor = Color.White;

                pcb.SizeMode = PictureBoxSizeMode.StretchImage;
                pcb.BackColor = Color.Transparent;
                panel2.Controls.Add(pcb);
            }

            DisegnaPiano();

        }

        private void Timer_Tick(object sender, EventArgs e)
        {


          


        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                if (indiceSelezione != 6)
                {
                    
                    indiceSelezione++;
                    DisegnaPiano();

                }
            }
            else if (e.KeyCode == Keys.Left)
            {
                if (indiceSelezione != 0)
                {
                    indiceSelezione--;
                    DisegnaPiano();
                }
            }
            else if (e.KeyCode == Keys.Enter)
            {
                DisegnaGioco();
                DisegnaPiano();

            }
        }
        void DisegnaPiano()
        {

            for (int i = 0; i < 7; i++)
            {
                if (i == indiceSelezione)
                {
                    PictureBox pic = panel2.Controls[indiceSelezione] as PictureBox;
                    if (chi)
                    {
                        pic.Image = Resources.rosso;
                    }
                    else
                    {
                        pic.Image = Resources.giallo;
                    }

                }
                else
                {
                    PictureBox pic = panel2.Controls[i] as PictureBox;
                    pic.Image = Resources.Grigio;
                }
            }
        }
        void DisegnaGioco()
        {
            string colore;
             indice = TrovaRiga();
            int indicePannello = indice * 7 + indiceSelezione;
            PictureBox pcb = panel1.Controls[indicePannello] as PictureBox;
            pcb.SizeMode = PictureBoxSizeMode.StretchImage;
            if (celle[0,indiceSelezione] != null)
            {
                return;
            }
            if (chi == true)
            {
                pcb.Image = Resources.rosso;
                colore = "rosso";
            }
            else
            {
                pcb.Image = Resources.giallo;
                colore = "giallo";
            }           
            chi = !chi;
            celle[indice,indiceSelezione] = colore;
            if (contamosse >= 6)
            {
                bool vinto = Vittoria(indice, indiceSelezione, colore);
                if (vinto)
                {
                    MessageBox.Show("ha vinto " + colore);
                    Resetta();
                }
                if (Riempito())
                {
                    MessageBox.Show("Nessuno ha vinto");
                    Resetta();
                }
            }
            indiceSelezione = 0;
            contamosse++;
           }
        bool Vittoria(int riga,int colonna,string colore)
        {
                    
            int conta = 0;
            //orizzontale
            for (int i = 0; i < celle.GetLength(1); i++)
            {
                if (celle[riga, i] == colore)
                {
                    conta++;
                }
                else conta = 0;
                if (conta >= 4)
                    return true;
            }
            //verticale
            conta = 0;
            for (int i = 0; i < celle.GetLength(0); i++)
            {
                if (celle[i, colonna] == colore)
                {
                    conta++;
                }
                else conta = 0;
                if (conta >= 4)
                    return true;
            }
            //obliquo sinistra
            conta = 0;
            for (int i = -4; i < 5; i++)
            {
               
                
                    if (riga+i>=0 && colonna+i>=0 && riga+i<=5 && colonna+i<=6 )
                    {
                        if (celle[riga + i, colonna + i] == colore)
                        {
                            conta++;
                        }
                        else conta = 0;
                        if (conta >= 4)
                            return true;
                    }
                }
            //obliquo destro
            conta = 0;
            for (int i = -4; i < 5; i++)
            {


               if (riga + i >= 0 && colonna - i >= 0 && riga +i <= 5 && colonna - i <= 6)
                {
                    if (celle[riga + i, colonna - i] == colore)
                    {
                        conta++;
                    }
                    else conta = 0;
                    if (conta >= 4)
                        return true;
                }
            }

            return false;
        }
        int TrovaRiga()
        {
          
            int riga = 0;
       
            for (int i = 0; i <celle.GetLength(0)-1; i ++)
            {

               if (celle[i+1, indiceSelezione] == null)
                {
                    riga++;

                }
                
            }

            return riga;
        }
        bool Riempito()
        {

            for(int i=0;i<celle.GetLength(0);i++)
            {
                for (int j = 0; j < celle.GetLength(1); j++)
                {
                    if (celle[i, j] == null)
                    {
                        return false;
                    }
                }

            }
            return true;
        }
       void Resetta()
        {
            for(int i = 0; i < panel1.Controls.Count; i++)
            {
                PictureBox pcb = panel1.Controls[i] as PictureBox;
                pcb.Image = Resources.Grigio;
            }
            for(int j = 0; j < celle.GetLength(0); j++)
            {
                for(int i = 0; i < celle.GetLength(1); i++)
                {
                    celle[j, i] = null;
                }
            }
        }
        
    }
}

    

