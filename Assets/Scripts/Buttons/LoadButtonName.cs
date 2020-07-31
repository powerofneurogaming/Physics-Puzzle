using UnityEditor;
using UnityEngine;

public class LoadButtonName : MonoBehaviour
{
    // Simply loads the scene of the object's name

    SceneLoading sl = new SceneLoading();
    public void LoadSceneOfObjectName()
    {       
        _ = sl.TryLoadingScene(name);
    }

    private void Start()
    {
        // Deactivate button if the scene doesn't exist
        if (!sl.CheckForScene(name))
            gameObject.SetActive(false);
    }
}
