using GXPEngine;

public class Cursor : Sprite
{
    AmmoTypeHandler barrelOne;
    AmmoTypeHandler barrelTwo;

    bool isWasMousDown = false;
    public Cursor() : base("assets/debug/square.png")
    {
        SetOrigin(width / 2, height / 2);

        barrelOne = AmmoTypeHandler.BUCKSHOT;
        barrelTwo = AmmoTypeHandler.BUCKSHOT;
    }


    void Update()
    {
        
    }

    public void fire()
    {
        if (barrelOne != null)
        {
            barrelOne.fire(x, y); //fire the barrel
                                  //barrelOne = null; //unload the barrel
        }
        else if (barrelTwo != null)
        {
            barrelTwo.fire(x, y); //fire the barrel
                                  //barrelTwo = null; //unload the barrel
        }
    }
}
