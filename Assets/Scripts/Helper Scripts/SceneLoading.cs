using UnityEngine;
using UnityEngine.SceneManagement;

//namespace SceneLoading
//{
// TODO: See if MonoBehavior is required
public class SceneLoading // : MonoBehaviour
{

    // Functions
    // Public

    // Loads scene and returns true if it exists, otherwise returns false
    public bool TryLoadingScene(string sceneName)
    {
        // Debug.Log("TryLoadingScene (from SceneLoadingHelper) has started!");
        if (Application.CanStreamedLevelBeLoaded(sceneName))
        {
            Debug.LogFormat("Found scene {0}! Now loading!", sceneName);
            SceneManager.LoadScene(sceneName);
            return true;
        }
        else
        {
            Debug.LogFormat("Did not find scene {0}! Either it doesn't exist, or it isn't in the build.", sceneName);
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

}
// }
