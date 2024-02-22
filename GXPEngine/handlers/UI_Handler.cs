using System.Drawing;
using TiledMapParser;

namespace GXPEngine
{
    public class UI_Handler : GameObject
    {
        Text textFont = new Text();
        EasyDraw textDrawer;
        EasyDraw playerHealthBarDrawer;
        Sprite grenadesImage;

        bool gameStarted = true;

        float playerBaseHealth;
        float playerCurrentHealth;

        int healthBarHeight = 50;
        int grenades = 0;

        public UI_Handler()
        {
            this.playerBaseHealth = 500;
            this.playerCurrentHealth = this.playerBaseHealth;

            this.grenadesImage = new Sprite("assets/debug/checkers.png");
            grenadesImage.SetXY(1200, 700);
            AddChild(grenadesImage);

            // TODO: figure out width and height later
            textDrawer = new EasyDraw(500, 300, false);
            textDrawer.SetXY(0, 0);
            textDrawer.alpha = 1.0f;
            AddChild(textDrawer);

            this.playerHealthBarDrawer = new EasyDraw((int)playerCurrentHealth, healthBarHeight);
            playerHealthBarDrawer.SetXY(25, 25);
            AddChild(playerHealthBarDrawer);
        }

        public void Update()
        {
            if (gameStarted)
            {
                renderPlayerHealthBar();
                renderGrenades();
            }
        }

        void renderPlayerHealthBar()
        {
            playerHealthBarDrawer.Fill(Color.Red);
            playerHealthBarDrawer.Rect(0, 0, playerCurrentHealth, healthBarHeight);
        }

        float xtest = 0;

        void renderGrenades()
        {
            //There is something HUGE thats blocking the text
            xtest += 1f;

            textDrawer.Text(" " + grenades + "/3", xtest, 200);
        }
    }
}
