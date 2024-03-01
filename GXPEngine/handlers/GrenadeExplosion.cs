using GXPEngine;

public class GrenadeExplosion : GameObject
{
    private int counter;
    private int frame;

    private AnimationSprite grenadeAnimationSprite;

    public GrenadeExplosion(float x, float y)
    {
        SetXY(x, y);
        grenadeAnimationSprite = new AnimationSprite("assets/sprites/UI/grenade explosion.png", 8, 6);
        AddChild(grenadeAnimationSprite);
    }

    public void Update()
    {
        if (counter > 1)
        {
            counter = 0;
            if (frame == grenadeAnimationSprite.frameCount)
            {
                LateDestroy();
                frame = 0;
            }
            grenadeAnimationSprite.SetFrame(frame);
            frame++;
        }
        counter++;
    }
}
