using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public AudioSource backgroundMusic; // Música de fondo
    public Slider volumeSlider; // Control de volumen
    private float currentVolume = 1f;

    void Start()
    {
        LoadSettings();
        PlayBackgroundMusic();

    }

    public void LoadSettings()
    {
        // Cargar ajustes guardados
        currentVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);

        // Verificar que el AudioSource no sea nulo antes de acceder a él
        if (backgroundMusic != null)
        {
            // Aplicar ajustes al AudioSource
            backgroundMusic.volume = currentVolume;
        }

        // Configurar el slider si existe en la escena
        if (volumeSlider != null)
        {
            volumeSlider.value = currentVolume;
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }
    }

    public void SetVolume(float volume)
    {
        currentVolume = volume;
        if (backgroundMusic != null) backgroundMusic.volume = volume;
        PlayerPrefs.SetFloat("MusicVolume", volume);
        PlayerPrefs.Save();
    }

    public void ResetToDefault()
    {
        // Restablecer valores por defecto
        currentVolume = 1f;
        PlayerPrefs.SetFloat("MusicVolume", currentVolume);
        PlayerPrefs.Save();
        LoadSettings();
    }

    private void PlayBackgroundMusic()
    {
        if (backgroundMusic != null && !backgroundMusic.isPlaying)
        {
            backgroundMusic.Play();
        }
    }
}
