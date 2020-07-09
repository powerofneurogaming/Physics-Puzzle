using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class LevelController : MonoBehaviour
{
    // public variables
    // TODO: Since button cannot be targeted, have button target variables in this class
   //  public Text winText;

    // Private
    // Static for the one instance ever within the game - only starts at given value once.
    // TODO: Make index start from whichever level we're on
    private static int _nextLevelIndex = 1;
    // Modifyable from Unity editor
    // Multiplyers for the score
    [SerializeField]  private int targetMultiplyer = 100;
    [SerializeField] private int projectileMultiplyer = 100;
    [SerializeField] private int timeMultiplyer = 100;
    [SerializeField] private int targetTime = 120;

    // Counters for the playable levels, with default values
    private int projectilesLeft = 6;
    private int targetsLeft = 1;
    private int targetsHit = 0;

    // For modifying the result button
    private GameObject _resultButton;
    private Text _resultText;

    // Used to target score
    private Target[] _targets;
    private float _totalTime = 0.0F;
    private bool _levelComplete = false;

    private void OnEnable()
    {
        _targets = FindObjectsOfType<Target>();
        targetsLeft = _targets.Count();
        // winText.text = "";
    }

    // Start is called before the first frame update
    private void Start()
    {
        // Button in this specific path, based on the Hierarchy view, will be returned.
        _resultButton = GameObject.Find("/Canvas/Result button");
        switch (_resultButton)
        {
            case null:
                Debug.Log("Could not find the Result button!");
                break;
            default:
                Debug.Log("Found the Result button!");
                _resultText = _resultButton.GetComponentInChildren<Text>();
                _resultButton.SetActive(false);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: put checks in big if block, so that update only has to check the boolean value when level is done)
        // increment timer
        if (!_levelComplete)
        {
            _totalTime = Time.deltaTime;
        }

        // TODO; Edit so that the number of projectiles
        // Check if there is at least one target in the list of target objects
        foreach (Target target in _targets)
        {
            if (target != null)
                return; // stops here if there is at least one target
        }
        // This part only reached when there are no targets found
        Debug.Log("All the targets are gone!");
        // We enable going to the next level
        //if (!_levelComplete)
        //{
            _levelComplete = true;
            /*
            winText.text = String.Format("You win!\n" +
                "Targets hit: {0}\n" +
                "Shots bonus: \n" +
                "Time bonus: \n" +
                "Total: ",
                targetsLeft);
            */
        //}
        // We go to the next level
        /*
        _nextLevelIndex++;
        string nextLevelName = "Level" + _nextLevelIndex;
        SceneManager.LoadScene(nextLevelName);
        */
    }

    public string MakeResultText(bool isWin)
    {
        // Calculate the level bonuses
        int targetBonus = targetsHit * targetMultiplyer;
        int projectileBonus = projectilesLeft * projectileMultiplyer;
        float timeLeft = Math.Min(0.00F, targetTime - _totalTime);
        float timeBonus = timeLeft * timeMultiplyer;
        float totalScore = targetBonus + projectileBonus + timeBonus;
        // Then assign them
        string resultText;
        if (isWin)
            resultText = "You Win!";
        else
            resultText = "You lose!";
        // resultText = $"{buttonText}\n";
        resultText += $"\nTargets hit: {targetsHit} x {targetMultiplyer} = {targetBonus}\n";
        _ = $"Shots bonus: {projectilesLeft} x {projectileMultiplyer} = {projectileBonus}\n";
        _ = $"Time bonus: {timeBonus} (target time of {targetTime}\n";
        _ = $"Total: {totalScore}";
        if (isWin)
        {
            resultText = "Click to next level";
        }
        else
        {
            // TODO : Display text based on required win conditions, or auto lose condition
            resultText = "Click to restart";
        }
        Debug.LogFormat("SetResult text returning a string of {0}", resultText);
        return resultText;
        // winText.text = resultText; 
        /* +
            $"Targets hit: {targetsHit} x {targetMultiplyer} = {targetBonus}\n" +
            $"Shots bonus: {projectilesLeft} x {projectileMultiplyer}\n" +
            $"Time bonus: {timeBonus}\n" +
            $"Total: {totalScore}";
        */

        /*
        winText.text = String.Format("{0}\n" +
                "Targets hit: {1} x {} = {2}\n" +
                "Shots bonus: {2}\n" +
                "Time bonus: {3}\n" +
                "Total: ",
                targetsLeft);
        */
    }
}
