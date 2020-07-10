using UnityEngine;
using UnityEngine.SceneManagement;

// TODO: See if MonoBehavior is required
public class SceneLoadingHelper : MonoBehaviour
{
    // Loads scene and returns true if it exists, otherwise returns false
    public bool TryLoadingScene(string sceneName)
    {
        Debug.Log("TryLoadingScene (from SceneLoadingHelper) has started!");
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



}
