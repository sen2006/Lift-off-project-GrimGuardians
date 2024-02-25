using System;
using GXPEngine;

public class Cursor : Sprite
{
    AmmoTypeHandler barrelOne;
    AmmoTypeHandler barrelTwo;
    AmmoTypeHandler throwGrenade;

    bool infiniteAmmo = true;

    int ammoIndex;
    public int killCount;
    public Cursor() : base("assets/debug/square.png")
    {
        SetOrigin(width / 2, height / 2);
        barrelOne = AmmoTypeHandler.BUCKSHOT;
        barrelTwo = AmmoTypeHandler.BUCKSHOT;
        throwGrenade = AmmoTypeHandler.GRENADE;
        ammoIndex = 0;
    }


    void Update()
    {
        GainGrenade();
    }

    public void fire()
    {
        if (barrelOne != null)
        {
            barrelOne.fire(x, y);
            if (!infiniteAmmo) barrelOne = null;
        }
        else if (barrelTwo != null)
        {
            barrelTwo.fire(x, y);
            if (!infiniteAmmo) barrelTwo = null;
        }
    }

    public void ThrowGrenade()
    {
        if (MyGame.uiHandler.grenades >= 1)
        {
            throwGrenade.fire(x, y);
            MyGame.uiHandler.grenades--;
            Console.WriteLine("Remaining grenades " + MyGame.uiHandler.grenades);
        }
    }

    public void ReloadOne()
    {
        switch (ammoIndex)
        {
            case 0:
                barrelOne = AmmoTypeHandler.BUCKSHOT;
                Console.WriteLine("You reloaded Buckshot!");
                break;
            case 1:
                barrelOne = AmmoTypeHandler.SLUG;
                Console.WriteLine("You reloaded Slug!");
                break;
            case 2:
                barrelOne = AmmoTypeHandler.DRAGONS_BREATH;
                Console.WriteLine("You reloaded Dragons Breath!");
                break;
            default:
                break;
        }
    }

    public void ReloadTwo()
    {
        switch (ammoIndex)
        {
            case 0:
                barrelTwo = AmmoTypeHandler.BUCKSHOT;
                Console.WriteLine("You reloaded Buckshot!");
                break;
            case 1:
                barrelTwo = AmmoTypeHandler.SLUG;
                Console.WriteLine("You reloaded Slug!");
                break;
            case 2:
                barrelTwo = AmmoTypeHandler.DRAGONS_BREATH;
                Console.WriteLine("You reloaded Dragons Breath!");
                break;
            default:
                break;
        }
    }

    public void AmmoSwitch()
    {
        ammoIndex++;

        if (ammoIndex > 2)
        {
            ammoIndex = 0;
        }
        Console.WriteLine("You switched to bullet: " + ammoIndex);
    }

    public void GainGrenade()
    {
        if (killCount >= 2)
        {
            MyGame.uiHandler.grenades++;
            killCount = 0;
            Console.WriteLine($"Grenades" + MyGame.uiHandler.grenades);
        }
    }
}