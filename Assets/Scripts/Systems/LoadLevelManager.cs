using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelManager : MonoBehaviour
{
    public static LoadLevelManager instance;
    private void Awake()
    {
        instance = this;
    }
    public void LoadLevel(int levelID)
    {
        Debug.Log("load level");
        SceneManager.LoadScene(levelID);
        //Debug.Log("Loading Level only works if the Level is added in build settings");
    }

    public void ReloadCurrentLevel()
    {
        int scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(scene, LoadSceneMode.Single); //Single unloads all the gameobjects before reloading it
        //Debug.Log("Reload level only works if the Level is added in build settings");
    }
    public void LoadNext()
    {
        int scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(scene + 1, LoadSceneMode.Single); //Single unloads all the gameobjects before reloading it
        //Debug.Log("Loading next level only works if both Levels are added in build settings");
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit works only in Export");
    }
}
