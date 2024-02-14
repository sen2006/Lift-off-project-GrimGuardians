
using System;
using GXPEngine;

public class Points : EasyDraw
{
    int points;

    float textTimer = 1;
    public Points(float startX, float startY, int width, int height, int points) : base(width, height, false)
    {
       this.points = points;
        x = startX;
        y = startY;
    }
    public void Update()
    {
        float actualDeltaTime = Time.deltaTime / 1000f;

        textTimer -= actualDeltaTime;
        alpha = Mathf.Max(0f, alpha - actualDeltaTime);

        this.TextAlign(CenterMode.Center, CenterMode.Center);
        this.Text(" " + points, width / 2, height / 2);

        textMovement();
        textFade();
    }
    public void textMovement()
    {
        y--;
    }

    public void textFade()
    {
        if (textTimer <= 0)
        {
            this.LateDestroy(); // temporary
        }

        
        
        
    }
}
