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
    private int _projectilesLeft = 6;
    private int _targetsLeft = 3;
    private int _targetsHit = 0;

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
        _targetsLeft = _targets.Count();
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
                _targetsLeft);
            */
        //}
        // We go to the next level
        /*
        _nextLevelIndex++;
        string nextLevelName = "Level" + _nextLevelIndex;
        SceneManager.LoadScene(nextLevelName);
        */
    }

    public void DecrementTargets()
    {
        _targetsLeft--;
        if (_targetsLeft <= 0)
            SetResultsButton(isWin: true, resultReason: "You fed all the customers!");
    }

    private void SetResultsButton(bool isWin, string resultReason)
    {
        _resultButton.SetActive(true);
        _resultText.text = MakeResultText(isWin, resultReason);
        // _resultButton.GetComponentInChildren<Text>().text = MakeResultText(isWin, resultReason);
        // _resultButton.GetComponentText.text = MakeResultText(isWin, resultReason);
        _levelComplete = true;
    }

    private string MakeResultText(bool isWin, string resultReason)
    {
        // Calculate the level bonuses
        int targetBonus = _targetsHit * targetMultiplyer;
        int projectileBonus = _projectilesLeft * projectileMultiplyer;
        float timeLeft = Math.Min(0.00F, targetTime - _totalTime);
        float timeBonus;
        if (isWin)
            timeBonus = timeLeft * timeMultiplyer;
        else
            timeBonus = 0;
        float totalScore = targetBonus + projectileBonus + timeBonus;
        // Then assign them
        string resultText;
        if (isWin)
            resultText = "You Win!";
        else
            resultText = "You lose!";
        // resultText = $"{buttonText}\n";
        // TODO: condense code into one continuous line of appends, if possible.
        resultText = resultText + resultReason;
        resultText = resultText + $"\nTargets hit: {_targetsHit} x {targetMultiplyer} = {targetBonus}\n";
        resultText = resultText + $"Shots bonus: {_projectilesLeft} x {projectileMultiplyer} = {projectileBonus}\n";
        if(isWin)
            resultText = resultText + $"Time bonus: {timeBonus} (target time of {targetTime}\n";
        resultText = resultText + $"Total: {totalScore}";

        if (isWin)
        {
            resultText = resultText + "Click for next level";
        }
        else
        {
            // TODO : Display text based on required win conditions, or auto lose condition
            resultText = resultText + "Click to restart";
        }
        Debug.LogFormat("SetResult text returning a string of {0}", resultText);
        return resultText;
        // winText.text = resultText; 
        /* +
            $"Targets hit: {_targetsHit} x {targetMultiplyer} = {targetBonus}\n" +
            $"Shots bonus: {_projectilesLeft} x {projectileMultiplyer}\n" +
            $"Time bonus: {timeBonus}\n" +
            $"Total: {totalScore}";
        */

        /*
        winText.text = String.Format("{0}\n" +
                "Targets hit: {1} x {} = {2}\n" +
                "Shots bonus: {2}\n" +
                "Time bonus: {3}\n" +
                "Total: ",
                _targetsLeft);
        */
    }
}
