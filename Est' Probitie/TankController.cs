using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Est__Probitie
{
    class TankController
    {
        Tank tank;
        public TankController(ITankArmor arm, Point loc)
        {
            tank = new Tank(arm, loc);
        }
        public PictureBox RPB()
        {
            return tank.Img;
        }
        public void Draw(int[] mm)
        {
            tank.Move(mm);
        }
        public void Hit(IBullet bullet)
        {
            tank.GetHit(bullet);
        }
        public IBullet Shoot()
        {
            return tank.Shoot();
        }
        public int[,] RangeBull()
        {
            return tank.RangeBull();
        }
        public void Reload()
        {
            tank.Reload();
        }
        public bool Alive()
        {
            if (tank.Health > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public Point RetLoc()
        {
            return tank.Position;
        }
        public string heal()
        {
            return Convert.ToString(tank.Health);
        }
        public int RetMag()
        {
            return tank.RetMag();
        }
        public string RetCurrentBullType()
        {
            return tank.CurrentBullType();
        }
    }
}
