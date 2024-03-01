using GXPEngine;
using GXPEngine.Managers;

public class DeathButton : Shootable
{
    Sprite backGround;
    public DeathButton(int startX = 0, int startY = 0, float speed=0, int health = 1, int enemyDamage = 0, float enemyAttackSpeed = 0, int points = 0, bool showHealthBar = false) : base(startX, startY, speed, health, enemyDamage, enemyAttackSpeed, points, showHealthBar)
    {
        moveAnimationSprite = new AnimationSprite("assets/sprites/screen/Return to menu button.png", 1, 1);
        AddChild(moveAnimationSprite);
        moveAnimationSprite.visible = false;

        backGround = new Sprite("assets/sprites/screen/End screen.png");
        MyGame.SetRunning(false);

        moveAnimationSprite.SetOrigin(moveAnimationSprite.width / 2, moveAnimationSprite.height / 2);

        SetXY(backGround.width / 2, backGround.height- 325);

        SetScaleXY(.2f);
    }

    public override void Update()
    {
        if (!MyGame.GetGame().HasChild(backGround)) { MyGame.GetGame().AddChild(backGround); }
        base.Update();

        MyGame.GetGame().SetChildIndex(backGround, 1001);
        MyGame.GetGame().SetChildIndex(this, 1002);
        if (MyGame.GetControlerHandler().GetCursor() != null) MyGame.GetControlerHandler().GetCursor().parent.SetChildIndex(MyGame.GetControlerHandler().GetCursor(), 1003);
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
        MyGame.GetGame().AddChild(new StartButton());
        return 0;
    }
}
