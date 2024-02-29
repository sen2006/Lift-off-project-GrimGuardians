using GXPEngine;
using GXPEngine.Core;
using System;

public class PanoramaHandler : Sprite
{

    // TODO complete this class
    int panoramaSpeed = 10;
    public PanoramaHandler(String texture) : base(texture, false, false)
    {

    }

    void Update()
    {
        x -= panoramaSpeed * Time.deltaTime / 60f;
        if (panoramaSpeed>0 && MyGame.GetGame().width - x >= this.width)
        {
            panoramaSpeed = -(Math.Abs(panoramaSpeed));
        }

        //onsole.WriteLine(-x + ">=" + this.width + ((-x) >= this.width) + (panoramaSpeed < 0 && x <= 0));

        if (panoramaSpeed<0 && -x <= 0)
        {
            panoramaSpeed = Math.Abs(panoramaSpeed);
        }
    }

    public int getSpeed() { return panoramaSpeed; }
}
