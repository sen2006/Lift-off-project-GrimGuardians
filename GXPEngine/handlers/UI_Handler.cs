using System.Drawing;
using GXPEngine;
using TiledMapParser;
public class UI_Handler : GameObject
{

    // Settings
    int healthWarWidth = 500;
    int healthBarHeight = 50;

    // variables
    Font fontTeko;
    EasyDraw textDrawer;

    Sprite playerHealthBarFrame;
    Sprite playerHealthBar;
    Sprite buckshotUI = new Sprite("assets/sprites/UI/buckshot.png");
    Sprite slugUI = new Sprite("assets/sprites/UI/Slug.png");
    Sprite dragonBreathIU = new Sprite("assets/sprites/UI/fire.png");
    Sprite buckshotUIHighlight = new Sprite("assets/sprites/UI/buckshot outline.png");
    Sprite slugUIHighlight = new Sprite("assets/sprites/UI/Slug outline.png");
    Sprite dragonBreathIUHighlight = new Sprite("assets/sprites/UI/fire outline.png");
    Sprite grenadesImage;

    bool gameStarted = true;
    public static float pointsMultiplier = 1.00f;
    float playerScore;
    

    public UI_Handler()
    {
        this.grenadesImage = new Sprite("assets/sprites/UI/grenade.png");
        grenadesImage.SetXY(1200, 660);
        grenadesImage.scale = 0.4f;
        AddChild(grenadesImage);

        fontTeko = new Font("assets/debug/teko.ttf", 15);

        textDrawer = new EasyDraw(1366, 768, false);
        textDrawer.alpha = 1.0f;
        textDrawer.scale = 1.2f;
        AddChild(textDrawer);

        playerHealthBar = new Sprite("assets/sprites/UI/HealthBar.png");

    }

    public void Update()
    {
        if (!MyGame.IsRunning()) return;
        if (MyGame.GetGame().HasChild(buckshotUI)) MyGame.GetGame().SetChildIndex(buckshotUI, 999);
        if (MyGame.GetGame().HasChild(slugUI)) MyGame.GetGame().SetChildIndex(slugUI, 999);
        if (MyGame.GetGame().HasChild(dragonBreathIU)) MyGame.GetGame().SetChildIndex(dragonBreathIU, 999);
        if (MyGame.GetGame().HasChild(buckshotUIHighlight)) MyGame.GetGame().SetChildIndex(buckshotUIHighlight, 999);
        if (MyGame.GetGame().HasChild(slugUIHighlight)) MyGame.GetGame().SetChildIndex(slugUIHighlight, 999);
        if (MyGame.GetGame().HasChild(dragonBreathIUHighlight)) MyGame.GetGame().SetChildIndex(dragonBreathIUHighlight, 999);
        if (MyGame.GetGame().HasChild(grenadesImage)) MyGame.GetGame().SetChildIndex(grenadesImage, 999);

        if (gameStarted)
        {
            renderGrenades();
            PlayerScore();
            AmmoSelect();
            renderPlayerHealthBar();
        }
    }

    void renderPlayerHealthBar()
    {
        this.playerHealthBarFrame = new Sprite("assets/sprites/UI/PhealthFrame.png");
        playerHealthBarFrame.scale = 0.25f;
        playerHealthBarFrame.SetXY(25, 60);
        textDrawer.Text("Health", 20, 50);
        AddChild(playerHealthBarFrame);

        playerHealthBar.scaleX = Mathf.Max(0f, PlayerHealthHandler.getHealth() / (PlayerHealthHandler.getMaxHealth() * 4));
        playerHealthBar.scaleY = 0.25f;
        playerHealthBar.SetXY(30, 65);
        AddChild(playerHealthBar);
    }

    void renderGrenades()
    {
        textDrawer.Clear(0, 0, 0, 0);
        textDrawer.Text(" " + GrenadeHandler.GetGrenades() + "/" + GrenadeHandler.GetMaxGrenades(), 1060, 600);
    }

    void PlayerScore()
    {
        textDrawer.Text("Score " + playerScore, 950, 70);
    }

    public void addPoints(int points)
    {
        this.playerScore += points * pointsMultiplier;
    }

    public void AmmoSelect()
    {
        if (!(MyGame.GetControlerHandler().isCalibrated() && MyGame.IsRunning())) return;
        int shellIndex = MyGame.GetControlerHandler().GetCursor().getAmmoIndex();
        if (shellIndex == 0)
        {
            buckshotUIHighlight.SetXY(10, 640);
            buckshotUIHighlight.scale = 0.4f;
            if (!MyGame.GetGame().HasChild(this.buckshotUIHighlight))
            {
                MyGame.GetGame().AddChild(buckshotUIHighlight);
            }
            if (MyGame.GetGame().HasChild(this.buckshotUI))
            {
                MyGame.GetGame().RemoveChild(buckshotUI);
            }
        }
        else
        {
            buckshotUI.SetXY(10, 640);
            buckshotUI.scale = 0.4f;
            if (!MyGame.GetGame().HasChild(this.buckshotUI))
            {
                MyGame.GetGame().AddChild(buckshotUI);
            }
            if (MyGame.GetGame().HasChild(this.buckshotUIHighlight))
            {
                MyGame.GetGame().RemoveChild(buckshotUIHighlight);
            }
        }

        if (shellIndex == 1)
        {
            slugUIHighlight.scale = 0.4f;
            slugUIHighlight.SetXY(110, 640);
            if (!MyGame.GetGame().HasChild(this.slugUIHighlight))
            {
                MyGame.GetGame().AddChild(slugUIHighlight);
            }
            if (MyGame.GetGame().HasChild(this.slugUI))
            {
                MyGame.GetGame().RemoveChild(slugUI);
            }
        }
        else
        {
            slugUI.scale = 0.4f;
            slugUI.SetXY(110, 640);
            if (!MyGame.GetGame().HasChild(this.slugUI))
            {
                MyGame.GetGame().AddChild(slugUI);
            }
            if (MyGame.GetGame().HasChild(this.slugUIHighlight))
            {
                MyGame.GetGame().RemoveChild(slugUIHighlight);
            }
        }

        if (shellIndex == 2)
        {
            dragonBreathIUHighlight.scale = 0.4f;
            dragonBreathIUHighlight.SetXY(200, 650);
            if (!MyGame.GetGame().HasChild(this.dragonBreathIUHighlight))
            {
                MyGame.GetGame().AddChild(dragonBreathIUHighlight);
            }
            if (MyGame.GetGame().HasChild(this.dragonBreathIU))
            {
                MyGame.GetGame().RemoveChild(dragonBreathIU);
            }
        }
        else
        {
            dragonBreathIU.scale = 0.4f;
            dragonBreathIU.SetXY(200, 650);
            if (!MyGame.GetGame().HasChild(this.dragonBreathIU))
            {
                MyGame.GetGame().AddChild(dragonBreathIU);
            }
            if (MyGame.GetGame().HasChild(this.dragonBreathIUHighlight))
            {
                MyGame.GetGame().RemoveChild(dragonBreathIUHighlight);
            }
        }
    }
}
