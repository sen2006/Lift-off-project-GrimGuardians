using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class AmmoTypeHandler
{
    public static AmmoTypeHandler BUCKSHOT = new AmmoTypeHandler(500);
    public static AmmoTypeHandler SLUG = new AmmoTypeHandler(5,2);

    public static void initializeClass()
    {
        return;
    }

    int spreadRadius;
    int damage;
    public AmmoTypeHandler(int spreadRadius, int damage = 1) 
    { 
        this.spreadRadius = spreadRadius;
        this.damage = damage;
    }

    public void fire(float x, float y)
    {
        Console.WriteLine("fire got called");
        MyGame game = MyGame.GetGame();
        EasyDraw damageZone = new EasyDraw(spreadRadius*2, spreadRadius*2);
        game.AddChild(damageZone);
        damageZone.SetOrigin(spreadRadius, spreadRadius);
        damageZone.SetXY(x, y);
        damageZone.Rect(0, 0, spreadRadius, spreadRadius);
        foreach (GameObject obj in damageZone.GetCollisions())
        {
            if (obj is Shootable hitObj)
            {
                hitObj.hit(damage);
            }
        }
        damageZone.LateDestroy();
    }
}

