using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ParsingTests : MonoBehaviour
{
    private SceneParsing _sp;
    // Start is called before the first frame update
    void Start()
    {
        _sp = new SceneParsing();
        string worldScene = "World 2-1";
        int actualWorldWorld = 2;
        int actualWorldLevel = 1;
        
        string levelScene = "Level 1-2";
        int actualLevelWorld = 1;
        int actualLevelLevel = 2;

        Debug.Log($"Now testing the scene {worldScene} with the following functions");
        HelpCheckResults("FindWord", _sp.FindFirstWord(worldScene, "World"), true);
        HelpCheckResults("FindLevelWord", _sp.FindLevelWord(worldScene), false);
        HelpCheckResults("FindWorldWord", _sp.FindWorldWord(worldScene), true);
        HelpCheckResults("FindStartingWord", _sp.FindStartingWord(worldScene), "World");
        HelpCheckResults("FindLevelFromScene", _sp.FindLevelFromScene(worldScene), actualWorldLevel);
        HelpCheckResults("FindWorldFromScene", _sp.FindWorldFromScene(worldScene), actualWorldWorld);
        HelpCheckResults("FindIfHasWorldLevel", _sp.FindIfHasWorldLevel(worldScene), true);

        Debug.Log($"Now testing the scene {levelScene} with the following functions");
        HelpCheckResults("FindWord", _sp.FindFirstWord(levelScene, "Level"), true);
        HelpCheckResults("FindLevelWord", _sp.FindLevelWord(levelScene), true);
        HelpCheckResults("FindWorldWord", _sp.FindWorldWord(levelScene), false);
        HelpCheckResults("FindStartingWord", _sp.FindStartingWord(levelScene), "Level");
        HelpCheckResults("FindLevelFromScene", _sp.FindLevelFromScene(levelScene), actualLevelLevel);
        HelpCheckResults("FindWorldFromScene", _sp.FindWorldFromScene(levelScene), actualLevelWorld);
        HelpCheckResults("FindIfHasWorldLevel", _sp.FindIfHasWorldLevel(levelScene), true);
    }


    // TODO: Condense functions with a wrapper (of some sort), so the argument results determines the
    // type of comparison
    private bool HelpCheckResults(string functionName, string results, string expectedResults)
    {
        Debug.Log($"[{functionName}] returned string result of [{results}]");
        if (results.Equals(expectedResults))
        {
            Debug.Log($"Results matches string of [{expectedResults}]!");
            return true;
        }
        Debug.Log($"Results doesn't match string of [{expectedResults}]!");
        return false;
    }

    private bool HelpCheckResults(string functionName, int results, int expectedResults)
    {
        Debug.Log($"[{functionName}] returned result int of [{results}]");
        if (results==expectedResults)
        {
            Debug.Log($"Integer results matches [{expectedResults}]!");
            return true;
        }
        Debug.Log($"Integer results doesn't match [{expectedResults}]!");
        return false;
    }

    private bool HelpCheckResults(string functionName, bool results, bool expectedResults)
    {
        Debug.Log($"[{functionName}] returned result Boolean of [{results}]");
        if (results == expectedResults)
        {
            Debug.Log($"Boolean results matches [{expectedResults}]!");
            return true;
        }
        Debug.Log($"Boolean results doesn't match [{expectedResults}]!");
        return false;
    }
}
