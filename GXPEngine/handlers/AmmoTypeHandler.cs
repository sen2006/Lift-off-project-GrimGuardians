using GXPEngine;

public class AmmoTypeHandler
{
    public static AmmoTypeHandler BUCKSHOT = new AmmoTypeHandler(75);
    public static AmmoTypeHandler SLUG = new AmmoTypeHandler(5,2);
    public static AmmoTypeHandler DRAGONS_BREATH = new AmmoTypeHandler(40, 1, 1, 3);

    public static AmmoTypeHandler GRENADE = new AmmoTypeHandler(100, 100);


    int spreadRadius;
    int damage;
    int damagePerSec;
    int forSec;
    public AmmoTypeHandler(int spreadRadius, int damage = 1, int damagePerSec = 0, int forSec = 0) 
    { 
        this.spreadRadius = spreadRadius;
        this.damage = damage;
        this.damagePerSec = damagePerSec;
        this.forSec = forSec;
    }

    /// <summary>
    /// Fires the shell at a given position
    /// </summary>
    /// <param name="x">X position</param>
    /// <param name="y">Y position</param>
    public void fire(float x, float y)
    {
        //Console.WriteLine("fire got called");
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
                hitObj.setOvertimeDamage(damagePerSec, forSec);
            }
        }
        damageZone.LateDestroy();
    }
}

