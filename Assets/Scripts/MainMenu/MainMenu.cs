using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundSlider;
    [SerializeField] private AudioMixer audioMixer;

    void Start()
    {
        LoadVolume();
        
        MusicManager.Instance.PlayMusic("MainMenu");
    }

    public void Play()
    {
        SceneManager.LoadScene("Game");
        MusicManager.Instance.PlayMusic("Game");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void UpdateMusicVolume(float volume)
    {
        audioMixer.SetFloat("musicVolume", volume);
    }

    public void UpdateSoundVolume(float volume)
    {
        audioMixer.SetFloat("soundVolume", volume);
    }

    public void SaveVolume()
    {
        audioMixer.GetFloat("musicVolume", out float musicVolume);
        audioMixer.GetFloat("soundVolume", out float soundVolume);

        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.SetFloat("SoundVolume", soundVolume);
    }
    private void LoadVolume()
    {
        if (musicSlider != null)
        {
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
            UpdateMusicVolume(musicSlider.value);
        }
        if (soundSlider != null)
        {
            soundSlider.value = PlayerPrefs.GetFloat("SoundVolume");
            UpdateSoundVolume(soundSlider.value);
        }
    }
}