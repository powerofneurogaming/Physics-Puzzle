using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class LevelController : MonoBehaviour
{
    // public variables
    // TODO: Since button cannot be targeted, have button target variables in this class
    public Text resultsText, projectilesLeft;
    public UnityEngine.UI.Button resultButton; 
    public ReloadButton reloadButton;
    public WorldController worldController;

    // Private
    // Static for the one instance ever within the game - only starts at given value once.
    // TODO: Make index start from whichever level we're on
    // private static int _nextLevelIndex = 1;
    // Modifyable from Unity editor
    // Multipliers for the score
    // free-play/practice, very easy, easy, normal, hard, very hard, near impossible - from 0 to 6
    [SerializeField] private int difficulty = 3; // normal
    [SerializeField]  private int targetMultiplier = 100;
    [SerializeField] private int projectileMultiplier = 100;
    [SerializeField] private int timeMultiplier = 100;
    [SerializeField] private int targetTime = 120;

    private int _projectilesPerTargets = 3; // 6 - difficulty
    // Counters for the playable levels, with default values
    // TODO: Assign number  of projectiles relative to the number of targets, and difficulty setting
    // _targetsLeft changes based on the actual number of targets in the scene during Start()
    private int _projectilesLeft = 9;
    private int _targetsLeft = 3;
    private int _targetsHit = 0;

    // For modifying the result button
    // private GameObject _resultButton;
    // private Text
    private TextMeshProUGUI _resultText;

    // Used to target score
    private Target[] _targets;
    private float _totalTime = 0.0F;
    private bool _levelComplete = false;

    public int GetTargetsCount(){ return _targetsLeft; }
    public int GetProjectilesCount() { return _projectilesLeft; }
    /*
     * Returns the number of targets left, after the reduction
     */
    public int DecrementTargets()
    {
        _targetsLeft--;
        _targetsHit++;
        if (_targetsLeft <= 0)
        {
            Debug.Log("targets counter has gone to 0 or below!");
            SetResultsButton(isWin: true, resultReason: "You fed all the customers!");
        }

        return _targetsLeft;
    }

    public int DecrementProjectiles()
    {
        _projectilesLeft--;
        if (_projectilesLeft <= 0) // TODO: Change message to projectiles, if targets also are not "food eaters"
        {
            Debug.Log("Projectiles counter has gone to 0 or below!");
            SetResultsButton(isWin: false, resultReason: "You ran out of food!");
        }
        return _projectilesLeft;
    }

    private void OnEnable()
    {
        // _resultText = resultButton.GetComponentInChildren<Text>();
        // winText.text = "";
    }

    // Start is called before the first frame update
    private void Start()
    {
        _targets = FindObjectsOfType<Target>();
        _targetsLeft = _targets.Count();
        _projectilesPerTargets = 6 - difficulty;
        _projectilesLeft = _targetsLeft * _projectilesPerTargets;
        // Button in this specific path, based on the Hierarchy view, will be returned.
        // resultButton = GameObject.Find("/Canvas/Result button");
        switch (resultButton)
        {
            case null:
                Debug.Log("Could not find the Result button!");
                break;
            default:
                Debug.Log("Found the Result button!");
                // _resultText = resultButton.GetComponentInChildren<Text>();
                resultButton.gameObject.SetActive(false);
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
            // Debug.Log($"Level not yet completed. Time: [{_totalTime}]");
            _totalTime += Time.deltaTime;

            // TODO; Edit to check the number of projectiles
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
        }
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

    private void SetResultsButton(bool isWin, string resultReason)
    {
        resultButton.gameObject.SetActive(true);

        resultsText.text = MakeResultText(isWin, resultReason);

        // _resultText = resultButton.GetComponentInChildren<TextMeshProUGUI>();
        // Problem!: Cannot reference the button text properly. Keep getting the following error

        // NullReferenceException: Object reference not set to an instance of an object LevelController.SetResultsButton(System.Boolean isWin, System.String resultReason);
        // GameObject.Find("Result button/Result text").GetComponent<Text>().text = MakeResultText(isWin, resultReason);
        // resultButton.GetComponentInChildren<Text>().text = MakeResultText(isWin, resultReason); // NullReferenceException: Object reference not set to an instance of an object LevelController.SetResultsButton(System.Boolean isWin, System.String resultReason);
        // GameObject.Find("Result button/Result text").GetComponent<Text>().text = MakeResultText(isWin, resultReason);
        // GameObject.Find("Result button").GetComponentInChildren<Text>().text = MakeResultText(isWin, resultReason);
        // Ended testing here...
        // _resultText.text = MakeResultText(isWin, resultReason);
        // resultButton.GetComponentInChildren<Text>().text = MakeResultText(isWin, resultReason);
        //resultButton.GetComponent<Text>().text = MakeResultText(isWin, resultReason);
        _levelComplete = true;
    }

    private string MakeResultText(bool isWin, string resultReason)
    {
        // Calculate the level bonuses
        int targetBonus = _targetsHit * targetMultiplier;
        int projectileBonus = _projectilesLeft * projectileMultiplier;
        float timeLeft = Math.Max(0.00F, targetTime - _totalTime);
        float timeBonus;
        if (isWin)
            timeBonus = timeLeft * timeMultiplier;
        else
            timeBonus = 0;
        float subTotal = targetBonus + projectileBonus + timeBonus;
        float totalScore = subTotal * difficulty;
        // Then put them in a string
        string resultsText;
        if (isWin)
            resultsText = "You Win!";
        else
            resultsText = "Game over!";
        // resultText = $"{buttonText}\n";
        // TODO: condense code into one continuous line of appends, if possible.
        resultsText = resultsText + $"\n{resultReason}";
        resultsText = resultsText + $"\nTargets hit: {_targetsHit} x {targetMultiplier} = {targetBonus}\n";

        if (isWin)
        {
            resultsText = resultsText + $"Shots bonus: {_projectilesLeft} x {projectileMultiplier} = {projectileBonus}\n";
            resultsText = resultsText + "Time bonus: ";
            if (_totalTime >= targetTime) // don't give the negative time - just give the time taken
                resultsText = resultsText + $"0! Time taken: {_totalTime} seconds\n"
                    + "You'll have to be quicker for this!";
            else
                resultsText = resultsText + $"({targetTime} - {_totalTime}) seconds X {timeMultiplier} = {timeBonus}";
            resultsText = resultsText + $" (target time of {targetTime} seconds)\n";
        }
        else
        {
            resultsText = resultsText + $"Time taken: {_totalTime} seconds\n";
            resultsText = $"You need to feed {_targetsLeft} more people next time!\n";
        }

        resultsText = resultsText + $"Difficulty multiplier: {subTotal} X {difficulty}\n" +
            $"Total: {totalScore}\n\n";
        // resultsText = resultsText + $"";

        if (isWin)
        {
            resultsText = resultsText + "Click for next level";
            // TODO: Finish method for loading next level relative to scene
            // resultButton.onClick.AddListener();
        }
        else
        {
            // TODO : Display text based on required win conditions, or auto lose condition
            resultsText = resultsText + "Click to restart";
        }
        Debug.LogFormat("SetResult text returning a string of {0}", resultsText);
        return resultsText;
        // winText.text = resultText; 
        /* +
            $"Targets hit: {_targetsHit} x {targetMultiplier} = {targetBonus}\n" +
            $"Shots bonus: {_projectilesLeft} x {projectileMultiplier}\n" +
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
