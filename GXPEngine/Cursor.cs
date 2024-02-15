using System;
using System.Diagnostics.Eventing.Reader;
using GXPEngine;

public class Cursor : Sprite
{
    AmmoTypeHandler barrelOne;
    AmmoTypeHandler barrelTwo;

    bool isWasMousDown = false;

    int ammoIndex = 1;
    public Cursor() : base("assets/debug/square.png")
    {
        SetOrigin(width / 2, height / 2);

        barrelOne = AmmoTypeHandler.BUCKSHOT;
        barrelTwo = AmmoTypeHandler.BUCKSHOT;
    }


    void Update()
    {
        Reload();
        AmmoSwitch();
    }

    public void fire()
    {
        if (barrelOne != null)
        {
            barrelOne.fire(x, y);
            barrelOne = null;
        }
        else if (barrelTwo != null)
        {
            barrelTwo.fire(x, y);
            barrelTwo = null;
        }
    }

    public void Reload()
    {
        if (Input.GetKeyUp(Key.R))
        {
            switch(ammoIndex)
            {
                case 1:
                    barrelOne = AmmoTypeHandler.BUCKSHOT;
                    barrelTwo = AmmoTypeHandler.BUCKSHOT;
                    Console.WriteLine("You reloaded Buckshot!");
                    break;
                case 2:
                    barrelOne = AmmoTypeHandler.SLUG;
                    barrelTwo = AmmoTypeHandler.SLUG;
                    Console.WriteLine("You reloaded Slug!");
                    break;
                case 3:
                    barrelOne = AmmoTypeHandler.INCENDIARY;
                    barrelTwo = AmmoTypeHandler.INCENDIARY;
                    Console.WriteLine("You reloaded Incendiary!");
                    break;
                default:
                    break;
            }
        }
    }

    public void AmmoSwitch()
    {
        if (Input.GetKeyUp(Key.X))
        {
            ammoIndex++;
            Console.WriteLine("You switched to bullet: " + ammoIndex);

            if (ammoIndex > 2)
            {
                ammoIndex = 0;
            }
        }
    }
}
