using GXPEngine;

public class LevelHandler
{

    public enum LevelMode
    {
        Menu,
        Level,
        EndMenu
    }

    public static LevelMode levelMode = LevelMode.Level;

    private MyGame game;

    public LevelHandler()
    {
        game = MyGame.GetGame();
    }

    public void Update()
    {
        if (levelMode == LevelMode.Menu)
        {

        }

        if (levelMode == LevelMode.Level)
        {

        }

        if (levelMode == LevelMode.EndMenu)
        {

        }
    }
}

