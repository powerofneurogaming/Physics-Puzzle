using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadButton : MonoBehaviour
{

    public void RestartLevel()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        Debug.LogFormat("Reloading scene {0}", currentSceneName);
        SceneManager.LoadScene(currentSceneName);
    }
    // Update is called once per frame
    /*void Update()
    {
        
    } */
}
