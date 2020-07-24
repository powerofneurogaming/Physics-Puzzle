using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldSelect : MonoBehaviour
{
    private SceneLoading _sl = new SceneLoading();
    public void GoToWorldSelect()
    {
        // TODO: check that scene exists
        Debug.Log("Going to world select scene");
        _sl.TryLoadingScene("World Select");
        // SceneManager.LoadScene("World Select");
    }
}
