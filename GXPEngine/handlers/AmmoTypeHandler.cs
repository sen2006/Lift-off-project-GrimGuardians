using GXPEngine;

public class AmmoTypeHandler
{
    public static AmmoTypeHandler BUCKSHOT = new AmmoTypeHandler(75,1);
    public static AmmoTypeHandler SLUG = new AmmoTypeHandler(5,3);
    public static AmmoTypeHandler DRAGONS_BREATH = new AmmoTypeHandler(40, .5f, .5f, 6);


    int spreadRadius;
    float damage;
    float damagePerSec;
    int forSec;
    public AmmoTypeHandler(int spreadRadius, float damage = 1, float damagePerSec = 0, int forSec = 0) 
    { 
        this.spreadRadius = spreadRadius;
        this.damage = damage;
        this.damagePerSec = damagePerSec;
        this.forSec = forSec;
    }

    /// <summary>
    /// Fires the shell atr a given position
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
        GameObject[] collisions = damageZone.GetCollisions();
        foreach (GameObject obj in collisions)
        {
            if (obj.visible && obj.parent is Shootable hitObj)
            {
                hitObj.hit(damage);
                hitObj.setOvertimeDamage(damagePerSec, forSec);
            }
        }
        damageZone.LateDestroy();
    }
}
