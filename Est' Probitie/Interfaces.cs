using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Est__Probitie
{
    interface ITankArmor
    {
        int strength { get; }
        int weidth { get; }
        string WayToImg { get; }
    }
    interface IBullet
    {
        int range { get; }
        int damage { get; }
        string name { get; }
    }
}
