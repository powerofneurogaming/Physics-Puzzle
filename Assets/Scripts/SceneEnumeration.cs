using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneEnumeration : MonoBehaviour
{

    public bool IsLevel(string scene)
    {
        if (scene.StartsWith("Level") )
            return true;
        return false;
    }

    public bool IsWorld(string scene)
    {
        if (scene.StartsWith("World"))
            return true;
        return false;
    }

    public bool IsMainMenu()
    {
        if (scene.StartsWith("Main Menu"))
            return true;
        return false;
    }

    public int FindLevelNumber(string scene)
    {
        //string currentSceneName = SceneManager.GetActiveScene().name;
        // To get the number, we have to remove some words and characters
        // Presuming the scene is in the format "Level [worldNo]-{levelNo]"
        // string level = scene.Replace("Level ", "");
        int endIndex = scene.IndexOf("-");
        string level = scene.Remove(0, endIndex + 1); // should be the level number
        return int.Parse(level); // convert it to integer
    }

    public int FindWorldFromLevelScene(string scene)
    {
        // Presuming the scene is in the format "Level [worldNo]-{levelNo]"
        string world = scene.Replace("Level ", ""); // [worldNo]-[LevelNo]
        int startIndex = scene.IndexOf("-");
        world = world.Substring(startIndex+1);
        return int.Parse(world);
    }

    FindLevelFromWord(string scene, string levelType="Level")
    {
        // Presuming the scene is in the format "[levelType] [levelNo]
        string world = scene.Replace(levelType, "");
        return int.Parse(world);
    }

    public int FindWorldNumber(string scene)
    {
        // Parsing scene in the format "World {worldNo]"
        string world = scene.Replace("World ", "");
        return int.Parse(world);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
