using GXPEngine;
using GXPEngine.Core;
using System;

public class panoramaHandler : Sprite
{

    // TODO complete this class
    int panoramaSpeed = 5;
    public panoramaHandler(Texture2D texture) : base(texture, false)
    {

    }

    void Update()
    {
        x -= panoramaSpeed * Time.deltaTime / 60f;
        if (panoramaSpeed>0 && x-this.game.width >= this.width)
        {
            panoramaSpeed = -Math.Abs(panoramaSpeed);
        }

        if (panoramaSpeed > 0 && x - this.game.width >= this.width)
        {
            panoramaSpeed = Math.Abs(panoramaSpeed);
        }
    }
}
