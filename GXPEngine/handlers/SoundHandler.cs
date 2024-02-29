using GXPEngine;
public class SoundHandler
{
    

    public static SoundHandler TESTSOUND = new SoundHandler("", 1, 0);
    public static SoundHandler shooting_sound = new SoundHandler("assets/sound/Shotgun firing.wav", 1, 0);
    public static SoundHandler grenade_exploding = new SoundHandler("assets/sound/Grenade exploding.wav", 2, 0);
    public static SoundHandler reloading = new SoundHandler("assets/sound/Shell loading.wav", 1, 0);
    public static SoundHandler boss_sound = new SoundHandler("assets/sound/Boss Monster sound.mp3", 1, 0);
    public static SoundHandler damage_taken = new SoundHandler("assets/sound/Taking damage (maybe).wav", 1, 0);
    public static SoundHandler shell_switch = new SoundHandler("assets/sound/Shell switch.wav", 1, 0);
    public static SoundHandler large_sound = new SoundHandler("assets/sound/Strong enemy sound.wav", 1, 0);
    public static SoundHandler medium_sound = new SoundHandler("assets/sound/Medium enemy sound.wav", 1, 0);
    /*public static SoundHandler shooting_sound = new SoundHandler("assets/sound/Shotgun firing.wav", 1, 0);
    public static SoundHandler shooting_sound = new SoundHandler("assets/sound/Shotgun firing.wav", 1, 0);
    public static SoundHandler shooting_sound = new SoundHandler("assets/sound/Shotgun firing.wav", 1, 0);
    public static SoundHandler shooting_sound = new SoundHandler("assets/sound/Shotgun firing.wav", 1, 0);
    public static SoundHandler shooting_sound = new SoundHandler("assets/sound/Shotgun firing.wav", 1, 0);*/
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
