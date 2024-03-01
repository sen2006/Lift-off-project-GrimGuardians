using GXPEngine;

public class MyGame : Game
{
    static bool running = true;

    static MyGame game;
    /// <summary>
    /// Get the MyGame to add things as children
    /// </summary>
    /// <returns>The MyGame</returns>
    public static MyGame GetGame() { return game; }
    public static ControllerHandler GetControlerHandler() { return controlerHandler; }
    public static UI_Handler GetUI_Handler() { return uiHandler; }
    public static PanoramaHandler GetPanoramaHandler() { return panoramaHandler; }
    public static bool IsRunning() { return running; }
    public static void SetRunning(bool state) { running = state; }

    static ControllerHandler controlerHandler;
    static UI_Handler uiHandler;
    static EnemySpawnHandler enemySpawnHandler;
    static PanoramaHandler panoramaHandler;

    public MyGame() : base(1366, 768, false, true, -1, -1, false)
    {
        enemySpawnHandler = new EnemySpawnHandler();
        uiHandler = new UI_Handler();
        panoramaHandler = new PanoramaHandler("assets/sprites/background/street.png");
        StartButton startButton = new StartButton();
        SoundHandler.game_music.play();

        AddChild(enemySpawnHandler);
        AddChild(panoramaHandler);
        AddChild(startButton);
        AddChild(controlerHandler);
        AddChild(uiHandler);
    }

    void Update()
    {
        PlayerHealthHandler.checkHealth();
    }

    

    static void Main()
    {
        controlerHandler = new ControllerHandler(); // Initiate the controlerHandler
        game = new MyGame(); // create the Game
        game.Start(); // start the Game
    }
}