using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
<<<<<<< Updated upstream

    [SerializeField]
    private TMP_Dropdown resolutionDropDown;

    private Resolution[] resolutions;

    private List<Resolution> filteredResolutions;

    private float currentRefreshRate;

    private int currentResolutionIndex = 0;

    private void Start()
    {
        resolutions = Screen.resolutions;
        filteredResolutions = new List<Resolution>();

        resolutionDropDown.ClearOptions();
        currentRefreshRate = Screen.currentResolution.refreshRate;

        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].refreshRate == currentRefreshRate)
            {
                filteredResolutions.Add(resolutions[i]);
            }
        }

        List<string> options = new List<string>();
        for (int i = 0; i < filteredResolutions.Count; i++)
        {
            string resolutionOption =
                filteredResolutions[i].width +
                "x" +
                filteredResolutions[i].height +
                " " +
                filteredResolutions[i].refreshRate +
                "Hz";
            options.Add (resolutionOption);
            if (
                filteredResolutions[i].width == Screen.width &&
                filteredResolutions[i].height == Screen.height
            )
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropDown.AddOptions (options);
        resolutionDropDown.value = currentResolutionIndex;
        resolutionDropDown.RefreshShownValue();
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel (qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
=======
    [SerializeField] private Slider master;
    [SerializeField] private Slider music;
    [SerializeField] private Slider sfx;
    [SerializeField] private TMP_Dropdown resolutionDropDown;
    [SerializeField] private Toggle isFullscreen;
    [SerializeField] private TMP_Dropdown graphics;
    [SerializeField] private TMP_Dropdown multisampling;
    [SerializeField] private TMP_Dropdown textureDetails;
    [SerializeField] private TMP_Dropdown textureFiltering;
    [SerializeField] private TMP_Dropdown vSyncCount;

    private Resolution[] resolutions;

    private void Start()
    {
        if (PlayerPrefs.HasKey("volume"))
        {
            float value = PlayerPrefs.GetFloat("volume");
            master.SetValueWithoutNotify(value);
        }

        if (PlayerPrefs.HasKey("Music"))
        {
            float value = PlayerPrefs.GetFloat("Music");
            music.SetValueWithoutNotify(value);
        }

        if (PlayerPrefs.HasKey("SFX"))
        {
            float value = PlayerPrefs.GetFloat("SFX");
            sfx.SetValueWithoutNotify(value);
        }
    }

    public void SetVolumeMaster ()
    {
        float volume = master.value;
        audioMixer.SetFloat("volume",volume);
        PlayerPrefs.SetFloat("volume", volume);
    }

    public void SetVolumeMusic()
    {
        float volume = music.value;
        audioMixer.SetFloat("Music", volume);
        PlayerPrefs.SetFloat("Music", volume);
    }

    public void SetVolumeSFX()
>>>>>>> Stashed changes
    {
        float volume = sfx.value;
        audioMixer.SetFloat("SFX", volume);
        PlayerPrefs.SetFloat("SFX", volume);
    }

    public void SetQuality (int quality)
    {
        QualitySettings.SetQualityLevel(quality);
        PlayerPrefs.SetInt("QualityLevel", quality);
    }

    public void SetFullscreen (bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        int fullScreenInt = isFullScreen ? 1 : 0;
        PlayerPrefs.SetInt("FullScreenMode", fullScreenInt);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
<<<<<<< Updated upstream
        Screen
            .SetResolution(resolution.width,
            resolution.height,
            Screen.fullScreen);
=======
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("resolutionIndex", resolutionIndex);
    }

    public void SetMultisampling(int sampling)
    {
        QualitySettings.antiAliasing = sampling;
        PlayerPrefs.SetInt("sampling", sampling);
    }

    public void SetTextureDetails(int textureDetails)
    {
        QualitySettings.globalTextureMipmapLimit = textureDetails;
        PlayerPrefs.SetInt("textureDetails", textureDetails);
    }

    public void SetTextureFiltering(int textureFilterin)
    {
        QualitySettings.anisotropicFiltering = (AnisotropicFiltering)textureFilterin;
        PlayerPrefs.SetInt("textureFilterin", textureFilterin);
    }

    public void SetvSync(int vSyncC)
    {
        QualitySettings.vSyncCount = vSyncC;
        PlayerPrefs.SetInt("vSyncC", vSyncC);
>>>>>>> Stashed changes
    }
}
