using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class AmmoTypeHandler
{
    public static AmmoTypeHandler BUCKSHOT = new AmmoTypeHandler(50, 1);
    public static AmmoTypeHandler SLUG = new AmmoTypeHandler(5,2);
    public static AmmoTypeHandler INCENDIARY = new AmmoTypeHandler(25, 1);


    int spreadRadius;
    int damage;
    int damageOverTime;
    public AmmoTypeHandler(int spreadRadius, int damage = 1) 
    { 
        this.spreadRadius = spreadRadius;
        this.damage = damage;
    }

    /// <summary>
    /// Fires the shell at a given position
    /// </summary>
    /// <param name="x">X position</param>
    /// <param name="y">Y position</param>
    public void fire(float x, float y)
    {
        Console.WriteLine("fire got called");
        MyGame game = MyGame.GetGame();
        EasyDraw damageZone = new EasyDraw(spreadRadius*2, spreadRadius*2);
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
        damageZone.LateDestroy();
    }
}

