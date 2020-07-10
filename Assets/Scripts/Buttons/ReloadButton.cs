using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadButton : MonoBehaviour
{
    // TODO: remove variable if single instance and have single call on one line.
    private SceneLoading _sl = new SceneLoading();
    public void RestartLevel()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        Debug.LogFormat("Reloading scene {0}", currentSceneName);
        _sl.TryLoadingScene(currentSceneName);
        // SceneManager.LoadScene(currentSceneName);
    }
    // Update is called once per frame
    /*void Update()
    {
        
    } */
}
