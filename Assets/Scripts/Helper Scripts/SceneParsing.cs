// TODO: Write unit test script and test this script
public class SceneParsing // : MonoBehaviour
{

    public bool FindWordInScene(string scene, string word)
    {
        if (scene.StartsWith(word))
            return true;
        return false;
    }
    // TODO: Change Contains functions to something more accurate, or remove if not needed
    public bool ContainsLevel(string scene)
    {
        return FindWordInScene(scene, "Level");
    }

    public bool ContainsWorld(string scene)
    {
        return FindWordInScene(scene, "World");
    }

    public bool ContainsMainMenu(string scene)
    {
        return FindWordInScene(scene, "Main Menu");
    }

    public int FindLevelFromWord(string scene, string levelType)
    {
        // Presuming the scene is in the format "[levelType] [levelNo]
        string levelNo = scene.Replace(levelType, ""); // " [levelNo]" - white space at least at beginning
        levelNo = levelNo.Trim();
        return int.Parse(levelNo);
    }

    public int FindLevelNumber(string scene)
    {
        //string currentSceneName = SceneManager.GetActiveScene().name;
        // To get the number, we have to remove some words and characters
        // Presuming the scene is in the format "Level [worldNo]-{levelNo]"
        // string level = scene.Replace("Level ", "");
        int index = scene.IndexOf("-");
        string level = scene.Substring(index + 1);
        //string level = scene.Remove(0, index); // should be the level number
        return int.Parse(level); // convert it to integer
    }

    public int FindWorldFromWordScene(string scene, string word)
    {
        // Presuming the scene is in the format "[word] [worldNo]-{levelNo]"
        string world = scene.Replace(word, ""); // [worldNo]-[LevelNo]
        world = world.Trim();
        int length = scene.IndexOf("-"); // due to indexing starting at 0 and the index being after what we want, it gives the length
        world = world.Substring(0, length); // [worldNo]
        return int.Parse(world);
    }

    public int FindWorldFromLevelScene(string scene)
    {
        // Presuming the scene is in the format "Level [worldNo]-{levelNo]"
        return FindWorldFromWordScene(scene, "Level");
    }

    public int FindWorldNumber(string scene)
    {
        // Parsing scene in the format "World {worldNo]"
        return FindLevelFromWord(scene, "World");
    }
}
