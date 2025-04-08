using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private SoundLibrary soundLibrary;
    [SerializeField] private AudioSource sound2DSource;

    [SerializeField] private float _volumeModifier = 0.15f;
    [SerializeField] private float _pitchModifier = 0.1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    
     public void PlaySound2D(string soundName)
    {
        float randVolume = Random.Range(1- _volumeModifier, 1 + _volumeModifier);
        float randPitch = Random.Range(1 - _pitchModifier, 1 + _pitchModifier);
        if (soundName != null)
        {
            sound2DSource.volume = randVolume;
            sound2DSource.pitch = randPitch;
            sound2DSource.PlayOneShot(soundLibrary.GetClipsFromName(soundName));
        }
    }
}
