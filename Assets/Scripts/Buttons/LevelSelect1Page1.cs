using UnityEngine;
// personal namespaces

public class LevelSelect1Page1 : MonoBehaviour
{
    private SceneLoading _sl = new SceneLoading();
    public bool GoToLevelSelect1Page1()
    {
        // TODO: Make 
        // TODO: check that scene exists
        Debug.Log("Going to first page of World 1");
        return _sl.TryLoadingScene("Level Select 1-1");
        //SceneManager.LoadScene("Level Select 1-1");
    }
}
