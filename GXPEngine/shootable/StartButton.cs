using GXPEngine;
using GXPEngine.Managers;

public class StartButton : Shootable
{
    Sprite backGround;
    public StartButton(int startX = 0, int startY = 0, float speed=0, int health = 1, int enemyDamage = 0, float enemyAttackSpeed = 0, int points = 0, bool showHealthBar = false) : base(startX, startY, speed, health, enemyDamage, enemyAttackSpeed, points, showHealthBar)
    {
        moveAnimationSprite = new AnimationSprite("assets/sprites/screen/Shoot to start button.png", 1, 1);
        AddChild(moveAnimationSprite);
        moveAnimationSprite.visible = false;

        //backGround = new Sprite("assets/sprites/screen/Menu title screen.psd");
        //MyGame.GetGame().AddChild(backGround);
        MyGame.SetRunning(false);

        moveAnimationSprite.SetOrigin(moveAnimationSprite.width / 2, moveAnimationSprite.height / 2);

        //SetXY(backGround.width / 2, backGround.width / 2);
    }

    public override void Update()
    {
        base.Update();
    }

    public override void playAnimation()
    {
        this.enemyState = EnemyState.Move;
        base.playAnimation();
    }

    public override float Hit(float damage)
    {
        //backGround.LateDestroy();
        this.LateDestroy();
        MyGame.SetRunning(true);
        return 0;
    }
}
