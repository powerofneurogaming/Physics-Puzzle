using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public SceneController SceneController;

    // private SceneParsing _sp = new SceneParsing();
    private SceneLoading _sl = new SceneLoading();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadNextLevel()
    {
        Debug.Log("Starting LoadNextLevel!");
        // TODO: Replace parsing with a direct reference to the current level & world,
        // likely from another object
        /*
        string currentSceneName = SceneManager.GetActiveScene().name;
        */
        _sl.loadLevel(SceneController.getWorld(), SceneController.getLevel());
    }
}
