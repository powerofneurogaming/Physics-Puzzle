using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    // Static for the one instance ever within the game - only starts at given value once.
    // TODO: Make index start from whichever level we're on
    private static int _nextLevelIndex = 1;
    private Target[] _targets;

    private void OnEnable()
    {
        _targets = FindObjectsOfType<Target>();
    }
    // Start is called before the first frame update
    /* void Start()
    {
        
    } */

    // Update is called once per frame
    void Update()
    {
        // Check if there is at least one target in the list of target objects
        foreach (Target target in _targets)
        {
            if (target != null)
                return; // stops here if there is at least one target
        }
        // This part only reached when there are no targets found
        Debug.Log("All the targets are gone!");
        // We go to the next level
        _nextLevelIndex++;
        string nextLevelName = "Level" + _nextLevelIndex;
        SceneManager.LoadScene(nextLevelName);
    }
}
