using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Script used to tie to a button, so that it doesn't have to directly tie
 * the Scene Controller object from the editor.
 */

// TODO
public class LoadCurrentWorld : MonoBehaviour
{
    [SerializeField] private SceneController sceneController;

    public void GoToLevelSelect()
    {
        sceneController.GoToCurrentWorld();
    }

    // Start is called before the first frame update
    void Start()
    {
        if(sceneController==null)
        {
            sceneController = FindObjectOfType<SceneController>();
        }

        UnityEngine.UI.Button currentWorldButton = GetComponent<UnityEngine.UI.Button>();
        currentWorldButton.onClick.AddListener(() => GoToLevelSelect());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
