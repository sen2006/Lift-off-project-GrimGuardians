using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public static class GrenadeHandler
{
    // settings
    static int spreadRadius = 100;
    static int damage = 10000;

    // variables
    static int grenadesLeft = 3;
    static int maxGrenades = 3;

    public static void throwGrenade(float x, float y)
    {
        if (grenadesLeft <= 0) { return; }
        //Console.WriteLine("a grenade got thrown")
        MyGame game = MyGame.GetGame();
        EasyDraw damageZone = new EasyDraw(spreadRadius * 2, spreadRadius * 2);
        game.AddChild(damageZone);
        damageZone.SetOrigin(spreadRadius, spreadRadius);
        damageZone.SetXY(x, y);
        damageZone.Ellipse(0, 0, spreadRadius, spreadRadius);
        foreach (GameObject obj in damageZone.GetCollisions())
        {
            if (obj is Shootable hitObj)
            {
                hitObj.hit(damage);
            }
        }
        grenadesLeft--;
        damageZone.LateDestroy();
    }

    public static void addGrenade() { grenadesLeft = Math.Min(maxGrenades, grenadesLeft+1); }

    internal static int GetGrenades()
    {
        return grenadesLeft;
    }

    internal static int GetMaxGrenades()
    {
        return maxGrenades;
    }
}
