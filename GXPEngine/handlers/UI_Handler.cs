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
    //EasyDraw playerHealthBarDrawer;

    Sprite playerHealthBarFrame;
    Sprite playerHealthBar;
    Sprite buckshotUI;
    Sprite slugUI;
    Sprite dragonBreathIU;
    Sprite grenadesImage;

    bool gameStarted = true;

    int playerScore;


    public UI_Handler()
    {
        this.grenadesImage = new Sprite("assets/debug/grenade.png");
        grenadesImage.SetXY(1200, 660);
        grenadesImage.scale = 0.4f;
        AddChild(grenadesImage);

        fontTeko = new Font("teko.ttf", 15);

        textDrawer = new EasyDraw(1366, 768, false);
        textDrawer.alpha = 1.0f;
        textDrawer.scale = 1.2f;
        AddChild(textDrawer);



        //this.playerHealthBarDrawer = new EasyDraw(healthWarWidth, healthBarHeight);
        //AddChild(playerHealthBarDrawer);
        playerHealthBar = new Sprite("assets/debug/HealthBar.png");

    }

    public void Update()
    {
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
        //playerHealthBarDrawer.Clear(0, 0, 0, 0);
        //playerHealthBarDrawer.Fill(Color.Red);
        //playerHealthBarDrawer.Rect(0, 0, PlayerHealthHandler.getHealth() / PlayerHealthHandler.getMaxHealth() * playerHealthBarDrawer.width, playerHealthBarDrawer.height);
        //playerHealthBarDrawer.SetXY(25, 60);
        //textDrawer.Text("Health", 20, 50);

        this.playerHealthBarFrame = new Sprite("assets/debug/PhealthFrame.png");
        playerHealthBarFrame.scale = 0.25f;
        playerHealthBarFrame.SetXY(25, 60);
        textDrawer.Text("Health", 20, 50);
        AddChild(playerHealthBarFrame);

        playerHealthBar.scaleX = PlayerHealthHandler.getHealth() / 400;
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
        this.playerScore += points;
    }

    public void AmmoSelect()
    {
        if (!MyGame.GetControlerHandler().isCalibrated()) return;
        int shellIndex = MyGame.GetControlerHandler().GetCursor().getAmmoIndex();
        if (shellIndex == 0)
        {
            this.buckshotUI = new Sprite("assets/debug/buckshot outline.png");
            buckshotUI.SetXY(10, 640);
            buckshotUI.scale = 0.4f;
            AddChild(buckshotUI);
        }
        else
        {
            this.buckshotUI = new Sprite("assets/debug/buckshot.png");
            buckshotUI.SetXY(10, 640);
            buckshotUI.scale = 0.4f;
            AddChild(buckshotUI);
        }

        if (shellIndex == 1)
        {
            this.slugUI = new Sprite("assets/debug/Slug outline.png");
            slugUI.scale = 0.4f;
            slugUI.SetXY(110, 640);
            AddChild(slugUI);
        }
        else
        {
            this.slugUI = new Sprite("assets/debug/Slug.png");
            slugUI.scale = 0.4f;
            slugUI.SetXY(110, 640);
            AddChild(slugUI);
        }

        if (shellIndex == 2)
        {
            this.dragonBreathIU = new Sprite("assets/debug/fire outline.png");
            dragonBreathIU.scale = 0.4f;
            dragonBreathIU.SetXY(200, 650);
            AddChild(dragonBreathIU);
        }
        else
        {
            this.dragonBreathIU = new Sprite("assets/debug/fire.png");
            dragonBreathIU.scale = 0.4f;
            dragonBreathIU.SetXY(200, 650);
            AddChild(dragonBreathIU);
        }
    }
}
