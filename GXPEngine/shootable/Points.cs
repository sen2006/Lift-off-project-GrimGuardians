
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
        float DeltaTimeS = Time.deltaTime / 1000f;

        textTimer -= DeltaTimeS;
        alpha = Mathf.Max(0f, alpha - DeltaTimeS);

        this.TextAlign(CenterMode.Center, CenterMode.Center);
        this.Text(" " + points, width / 2, height / 2);

        textMovement();
        textFade();
    }
    public void textMovement()
    {
        y--; // TODO make this Deltatime based
    }

    public void textFade()
    {
        // TODO make text fade here

        // destroy text once timer is up
        if (textTimer <= 0)
        {
            this.LateDestroy();
        }
    }
}
