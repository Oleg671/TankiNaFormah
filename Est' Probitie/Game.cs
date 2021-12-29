using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Est__Probitie
{
    class Game
    {

        int active;
        int[] move;
        int[,] kostil;
        Brush brush; 
        public int Active
        {
            get
            {
                return active;
            }
            private set
            {
                active = value;
            }
        }
        public int[] Move
        {
            get
            {
                return move;
            }
            private set
            {
                move = value;
            }
        }
        List<TankController> players;
        public Game(ITankArmor arm1, ITankArmor arm2)
        {
            active = 1;
            brush = new SolidBrush(Color.Red);
            move = new int[] { 5, 5 };
            players = new List<TankController>() { new TankController(arm1, new Point(500, 200)), new TankController(arm2, new Point(500, 810)) };
        }
        public PictureBox[] retPB()
        {
            return new PictureBox[2] { players[0].RPB(), players[1].RPB() };
        }
        public void Draw(Keys key, Graphics graph)
        {
            if (active == 1)
            {
                if (move[0] > 0)
                {
                    if (key == Keys.W && players[0].RetLoc().Y>=190)
                    {
                        players[0].Draw(new int[2] { 0, -1 });
                        move[0] -= 1;
                    }
                    else if (key == Keys.A && players[0].RetLoc().X >= 110)
                    {
                        players[0].Draw(new int[2] { -1, 0 });
                        move[0] -= 1;
                    }
                    else if (key == Keys.S && players[0].RetLoc().Y <= 890)
                    {
                        players[0].Draw(new int[2] { 0, 1 });
                        move[0] -= 1;
                    }
                    else if (key == Keys.D && players[0].RetLoc().X <= 890)
                    {
                        players[0].Draw(new int[2] { 1, 0 });
                        move[0] -= 1;
                    }
                    else if (key == Keys.E)
                    {
                        kostil = players[0].RangeBull();
                        if ((players[1].RetLoc().X < Math.Max(kostil[0, 0], kostil[0, 1]) && players[1].RetLoc().X > Math.Min(kostil[0, 0], kostil[0, 1]) &&
                            (players[1].RetLoc().Y <= players[0].RetLoc().Y + 30) && (players[1].RetLoc().Y >= players[0].RetLoc().Y - 30)))
                        {
                            ///graph.FillRectangle(brush, kostil[0, 0], kostil[1, 0], Math.Abs(kostil[0, 1] - kostil[0, 0]), Math.Abs(kostil[1, 1] - kostil[1, 0]));
                            players[1].Hit(players[0].Shoot());
                            move[0] -= 1;
                        }
                        else if ((players[1].RetLoc().Y < Math.Max(kostil[1, 0], kostil[1, 1]) && players[1].RetLoc().Y > Math.Min(kostil[1, 0], kostil[1, 1]) && //////////////111111112432543gtrg45t
                            (players[1].RetLoc().X <= players[0].RetLoc().X + 30) && (players[1].RetLoc().X >= players[0].RetLoc().X - 30)))
                        {
                            ///graph.FillRectangle(brush, kostil[0, 0], kostil[1, 0], Math.Abs(kostil[0, 1] - kostil[0, 0]), Math.Abs(kostil[1, 1] - kostil[1, 0]));
                            players[1].Hit(players[0].Shoot());
                            move[0] -= 1;
                        }
                        else 
                        {
                            players[0].Shoot();
                            move[0] -= 1;
                        }
                    }
                    else if (key == Keys.Q)
                    {
                        players[0].Reload();
                        move[0] -= 1;
                    }
                }
                else
                {
                    NextTurn();
                }
            }
            else
            {
                if (move[1] > 0)
                {
                    if (key == Keys.Up && players[1].RetLoc().Y >= 190)
                    {
                        players[1].Draw(new int[2] { 0, -1 });
                        move[1] -= 1;
                    }
                    else if (key == Keys.Left && players[1].RetLoc().X >= 110)
                    {
                        players[1].Draw(new int[2] { -1, 0 });
                        move[1] -= 1;
                    }
                    else if (key == Keys.Down && players[1].RetLoc().Y <= 890)
                    {
                        players[1].Draw(new int[2] { 0, 1 });
                        move[1] -= 1;
                    }
                    else if (key == Keys.Right && players[1].RetLoc().X <= 890)
                    {
                        players[1].Draw(new int[2] { 1, 0 });
                        move[1] -= 1;
                    }
                    else if (key == Keys.L)
                    {
                        if ((players[0].RetLoc().X < Math.Max(players[1].RangeBull()[0, 0], players[1].RangeBull()[0, 1]) && players[0].RetLoc().X > Math.Min(players[1].RangeBull()[0, 0], players[1].RangeBull()[0, 1]) &&
                            (players[0].RetLoc().Y <= players[1].RetLoc().Y + 30) && (players[0].RetLoc().Y >= players[1].RetLoc().Y - 30)))
                        {
                            players[0].Hit(players[1].Shoot());
                            move[1] -= 1;
                        }
                        else if ((players[0].RetLoc().Y < Math.Max(players[1].RangeBull()[1, 0], players[1].RangeBull()[1, 1]) && players[0].RetLoc().Y > Math.Min(players[1].RangeBull()[1, 0], players[1].RangeBull()[1, 1]) &&
                            (players[0].RetLoc().X <= players[1].RetLoc().X + 30) && (players[0].RetLoc().X >= players[1].RetLoc().X - 30)))
                        {
                            players[0].Hit(players[1].Shoot());
                            move[1] -= 1;
                        }
                        else
                        {
                            players[1].Shoot();
                            move[1] -= 1;
                        }
                    }
                    else if (key == Keys.K)
                    {
                        players[1].Reload();
                        move[1] -= 1;
                    }
                }
                else
                {
                    NextTurn();
                }
            }
        }
        public void NextTurn()
        {
            if (active == 1)
            {
                active = 2;
                move[1] = 5;
            }
            else
            {
                active = 1;
                move[0] = 5;
            }
        }
        public bool ChWin()
        {
            if (players[0].Alive() == false)
            {
                return true;
            }
            else if (players[1].Alive() == false)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public string heal(int ind)
        {
            return players[ind].heal();
        }
        public int[] RetMag()
        {
            return new int[2] { players[0].RetMag(), players[1].RetMag() };
        }
        public string[] CurrBullType()
        {
            return new string[2] { players[0].RetCurrentBullType(), players[1].RetCurrentBullType() };
        }
    }
}