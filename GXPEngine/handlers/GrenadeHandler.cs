using GXPEngine;
using System;

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
        SoundHandler.grenade_exploding.play();
        MyGame game = MyGame.GetGame();
        EasyDraw damageZone = new EasyDraw(spreadRadius * 2, spreadRadius * 2);
        game.AddChild(damageZone);
        damageZone.SetOrigin(spreadRadius, spreadRadius);
        damageZone.SetXY(x, y);
        damageZone.Ellipse(0, 0, spreadRadius, spreadRadius);
        foreach (GameObject obj in damageZone.GetCollisions())
        {
            if (obj.visible && obj.parent is Shootable hitObj)
            {
                hitObj.Hit(damage);
            }
        }
        GrenadeExplosion explosion = new GrenadeExplosion(x - spreadRadius, y - spreadRadius);
        game.AddChild(explosion);
        grenadesLeft--;
        Console.WriteLine("Grenades left: " + grenadesLeft);
        damageZone.LateDestroy();
    }

    public static void addGrenade()
    {
        grenadesLeft = Math.Min(maxGrenades, grenadesLeft + 1);
    }

    internal static int GetGrenades()
    {
        return grenadesLeft;
    }

    internal static int GetMaxGrenades()
    {
        return maxGrenades;
    }
}
