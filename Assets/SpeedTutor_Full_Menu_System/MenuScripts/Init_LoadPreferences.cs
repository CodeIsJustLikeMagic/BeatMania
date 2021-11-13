using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpeedTutorMainMenuSystem
{
    public class Init_LoadPreferences : MonoBehaviour
    {
        #region Variables
        //BRIGHTNESS
        [Space(20)]
        [SerializeField] private Brightness brightnessEffect =null;
        [SerializeField] private Text brightnessText = null;
        [SerializeField] private Slider brightnessSlider = null;

        //VOLUME
        [Space(20)]
        [SerializeField] private Text volumeText = null;
        [SerializeField] private Slider volumeSlider = null;

        //SENSITIVITY
        [Space(20)]
        [SerializeField] private Text controllerText = null;
        [SerializeField] private Slider controllerSlider = null;

        //INVERT Y
        [Space(20)]
        [SerializeField] private Toggle invertYToggle = null;

        [Space(20)]
        [SerializeField] private bool canUse = false;
        [SerializeField] private MenuController menuController = null;
        #endregion

        private void Awake()
        {
            Debug.Log("Loading player prefs settings");

            if (canUse)
            {
                //BRIGHTNESS
                if (brightnessEffect != null)
                {
                    if (PlayerPrefs.HasKey("masterBrightness"))
                    {
                        float localBrightness = PlayerPrefs.GetFloat("masterBrightness");

                        brightnessText.text = localBrightness.ToString("0.0");
                        brightnessSlider.value = localBrightness;
                        brightnessEffect.brightness = localBrightness;
                    }

                    else
                    {
                        menuController.ResetButton("Brightness");
                    }
                }

                //VOLUME
                if (PlayerPrefs.HasKey("masterVolume"))
                {
                    float localVolume = PlayerPrefs.GetFloat("masterVolume");

                    volumeText.text = localVolume.ToString("0.0");
                    volumeSlider.value = localVolume;
                    AudioListener.volume = localVolume;
                }
                else
                {
                    menuController.ResetButton("Audio");
                }

            }
        }
    }
}
