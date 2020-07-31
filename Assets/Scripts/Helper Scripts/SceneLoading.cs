using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoading // : MonoBehaviour
{

    // Functions
    // Public
    public bool CheckForScene(string sceneName)
    {
        if (Application.CanStreamedLevelBeLoaded(sceneName))
        {
            Debug.LogFormat("Found scene {0}! Now loading!", sceneName);
            return true;
        }
        else
        {
            Debug.LogFormat("Did not find scene {0}! Either it doesn't exist, or it isn't in the build.", sceneName);
        }

        return false;
    }

    // Loads scene and returns true if it exists, otherwise returns false
    public bool TryLoadingScene(string sceneName)
    {
        // Debug.Log("TryLoadingScene (from SceneLoadingHelper) has started!");
        if (CheckForScene(sceneName))
        {
            SceneManager.LoadScene(sceneName);
            return true;
        }
       
        return false;
    }

    // Searches a list of scenes (either as list itself or passed as several strings with commas)
    // Returns the index of the first valid scene (including 0), otherwise returns -1
    public int TryLoadingSceneArray(string[] sceneList)
    {
        int listLength = sceneList.Length;
        for (int i = 0; i < listLength; i++)
        {
            if (TryLoadingScene(sceneList[i]))
            {
                Debug.LogFormat("Found scene at index {0}", i);
                return i;
            }
        }

        Debug.Log("Couldn't find the scene in the list!");
        return -1;
    }

    private bool LoadSceneByNameAndLevel(string name, int levelNo)
    {
        string sceneName = $"{name} {levelNo}";
        return TryLoadingScene(sceneName);
    }
    /*
     * Tries to load the World by number, or the Main Menu if it can't
     * Returns 1 if successful at loading World, 2 if the Main Menu exists,
     * Otherwise, 0 if it can't load any scenes.
     */
    public int LoadWorldNo(int worldNo)
    {
        string world = $"World {worldNo}-1";
        if (TryLoadingScene(world))
            return 1;
        else if (TryLoadingScene("Main Menu"))
            return 2;
        return 0;
    }

    /*
     * Returns 1 if the desired level
     */
    public int loadLevel(int worldNo, int levelNo)
    {
        string level = $"Level {worldNo}-{levelNo}";
        if (TryLoadingScene(level))
            return 1;
        else
        {
            string nextWorldFirstLevel = $"Level {worldNo + 1}-1";
            if (TryLoadingScene(nextWorldFirstLevel))
                return 2;
        }

        return 0;
    }

    // Attempt to load the next level of the scene type (namely Level or World), going to the next world if needed.
    // Return: 1 if the scene type is in the current world, 2 if it's the next world,
    // 0 if nothing was loaded.
    public int LoadNextSceneType(string sceneType, int worldNo, int levelNo)
    {
        int startingWorld = worldNo;
        if (TryLoadingScene($"{sceneType} {worldNo}-{++levelNo}"))
        {
            return 1;
        }
        else
        {
            Debug.Log("Couldn't load the next level in this world's selection -likely the last level\n" +
                $"Trying to load the next {sceneType}'s World");
            levelNo = 1;
            if (TryLoadingScene($"{sceneType} {++worldNo}-{levelNo}"))
            {
                return 2;
            }
            else
            {
                Debug.Log($"Couldn't load the next {sceneType}'s World's first level. \n" +
                    $"You've probably reached the end of the selection, or game!");
                //--worldNo;
            }
        }

        return 0;
    }

    // Attempt to load the next level, going to the next world if needed.
    // Return: 1 if the level is in the current world, 2 if it's the next world,
    // 0 if nothing was loaded.
    public int LoadNextLevel(int worldNo, int levelNo)
    {
        int startingWorld = worldNo;
        return LoadNextSceneType(sceneType: "Level", worldNo, levelNo);
    }

    /*
     * Attempts to load the next level in the world, then the first level of the next world
     * Returns: 0 if no scene found
     */
    public int LoadNextLevel2(int worldNo, int levelNo)
    {
        // int startingWorld = worldNo;
        int levelLoadedLevel = LoadNextSceneType(sceneType: "Level", worldNo, levelNo);
        if (levelLoadedLevel <= 0) // if we didn't load the level
        {
            int sceneLoadedWorld = LoadNextWorld(worldNo, levelNo);
            if (sceneLoadedWorld <= 0)
                return LoadWorldNo(worldNo) * 3;
            return sceneLoadedWorld * 2;
        }
        else
            return levelLoadedLevel;
    }


    // TODO: Make function go to the next worldNo, not levelNo
    public int LoadNextWorld(int worldNo, int levelNo)
    {
        int startingWorld = worldNo;
        return LoadNextSceneType(sceneType: "World", worldNo, levelNo);
    }
}
// }
