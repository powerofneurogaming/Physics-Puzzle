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
    private SceneParsing _sp;
    private static int World = 0; // {get; set; }  //= 0; // 
    private static int Level = 0; // { get; set; }

    // TODO: replace getters and setters with shorthand
    public int getWorld(){return World;}
    public void setWorld(int newWorld) => World = newWorld;
    public int getLevel() { return Level; }
    public void setLevel(int newLevel) => Level = newLevel;


    // Start is called before the first frame update
    void Start()
    {
        // Seet the world values upon starting - i.e. every time a scene is loaded
        _sp = new SceneParsing();
        string currentScene = SceneManager.GetActiveScene().name;
        Debug.LogFormat("Getting info on scene {0}", currentScene);
        // check based on the most common levels
        if(_sp.ContainsLevel(currentScene))
        {
            World = _sp.FindWorldFromLevelScene(currentScene);
            Level=_sp.FindLevelNumber(currentScene);
        }
        else if(_sp.ContainsWorld(currentScene))
        {
            World = _sp.FindWorldNumber(currentScene);
            Level = 1;
        }
        else
        {
            World = 0;
            Level = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
