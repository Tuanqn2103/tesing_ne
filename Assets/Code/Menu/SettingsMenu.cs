using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown;
    Resolution[] customResolutions = new Resolution[3];

    void Start()
    {
        // Define the custom resolutions
        customResolutions[0] = new Resolution { width = 1920, height = 1080 };
        customResolutions[1] = new Resolution { width = 1680, height = 1050 };
        customResolutions[2] = new Resolution { width = 1680, height = 900 };

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < customResolutions.Length; i++)
        {
            string option = customResolutions[i].width + " x " + customResolutions[i].height;
            options.Add(option);

            // Check if this is the current resolution
            if (customResolutions[i].width == Screen.currentResolution.width && 
                customResolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = customResolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
}
