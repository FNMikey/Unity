using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

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
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen
            .SetResolution(resolution.width,
            resolution.height,
            Screen.fullScreen);
    }
}
