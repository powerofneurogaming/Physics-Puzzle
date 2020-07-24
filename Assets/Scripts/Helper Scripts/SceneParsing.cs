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
        string world = scene.Substring(index1 + 1, length);
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

}