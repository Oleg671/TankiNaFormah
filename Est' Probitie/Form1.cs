using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Est__Probitie
{
    public partial class Form1 : Form
    {
        DialogResult Result;
        Game game;
        Graphics graph;
        int choos1, choos2;
        ITankArmor[] arms;
        PictureBox[] players;
        Label[] heal, move, mag, bull;
        Label turn;
        public Form1()
        {
            InitializeComponent();
            heal = new Label[2];
            move = new Label[2];
            mag = new Label[2];
            bull = new Label[2];
            graph = CreateGraphics();
          
            arms = new ITankArmor[] { new LigthArmor(), new MiddleArmor(), new HeavyArmor() };
            for (int i = 0; i < 2; i++)
            {
                heal[i] = new Label();
                heal[i].Size = new Size(120, 20);
                heal[i].Location = new Point(5+800*i, 5);
                heal[i].Visible = false;
                Controls.Add(heal[i]);

                move[i] = new Label();
                move[i].Size = new Size(120, 20);
                move[i].Location = new Point(5 + 800 * i, 25);
                move[i].Visible = false;
                Controls.Add(move[i]);

                mag[i] = new Label();
                mag[i].Size = new Size(120, 20);
                mag[i].Location = new Point(5 + 800 * i, 50);
                mag[i].Visible = false;
                Controls.Add(mag[i]);

                bull[i] = new Label();
                bull[i].Size = new Size(160, 20);
                bull[i].Location = new Point(5 + 800 * i, 75);
                bull[i].Visible = false;
                Controls.Add(bull[i]);
            }
            turn = new Label();
            turn.Size = new Size(120, 40);
            turn.Location = new Point(440, 5);
            turn.Visible = false;
            turn.Text = "Ход игрока 1";
            Controls.Add(turn);
        }
        private void begin_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                choos1 = 0;
            }
            else if (radioButton2.Checked)
            {
                choos1 = 1;
            }
            else if (radioButton3.Checked)
            {
                choos1 = 2;
            }
            if (radioButton4.Checked)
            {
                choos2 = 0;
            }
            else if (radioButton5.Checked)
            {
                choos2 = 1;
            }
            else if (radioButton6.Checked)
            {
                choos2 = 2;
            }
            game = new Game(arms[choos1], arms[choos2]);
            Controls.Remove(groupBox1);
            Controls.Remove(groupBox2);
            Controls.Remove(begin);
            Controls.Remove(ChoArm);

            players = game.retPB();
            Controls.Add(players[0]);
            Controls.Add(players[1]);

            for (int i = 0; i < 2; i++)
            {
                heal[i].Visible = true;
                move[i].Visible = true;
                mag[i].Visible = true;
                bull[i].Visible = true;
            }
            turn.Visible = true;
            timer1.Start();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            game.Draw(e.KeyData,graph);
            players[0].Refresh();
            players[1].Refresh();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            turn.Text = "Ход игрока " + Convert.ToString(game.Active);
            heal[0].Text = "Здоровье: " + game.heal(0);
            heal[1].Text = "Здоровье: " + game.heal(1);
            move[0].Text = "Действий: " + Convert.ToString(game.Move[0]);
            move[1].Text = "Действий: " + Convert.ToString(game.Move[1]);
            mag[0].Text = "Боезапас: " + Convert.ToString(game.RetMag()[0]);
            mag[1].Text = "Боезапас: " + Convert.ToString(game.RetMag()[1]);
            bull[0].Text =  Convert.ToString(game.CurrBullType()[0]);
            bull[1].Text =  Convert.ToString(game.CurrBullType()[1]);
            if (game.ChWin())
            {
                timer1.Stop();
                if (Convert.ToInt32(game.heal(0)) <= 0)
                {
                    Result = MessageBox.Show("Победа игрока 2", "Конец", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    Result = MessageBox.Show("Победа игрока 1", "Конец", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                Controls.Clear();
                if (Result==DialogResult.OK)
                {
                    Close();
                }
            }
        }
    }
}
