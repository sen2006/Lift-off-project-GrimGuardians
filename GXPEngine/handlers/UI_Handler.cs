using System.Drawing;
using GXPEngine;
using TiledMapParser;
public class UI_Handler : GameObject
{
    Text textFont = new Text();
    EasyDraw textDrawer;
    EasyDraw playerHealthBarDrawer;

    EasyDraw buckshotUI;
    EasyDraw slugUI;
    EasyDraw dragonBreathIU;
    Sprite grenadesImage;

    private bool gameStarted = true;

    public float playerBaseHealth;
    public float playerCurrentHealth;

    public int grenades = 0;
    public int playerScore;
    public int shellIndex = 0;
    private int multiplier = 1;
    private int healthBarHeight = 50;


    public UI_Handler()
    {
        this.playerBaseHealth = 100;
        this.playerCurrentHealth = this.playerBaseHealth;

        this.grenadesImage = new Sprite("assets/debug/checkers.png");
        grenadesImage.SetXY(1200, 700);
        AddChild(grenadesImage);

        // TODO: figure out width and height later
        textDrawer = new EasyDraw(1366, 768, false);
        textDrawer.alpha = 1.0f;
        AddChild(textDrawer);

        this.playerHealthBarDrawer = new EasyDraw((int)playerCurrentHealth * 5, healthBarHeight);
        AddChild(playerHealthBarDrawer);

        this.buckshotUI = new EasyDraw(75, 75);
        AddChild(buckshotUI);

        this.slugUI = new EasyDraw(75, 75);
        AddChild(slugUI);

        this.dragonBreathIU = new EasyDraw(75, 75);
        AddChild(dragonBreathIU);
    }

    public void Update()
    {
        if (gameStarted)
        {
            renderPlayerHealthBar();
            renderGrenades();
            PlayerScore();
            AmmoSelect();
        }
    }

    void renderPlayerHealthBar()
    {
        playerHealthBarDrawer.Clear(0, 0, 0, 0);
        playerHealthBarDrawer.Fill(Color.Red);
        playerHealthBarDrawer.Rect(0, 0, playerCurrentHealth * 5, healthBarHeight);
        playerHealthBarDrawer.SetXY(25, 50);
    }

    void renderGrenades()
    {
        textDrawer.Clear(0, 0, 0, 0);
        textDrawer.Text(" " + grenades + "/3", 1280, 735);
    }

    void PlayerScore()
    {
        textDrawer.Text("Score " + playerScore, 1220, 75);
    }

    public void addPoints(int points)
    {
        this.playerScore += points;
    }

    public void AmmoSelect()
    {
        if (shellIndex == 0)
        {
            buckshotUI.Fill(Color.Red);
        }
        else
        {
            buckshotUI.Fill(Color.Green);
        }
        buckshotUI.Rect(0, 0, 75, 75);
        buckshotUI.SetXY(25, 710);

        if (shellIndex == 1)
        {
            slugUI.Fill(Color.Red);
        }
        else
        {
            slugUI.Fill(Color.Green);
        }
        slugUI.Rect(0, 0, 75, 75);
        slugUI.SetXY(75, 710);

        if (shellIndex == 2)
        {
            dragonBreathIU.Fill(Color.Red);
        }
        else
        {
            dragonBreathIU.Fill(Color.Green);
        }
        dragonBreathIU.Rect(0, 0, 75, 75);
        dragonBreathIU.SetXY(125, 710);

        if (shellIndex >= 3)
        {
            shellIndex = 0;
        }
    }
}
