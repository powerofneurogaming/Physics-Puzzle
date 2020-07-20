// TODO: Write unit test script and test this script
// using System.Diagnostics;
using UnityEngine;

public class SceneParsing // : MonoBehaviour
{

    public string FindStartingWord(string scene)
    {
        int length = scene.IndexOf(" ");
        return scene.Substring(0, length);
    }

    public bool FindIfHasWorldLevel(string scene)
    {
        return scene.Contains("-");
    }
    /*
     * 
     */
    public bool FindFirstWord(string scene, string word)
    {
        if (scene.StartsWith(word))
            return true;
        return false;
    }

    // TODO: Change Contains functions to something more accurate, or remove if not needed
    public bool FindLevelWord(string scene)
    {
        return FindFirstWord(scene, "Level");
    }

    public bool FindWorldWord(string scene)
    {
        return FindFirstWord(scene, "World");
    }

    public bool FindMainMenu(string scene)
    {
        return FindFirstWord(scene, "Main Menu");
    }

    public (int worldNo, int levelNo) FindWorldLevelFromScene(string scene) //, string word)
    {
        /*
        string worldLevel = scene.Replace(word, ""); // " [levelNo]" - white space at least at beginning
        _ = worldLevel.Trim();
        */
        int index1 = scene.IndexOf(" ");
        int index2 = scene.IndexOf("-");
        int startIndex = index1 + 1;
        int length = index2 - startIndex; // length = endIndex - startIndex (end index would be the null or other indiator that the string has ended)
        string world = scene.Substring(index1+1, length);
        string level = scene.Substring(index2 + 1);
        int.TryParse(world, out int worldNo); // convert it to integer
        int.TryParse(level, out int levelNo); // convert it to integer
        return (worldNo, levelNo);
    }

    public int FindWorldFromScene(string scene) //, string word)
    {
        /*
        string worldLevel = scene.Replace(word, ""); // " [levelNo]" - white space at least at beginning
        _ = worldLevel.Trim();
        */
        int index1 = scene.IndexOf(" ");
        int index2 = scene.IndexOf("-");

        int startIndex = index1 + 1;
        int length = index2 - startIndex; // length = endIndex - startIndex (end index would be the null or other indiator that the string has ended)
        
        string world = scene.Substring(startIndex, length);
        int.TryParse(world, out int worldNo); // convert it to integer
        return worldNo;
    }

    public int FindLevelFromScene(string scene) //, string word)
    {
        //string worldLevel = scene.Replace(word, ""); // " [levelNo]" - white space at least at beginning
        // _ = worldLevel.Trim();
        int index2 = scene.IndexOf("-");
        string level = scene.Substring(index2 + 1);
        int.TryParse(level, out int levelNo); // convert it to integer
        return levelNo;
    }

    /*
    public (int worldNo, int levelNo) FindWorldLevelFromWord(string scene, string word)
    {
        
        //string worldLevel = scene.Replace(word, ""); // " [levelNo]" - white space at least at beginning
        // _ = worldLevel.Trim();
        int index1 = scene.IndexOf(" ");
        int index2 = scene.IndexOf("-");
        int startIndex = index1 + 1;
        int length = index2 - startIndex; // length = endIndex - startIndex (end index would be the null or other indiator that the string has ended)
        string world = scene.Substring(index1+1, length);
        string level = scene.Substring(index2 + 1);
        int.TryParse(world, out int worldNo); // convert it to integer
        int.TryParse(level, out int levelNo); // convert it to integer
        return (worldNo, levelNo);
    }


    public (int, int) FindWorldLevelFromLevel(string scene)
    {
        return FindWorldLevelFromScene(scene); //, word: "Level");
    }
    public int FindLevelFromLevel(string scene)
    {
        return FindLevelFromWord(scene, word: "level");
    }

    public int FindWorldFromWord(string scene, string word)
    {
        // Presuming the scene is in the format "[levelType] [worldNo]-[levelNo]
        //string world = scene.Replace(word, ""); // " [worldNo]-[levelNo]" - white space at least at beginning
        //_ = world.Trim();
        int index1 = scene.IndexOf(" ");
        int index2 = scene.IndexOf("-");
        int startIndex = index1 + 1;
        int length = index2 - startIndex; // length = endIndex - startIndex (end index would be the null or other indiator that the string has ended)
        string world = scene.Substring(startIndex, length);
        // level = level.Trim();
        // Debug.Log($"FindWorldFromWord now trimmed scene to {level}");
        int.TryParse(world, out int worldNo); // convert it to integer
        return worldNo;
    }

    public int FindLevelFromWord(string scene, string word)
    {
        // Presuming the scene is in the format "[levelType] [levelNo]
        // string level = scene.Replace(word, ""); // " [levelNo]" - white space at least at beginning
        int index = scene.IndexOf("-");
        string level = scene.Substring(index + 1);
        // level = level.Trim();
        // Debug.Log($"FindLevelFromWord now trimmed scene to {level}");
        int.TryParse(level, out int levelNo); // convert it to integer
        return levelNo;
    }

    public int FindWorldFromLevel(string scene)
    {
        return FindWorldFromWord(scene, word: "level");
    }

    public int FindWorldFromWorld(string scene) //, string word)
    {
        return FindWorldFromWord(scene, word: "World");
    }

    public int FindLevelFromWorld(string scene)
    {
        // Presuming the scene is in the format "Level [worldNo]-{levelNo]"
        return FindWorldFromWord(scene, word: "Level");
    }
    */

}
