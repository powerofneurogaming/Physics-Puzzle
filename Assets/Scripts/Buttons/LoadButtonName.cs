using UnityEditor;
using UnityEngine;

public class LoadButtonName : MonoBehaviour
{
    // Simply loads the scene of the object's name
    public void LoadSceneOfObjectName()
    {
        SceneLoading sl = new SceneLoading();
        _ = sl.TryLoadingScene(name);
    }
}
