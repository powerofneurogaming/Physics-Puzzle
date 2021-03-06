﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public SceneController sceneController;

    // private SceneParsing _sp = new SceneParsing();
    private SceneLoading _sl = new SceneLoading();
    private ObjectFinder _of = new ObjectFinder();
    // private SceneController sceneController;

    // Start is called before the first frame update
    void Start()
    {
        GameObject sceneControllerObject;
        if (sceneController == null)
        {
            if (_of.FindObject("Scene Controller", out sceneControllerObject))
                sceneController = sceneControllerObject.GetComponent<SceneController>();
        }
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
        _sl.loadLevel(sceneController.getWorld(), sceneController.getLevel());
    }
}
