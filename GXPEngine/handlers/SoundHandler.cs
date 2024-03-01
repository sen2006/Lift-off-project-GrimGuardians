using GXPEngine;
public class SoundHandler
{
    

    public static SoundHandler shooting_sound = new SoundHandler("assets/sound/Shotgun firing.wav", 0.4f, 0);
    public static SoundHandler grenade_exploding = new SoundHandler("assets/sound/Grenade exploding.wav", 1, 0);
    public static SoundHandler reloading = new SoundHandler("assets/sound/Shell loading.wav", 1, 0);
    public static SoundHandler boss_sound = new SoundHandler("assets/sound/Boss Monster sound.mp3", 1, 0);
    public static SoundHandler damage_taken = new SoundHandler("assets/sound/Taking damage (maybe).mp3", 1, 0);
    public static SoundHandler shell_switch = new SoundHandler("assets/sound/Shell switch.wav", 1, 0);
    public static SoundHandler large_sound = new SoundHandler("assets/sound/Strong enemy sound.wav", 1, 0);
    public static SoundHandler medium_sound = new SoundHandler("assets/sound/Medium enemy sound.wav", 1, 0);
    public static SoundHandler small_sound = new SoundHandler("assets/sound/Small enemy sound.wav", 1, 0);
    public static SoundHandler small_death = new SoundHandler("assets/sound/Small enemy death.mp3", 1, 0);
    public static SoundHandler medium_death = new SoundHandler("assets/sound/Medium enemy death.wav", 1, 0);
    public static SoundHandler large_death = new SoundHandler("assets/sound/Strong enemy death.wav", 1, 0);
    public static SoundHandler boss_death = new SoundHandler("assets/sound/Boss enemy death.wav", 1, 0);
    public static SoundHandler player_death = new SoundHandler("assets/sound/Player death.wav", 1, 0);
    public static SoundHandler barrel_exploding = new SoundHandler("assets/sound/Explosive Barrel.mp3", 1, 0);
    public static SoundHandler start_game = new SoundHandler("assets/sound/Game Start.wav", 1, 0);
    public static SoundHandler game_music = new SoundHandler("assets/sound/In-Game ambient music.mp3", 0.9f, 0);
    public static SoundHandler main_menu_music = new SoundHandler("assets/sound/Main Menu music.mp3", 1, 0);


    Sound storedSound;
    float defaultVolume;
    uint defaultChanel;

    public SoundHandler(string fileName, float defaultVolume, uint defaultChanel)
    {
        storedSound = new Sound(fileName);
        this.defaultVolume = defaultVolume;
        this.defaultChanel = defaultChanel;
    }

    public void play()
    {
        play(defaultVolume);
    }

    public void play(float volume)
    {
        play(volume, defaultChanel);
    }

    public void play(float volume, uint chanel)
    {
        storedSound.Play(false, chanel, volume, 0);
    }
}
