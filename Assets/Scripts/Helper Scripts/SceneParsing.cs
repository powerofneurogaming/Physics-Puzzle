using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// TODO: Write unit test script and test this script
public class SceneParsing // : MonoBehaviour
{

    public bool IsALevel(string scene)
    {
        if (scene.StartsWith("Level") )
            return true;
        return false;
    }

    public bool IsAWorld(string scene)
    {
        if (scene.StartsWith("World"))
            return true;
        return false;
    }

    public bool IsAMainMenu(string scene)
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

    public int FindLevelFromWord(string scene, string levelType="Level")
    {
        // Presuming the scene is in the format "[levelType] [levelNo]
        string levelNo = scene.Replace(levelType, ""); // " [levelNo]" - white space at least at beginning
        levelNo = levelNo.Trim();
        return int.Parse(levelNo);
    }

    public int FindWorldNumber(string scene)
    {
        // Parsing scene in the format "World {worldNo]"
        string world = scene.Replace("World ", "");
        return int.Parse(world);
    }
}
