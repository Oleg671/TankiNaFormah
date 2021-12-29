using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Est__Probitie
{
    struct HeavyArmor : ITankArmor
    {
        public int strength => 70;
        public int weidth => 70;

        public string WayToImg => @"D:\unic\OOP\Est' Probitie\Est' Probitie\T3.jpg";
    }
    struct MiddleArmor : ITankArmor
    {
        public int strength => 45;
        public int weidth => 40;
        public string WayToImg => @"D:\unic\OOP\Est' Probitie\Est' Probitie\T2.jpg";
    }
    struct LigthArmor : ITankArmor
    {
        public int strength => 20;
        public int weidth => 20;
        public string WayToImg => @"D:\unic\OOP\Est' Probitie\Est' Probitie\T1.jpg";
    }



    struct LRBullet : IBullet
    {
        public int range => 400;
        public int damage => 40;
        public string name => "Длинная дистанция";
    }
    struct SRBullet : IBullet
    {
        public int range => 100;
        public int damage => 70;
        public string name => "Короткая дистанция";
    }
    struct MRBullet : IBullet
    {
        public int range => 200;
        public int damage => 55;
        public string name => "Средняя дистанция";
    }
}
