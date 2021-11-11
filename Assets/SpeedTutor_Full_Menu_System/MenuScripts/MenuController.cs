﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace SpeedTutorMainMenuSystem
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField] private bool isMainMenu = false;
        #region Default Values
        [Header("Default Menu Values")]
        [SerializeField] private float defaultBrightness =1;
        [SerializeField] private float defaultVolume = 0.5f;
        [SerializeField] private int defaultSen=4;
        
        //[SerializeField] private bool defaultInvertY = false;

        [Header("Levels To Load")]
        public int _newGameButtonLevel;

        public int _loadGameButtonLevel;
        private string levelToLoad;

        private int menuNumber;
        #endregion

        [SerializeField] private Text playerTag = null;
        [SerializeField] private Text savedState = null;
        
        #region Menu Dialogs
        [Header("Main Menu Components")]
        [SerializeField] private GameObject menuDefaultCanvas = null;
        [SerializeField] private GameObject GeneralSettingsCanvas = null;
        [SerializeField] private GameObject graphicsMenu = null;
        [SerializeField] private GameObject soundMenu = null;
        [SerializeField] private GameObject gameplayMenu = null;
        [SerializeField] private GameObject controlsMenu = null;
        [SerializeField] private GameObject evalMenu = null;
        [SerializeField] private GameObject confirmationMenu = null;
        [Space(10)]
        [Header("Menu Popout Dialogs")]
        [SerializeField] private GameObject noSaveDialog = null;
        [SerializeField] private GameObject newGameDialog = null;
        [SerializeField] private GameObject loadGameDialog = null;
        #endregion

        #region Slider Linking
        [Header("Menu Sliders")]
        [SerializeField] private Text controllerSenText  = null;
        [SerializeField] private Slider controllerSenSlider = null;
        public float controlSenFloat = 2f;
        [Space(10)]
        [SerializeField] private Brightness brightnessEffect = null;
        [SerializeField] private Slider brightnessSlider = null;
        [SerializeField] private Text brightnessText = null;
        [Space(10)]
        [SerializeField] private Text volumeText = null;
        [SerializeField] private Slider volumeSlider = null;
        [Space(10)]
        [SerializeField] private Toggle invertYToggle = null;
        #endregion

        #region Initialisation - Button Selection & Menu Order
        private void Start()
        {
            menuNumber = 1;
        }
        #endregion

        //MAIN SECTION
        public IEnumerator ConfirmationBox()
        {
            confirmationMenu.SetActive(true);
            yield return new WaitForSeconds(2);
            confirmationMenu.SetActive(false);
        }

        private void Awake()
        {
            //create playername if none exists.
            if (!PlayerPrefs.HasKey("PlayerName"))
            {
                GeneratePlayerName();
            }
            else
            {
                if (playerTag != null)
                {
                    playerTag.text = PlayerPrefs.GetString("PlayerName", "Error:notSet");
                }
                Debug.Log("PlayerName Loaded "+PlayerPrefs.GetString("PlayerName"));
            }

            if (PlayerPrefs.HasKey("Version"))
            {
                Debug.Log("Saved Game is "+ PlayerPrefs.GetInt("Version"));
                if (savedState != null)
                {
                    if (PlayerPrefs.GetInt("Version") == 0)
                    {
                        savedState.text = "Saved Game is Version A";
                    }
                    else
                    {
                        savedState.text = "Saved Game is Version B";
                    }
                }
            }
            else
            {
                if (savedState != null)
                {
                    savedState.text = "No Saved Game";
                }
            }
        }

        private void GeneratePlayerName()
        {
            string ret = "";
            for (int i = 0; i < 6; i++)
            {
                ret += UnityEngine.Random.Range(0, 10);
            }
            Debug.Log("Generate Player Name "+ret);
            if (playerTag != null)
            {
                playerTag.text = ret;
            }
            PlayerPrefs.SetString("PlayerName",ret);
        }

        public void OnOpenmenu()
        {
            if (isMainMenu)
            {
                if (menuNumber == 2 || menuNumber == 7 || menuNumber == 8)
                {
                    GoBackToMainMenu();
                    ClickSound();
                }

                else if (menuNumber == 3 || menuNumber == 4 || menuNumber == 5)
                {
                    GoBackToOptionsMenu();
                    ClickSound();
                }

                else if (menuNumber == 6) //CONTROLS MENU
                {
                    GoBackToGameplayMenu();
                    ClickSound();
                }
            }
        }

        private void ClickSound()
        {
            GetComponent<AudioSource>().Play();
        }

        #region Menu Mouse Clicks
        public void MouseClick(string buttonType)
        {
            if (buttonType == "Controls")
            {
                gameplayMenu.SetActive(false);
                controlsMenu.SetActive(true);
                menuNumber = 6;
            }

            if (buttonType == "Graphics")
            {
                GeneralSettingsCanvas.SetActive(false);
                graphicsMenu.SetActive(true);
                menuNumber = 3;
            }

            if (buttonType == "Sound")
            {
                GeneralSettingsCanvas.SetActive(false);
                soundMenu.SetActive(true);
                menuNumber = 4;
            }

            if (buttonType == "Gameplay")
            {
                GeneralSettingsCanvas.SetActive(false);
                gameplayMenu.SetActive(true);
                menuNumber = 5;
            }

            if (buttonType == "EvaluationSettings")
            {
                GeneralSettingsCanvas.SetActive(false);
                evalMenu.SetActive(true);
                menuNumber = 10;
            }

            if (buttonType == "BackToMenu")
            {
                LoadAndSaveGame.Instance.SaveState();
                SceneManager.LoadScene(0);
            }
            if (buttonType == "Exit")
            {
                try
                {
                    LoadAndSaveGame.Instance.SaveState();
                }
                catch (Exception e)
                {
                    Debug.Log(e);
                }
                Application.Quit();
            }

            if (buttonType == "Options")
            {
                menuDefaultCanvas.SetActive(false);
                GeneralSettingsCanvas.SetActive(true);
                menuNumber = 2;
            }

            if (buttonType == "LoadGame")
            {
                menuDefaultCanvas.SetActive(false);
                loadGameDialog.SetActive(true);
                menuNumber = 8;
            }

            if (buttonType == "NewGame")
            {
                menuDefaultCanvas.SetActive(false);
                newGameDialog.SetActive(true);
                menuNumber = 7;
            }
        }
        #endregion

        public void VolumeSlider(float volume)
        {
            AudioListener.volume = volume;
            volumeText.text = volume.ToString("0.0");
        }

        public void VolumeApply()
        {
            PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
            Debug.Log(PlayerPrefs.GetFloat("masterVolume"));
            StartCoroutine(ConfirmationBox());
        }

        public void BrightnessSlider(float brightness)
        {
            brightnessEffect.brightness = brightness;
            brightnessText.text = brightness.ToString("0.0");
        }

        public void BrightnessApply()
        {
            try
            {
                PlayerPrefs.SetFloat("masterBrightness", brightnessEffect.brightness);
                Debug.Log(PlayerPrefs.GetFloat("masterBrightness"));
                StartCoroutine(ConfirmationBox());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }

        public void ControllerSen()
        {
            controllerSenText.text = controllerSenSlider.value.ToString("0");
            controlSenFloat = controllerSenSlider.value;
        }

        public void GameplayApply()
        {
            if (invertYToggle.isOn) //Invert Y ON
            {
                PlayerPrefs.SetInt("masterInvertY", 1);
                Debug.Log("Invert" + " " + PlayerPrefs.GetInt("masterInvertY"));
            }

            else if (!invertYToggle.isOn) //Invert Y OFF
            {
                PlayerPrefs.SetInt("masterInvertY", 0);
                Debug.Log(PlayerPrefs.GetInt("masterInvertY"));
            }

            PlayerPrefs.SetFloat("masterSen", controlSenFloat);
            Debug.Log("Sensitivity" + " " + PlayerPrefs.GetFloat("masterSen"));

            StartCoroutine(ConfirmationBox());
        }

        #region ResetButton
        public void ResetButton(string GraphicsMenu)
        {
            if (GraphicsMenu == "Brightness")
            {
                brightnessEffect.brightness = defaultBrightness;
                brightnessSlider.value = defaultBrightness;
                brightnessText.text = defaultBrightness.ToString("0.0");
                BrightnessApply();
            }

            if (GraphicsMenu == "Audio")
            {
                AudioListener.volume = defaultVolume;
                volumeSlider.value = defaultVolume;
                volumeText.text = defaultVolume.ToString("0.0");
                VolumeApply();
            }

            if (GraphicsMenu == "Graphics")
            {
                controllerSenText.text = defaultSen.ToString("0");
                controllerSenSlider.value = defaultSen;
                controlSenFloat = defaultSen;

                invertYToggle.isOn = false;

                GameplayApply();
            }
        }
        #endregion

        #region Dialog Options - This is where we load what has been saved in player prefs!
        public void ClickNewGameDialog(string ButtonType)
        {
            if (ButtonType == "0")
            {
                PlayerPrefs.SetInt("LoadSavedState", 0);
                SceneManager.LoadScene(_newGameButtonLevel);
                PlayerPrefs.SetInt("Version",0);
            }

            if (ButtonType == "1")
            {
                PlayerPrefs.SetInt("LoadSavedState", 0);
                SceneManager.LoadScene(_newGameButtonLevel);
                PlayerPrefs.SetInt("Version",1);
            }

            if (ButtonType == "No")
            {
                GoBackToMainMenu();
            }
        }

        public void ClickLoadGameDialog(string ButtonType)
        {
            if (ButtonType == "Yes")
            {
                if (PlayerPrefs.HasKey("LoadSavedState"))
                {
                    Debug.Log("I WANT TO LOAD THE SAVED GAME");
                    //LOAD LAST SAVED SCENE
                    //levelToLoad = PlayerPrefs.GetString("SavedLevel");
                    //SceneManager.LoadScene(levelToLoad);
                    PlayerPrefs.SetInt("LoadSavedState", 1);
                    SceneManager.LoadScene(_loadGameButtonLevel);
                }

                else
                {
                    Debug.Log("Load Game Dialog");
                    menuDefaultCanvas.SetActive(false);
                    loadGameDialog.SetActive(false);
                    noSaveDialog.SetActive(true);
                }
            }

            if (ButtonType == "No")
            {
                GoBackToMainMenu();
            }
        }

        public void ClickEvaluationDialog(string ButtonType)
        {
            if (ButtonType == "PlayerName")
            {
                GeneratePlayerName();
            }

            if (ButtonType == "Back")
            {
                GoBackToOptionsMenu();
            }
        }
        #endregion

        #region Back to Menus
        public void GoBackToOptionsMenu()
        {
            GeneralSettingsCanvas.SetActive(true);
            graphicsMenu.SetActive(false);
            soundMenu.SetActive(false);
            gameplayMenu.SetActive(false);
            if (evalMenu != null)
            {
                evalMenu.SetActive(false);
            }
            

            GameplayApply();
            BrightnessApply();
            VolumeApply();

            menuNumber = 2;
        }

        public void GoBackToMainMenu()
        {
            menuDefaultCanvas.SetActive(true);
            newGameDialog.SetActive(false);
            loadGameDialog.SetActive(false);
            noSaveDialog.SetActive(false);
            GeneralSettingsCanvas.SetActive(false);
            graphicsMenu.SetActive(false);
            soundMenu.SetActive(false);
            gameplayMenu.SetActive(false);
            if (evalMenu != null)
            {
                evalMenu.SetActive(false);
            }
            menuNumber = 1;
        }

        public void GoBackToGameplayMenu()
        {
            controlsMenu.SetActive(false);
            gameplayMenu.SetActive(true);
            menuNumber = 5;
        }

        public void ClickQuitOptions()
        {
            GoBackToMainMenu();
        }

        public void ClickNoSaveDialog()
        {
            GoBackToMainMenu();
        }
        #endregion
    }
}
