using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParsingTests : MonoBehaviour
{
    private SceneParsing _sp;
    // Start is called before the first frame update
    void Start()
    {
        _sp = new SceneParsing();
        string testScene = "World 1";
        int actualLevel = 1;

        Debug.LogFormat("Now testing the scene parser!");
        if (_sp.FindLevelFromWord(testScene, "World") == actualLevel)
            Debug.Log("FindLevelFromWord actually found the level!");
        else
            Debug.Log("FindLevelFromWord failed to find the level!");

        if(_sp.FindWordInScene(testScene, "World"))
            Debug.Log("FindWordInScene found the \"World\"!");
        else
            Debug.Log("FindWordInScene didn't find the \"World\"!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
