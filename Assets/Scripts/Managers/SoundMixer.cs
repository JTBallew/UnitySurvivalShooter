using UnityEngine;

public class SoundMixer : MonoBehaviour
{
    public static SoundMixer Instance;

    public AudioSource titleMusic;
    public AudioSource bgMusic;
    public AudioSource playerAudio;
    public AudioSource gunAudio;
    public AudioSource sprintAudio;
    public AudioSource damageAudio;
    public AudioSource staminaAudio;
    public AudioSource shieldEquip;
    public AudioSource ammoAudio;
    public AudioSource healAudio;
    public AudioSource shieldBreak;

    void Awake()
    {
        Instance = this;
    }

    public void MusicChange()
    {
        titleMusic.volume = 0.25f * SoundManager.Instance.musicVolume;
        bgMusic.volume = 0.1f * SoundManager.Instance.musicVolume;
    }

    public void sfxChange()
    {
        playerAudio.volume = 1 * SoundManager.Instance.sfxVolume;
        gunAudio.volume = 0.5f * SoundManager.Instance.sfxVolume;
        sprintAudio.volume = 0.5f * SoundManager.Instance.sfxVolume;
        damageAudio.volume = 0.5f * SoundManager.Instance.sfxVolume;
        staminaAudio.volume = 0.3f * SoundManager.Instance.sfxVolume;
        shieldEquip.volume = 1 * SoundManager.Instance.sfxVolume;
        ammoAudio.volume = 1 * SoundManager.Instance.sfxVolume;
        healAudio.volume = 1 * SoundManager.Instance.sfxVolume;
        shieldBreak.volume = 0.5f * SoundManager.Instance.sfxVolume;
    }
}
