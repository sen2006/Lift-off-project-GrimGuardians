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

        backGround = new Sprite("assets/sprites/screen/Start screen.png");
        MyGame.SetRunning(false);

        moveAnimationSprite.SetOrigin(moveAnimationSprite.width / 2, moveAnimationSprite.height / 2);

        SetXY(backGround.width / 2, backGround.height - 200);
    }

    public override void Update()
    {
        if (!MyGame.GetGame().HasChild(backGround)) { MyGame.GetGame().AddChild(backGround); }
        base.Update();

        MyGame.GetGame().SetChildIndex(backGround, 1001);
        MyGame.GetGame().SetChildIndex(this, 1002);
    }

    public override void playAnimation()
    {
        this.enemyState = EnemyState.Move;
        base.playAnimation();
    }

    public override float Hit(float damage)
    {
        backGround.LateDestroy();
        this.LateDestroy();
        MyGame.SetRunning(true);
        return 0;
    }
}
