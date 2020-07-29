using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Class for maintaining commonly used information on the scene
 */
public class SceneController : MonoBehaviour
{
    private SceneParsing _sp = new SceneParsing();
    private SceneLoading _sl = new SceneLoading();
    private static int World = -1; // {get; set; }  //= 0; // 
    private static int Level = -1; // { get; set; 
    private static string _currentScene = "Main Menu";  // assuming start from Main Menu
    private static string _previousScene = "Main Menu";
    // Since the Options menu can be from anywhere, we have to memorize the scene that was brought from there
    private static string _returnFromOptionsScene = "Main Menu";

    // TODO: replace getters and setters with shorthand
    public int getWorld(){return World;}
    public void setWorld(int newWorld) => World = newWorld;
    public int getLevel() { return Level; }
    public void setLevel(int newLevel) => Level = newLevel;

    // TODO: Replace ReloadButton with direct to SceneController & this function
    public void RestartLevel()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        Debug.LogFormat("Reloading scene {0}", currentSceneName);
        _sl.TryLoadingScene(currentSceneName);
        // SceneManager.LoadScene(currentSceneName);
    }

    //Almost the same as SceneLoading's LoadNextLevel, but it takes in the inputs
    public void GoToNextLevel()
    {
        _sl.LoadNextLevel2(worldNo: World, levelNo: Level);
    }

    public void GoToCurrentWorld()
    {
        _sl.LoadWorldNo(World);
    }


    public void LoadPreviousScene()
    {
        _sl.TryLoadingScene(sceneName: _previousScene);
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Scene Controller has started!");
        Debug.LogFormat("Object name of {0}", name);
        // Set the world values upon starting - i.e. every time a scene is loaded
        // _sp = new SceneParsing();

        // next is used temporarily, in relation to the previous scenes we've loaded
        string nextScene = SceneManager.GetActiveScene().name;
        Debug.LogFormat("Getting info on scene {0}", nextScene);

        if(!nextScene.Equals(_currentScene)) // if we are loading a different scene
        {
            // We've found a new scene, so switch the 
            _previousScene = _currentScene;
            _currentScene = nextScene;
        }
        // check based on the most common levels
        Debug.LogFormat("Getting world and level on scene {0}", nextScene);
        if (_sp.FindIfHasWorldLevel(_currentScene)) // _sp.FindLevelWord(_currentScene))
        {
            (World, Level) = _sp.FindWorldLevelFromScene(_currentScene);
            Debug.Log($"Got World {World} and Level {Level}");
            // World = _sp.FindLevelFromScene(_currentScene);
            // Level=_sp.FindWorldFromScene(_currentScene);
        }
        else
        {
            Debug.LogFormat("Couldn't get info on scene {0}\n" +
                "Setting world and level to 0", nextScene);
            World = 0;
            Level = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
