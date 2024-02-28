using GXPEngine;

public class MyGame : Game
{
    static MyGame game;
    /// <summary>
    /// Get the MyGame to add things as children
    /// </summary>
    /// <returns>The MyGame</returns>
    public static MyGame GetGame() { return game; }
    public static ControllerHandler GetControlerHandler() { return controlerHandler; }
    public static UI_Handler GetUI_Handler() { return uiHandler; }

    static ControllerHandler controlerHandler;
    static EnemySpawnHandler enemySpawnHandler;
    static UI_Handler uiHandler;

    public MyGame() : base(1366, 768, false, true, -1, -1, false)
    {
        uiHandler = new UI_Handler();
        enemySpawnHandler = new EnemySpawnHandler();
        PanoramaHandler StreetPanorama = new PanoramaHandler("assets/sprites/background/street.png");
        AddChild(enemySpawnHandler);
        AddChild(controlerHandler);
        AddChild(StreetPanorama);
        AddChild(uiHandler);
    }

    void Update()
    {

    }

    

    static void Main()
    {
        controlerHandler = new ControllerHandler(); // Initiate the controlerHandler
        game = new MyGame(); // create the Game
        game.Start(); // start the Game
    }
}