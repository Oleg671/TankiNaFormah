using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Est__Probitie
{
    class Tank
    {
        ITankArmor armor;
        List<IBullet> bullets;
        int health, speed, indBul;
        Point position;
        int[] look;
        PictureBox img;
        IBullet currBull;
        int[,] rangeBullet;
        int rotat = 0;
        public int[] Look
        {
            get
            {
                return look;
            }
            private set
            {
                look = value;
            }
        }
        public PictureBox Img
        {
            get
            {
                return img;
            }
            private set
            {
                img = value;
            }
        }
        public Point Position
        {
            get 
            {
                return position;
            }
            private set
            {
                position = value;
            }
        }
        public int Health
        {
            get
            {
                return health;
            }
            private set
            {
                health = value;
            }
        }
        public Tank(ITankArmor arm, Point loc)
        {
            armor = arm;
            bullets = new List<IBullet>() {new SRBullet(), new SRBullet(), new MRBullet(), new MRBullet(), new MRBullet(), new LRBullet(), new LRBullet()};
            health = 100;
            speed = 100 - armor.weidth;
            position = loc;
            indBul = 0;
            Img = new PictureBox();
            img.Image = Image.FromFile(armor.WayToImg);
            img.SizeMode = PictureBoxSizeMode.StretchImage;
            img.Location = new Point(loc.X - 30, loc.Y - 30);
            img.Size = new Size(60, 60);
        }
        public void Move(int[] mm)
        {
            position.X += speed * mm[0];
            position.Y += speed * mm[1];
            look = mm;
            if ((mm[0] == -1 && rotat==3) || (mm[0] == 1 && rotat == 1) || (mm[1] == -1 && rotat == 2) || (mm[1] == 1 && rotat == 0))
            {
                img.Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                rotat = (rotat + 2) % 4;
            }
            else if ((mm[0] == -1 && rotat == 0)|| (mm[0] == 1 && rotat == 2) || (mm[1] == -1 && rotat == 3) || (mm[1] == 1 && rotat == 1))
            {
                img.Image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                rotat = (rotat + 1) % 4;
            }
            else if ((mm[0] == -1 && rotat == 2) || (mm[0] == 1 && rotat == 0) || (mm[1] == -1 && rotat == 1) || (mm[1] == 1 && rotat == 3))
            {
                img.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                rotat = (rotat + 3) % 4;
            }
            img.Location = new Point(position.X-30, position.Y-30);
        }
        public void GetHit(IBullet bull)
        {
            health -= bull.damage - Convert.ToInt32(armor.strength * 0.5);
        }
        public IBullet Shoot()
        {
            if (bullets.Count == 0)
            {
                Reload();
                return new SRBullet();
            }
            else
            {
                currBull = bullets[indBul];
                bullets.Remove(bullets[indBul]);
                if (indBul > bullets.Count - 1)
                {
                    indBul = 0;
                }
                return currBull;
            }
        }
        public void Reload()
        {
            bullets = new List<IBullet>() { new SRBullet(), new SRBullet(), new MRBullet(), new MRBullet(), new MRBullet(), new LRBullet(), new LRBullet() };
        }
        public void ChangeBullet()
        {
            if (indBul < bullets.Count)
            {
                indBul += 1;
            }
            else
            {
                indBul = 0;
            }
        }
        public int[,] RangeBull()
        {
            if (bullets.Count != 0)
            {
                rangeBullet = new int[2, 2];
                rangeBullet[0, 0] = Convert.ToInt32(position.X);
                rangeBullet[0, 1] = Convert.ToInt32(position.X) + (look[0] * bullets[indBul].range);
                rangeBullet[1, 0] = Convert.ToInt32(position.Y);
                rangeBullet[1, 1] = Convert.ToInt32(position.Y) + (look[1] * bullets[indBul].range);
                return rangeBullet;
            }
            else
            {
                return rangeBullet = new int[2, 2] { { 0, 0 }, { 0, 0 } };
            }
        }
        public int RetMag()
        {
            return bullets.Count;
        }
        public string CurrentBullType()
        {
            if (bullets.Count != 0)
            {
                return bullets[indBul].name;
            }
            else
            {
                return "Боезапас пуст";
            }
        }
    }
}
