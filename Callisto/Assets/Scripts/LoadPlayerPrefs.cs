using UnityEngine;
using UnityEngine.Audio;

public class LoadPlayerPrefs : MonoBehaviour
{

    public AudioMixer audioMixer;
    private Resolution[] resolutions;
    void Start()
    {
        resolutions = Screen.resolutions;

        if (PlayerPrefs.HasKey("volume"))
        {
            float value = PlayerPrefs.GetFloat("volume");
            audioMixer.SetFloat("volume", value);
        }

        if (PlayerPrefs.HasKey("Music"))
        {
            float value = PlayerPrefs.GetFloat("Music");
            audioMixer.SetFloat("Music", value);
        }

        if (PlayerPrefs.HasKey("SFX"))
        {
            float value = PlayerPrefs.GetFloat("SFX");
            audioMixer.SetFloat("SFX", value);
        }

        if (PlayerPrefs.HasKey("QualityLevel"))
        {
            QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("QualityLevel"));
        }

        if (PlayerPrefs.HasKey("resolutionIndex"))
        {
            Resolution resolution = resolutions[PlayerPrefs.GetInt("resolutionIndex")];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        }

        if (PlayerPrefs.HasKey("sampling"))
        {
            QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("sampling"));
        }

        if (PlayerPrefs.HasKey("textureDetails"))
        {
            QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("textureDetails"));
        }

        if (PlayerPrefs.HasKey("textureFilterin"))
        {
            QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("textureFilterin"));
        }

        if (PlayerPrefs.HasKey("vSyncC"))
        {
            QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("vSyncC"));
        }

        if (PlayerPrefs.HasKey("isFullScreen"))
        {
            bool isFullScreen = PlayerPrefs.GetInt("FullScreenMode") == 1;
            Screen.fullScreen = isFullScreen;
        }

    }
}
