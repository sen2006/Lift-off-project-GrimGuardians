
using System;
using GXPEngine;

public class Points : EasyDraw
{
    int points;
    public Points(float startX, float startY, int width, int height, int points) : base(width, height, false)
    {
       this.points = points;
        x = startX;
        y = startY;
    }
    public void Update()
    {
        this.TextAlign(CenterMode.Center, CenterMode.Center);
        this.Text(" " + points, width / 2, height / 2);
        y--;
    }
}
