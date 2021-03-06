﻿using System;
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
    /*
    public Text resultsText; // Appears when level is lossed or won
    public Text projectilesLeftText, targetsLeftText; // Displayed in buttons
    public UnityEngine.UI.Button resultButton; // For making the button disappear, reappear, & change behavior
    // public ReloadButton reloadButton;
    public SceneController sceneController;
    public ReloadButton reloadButton;
    */

    // Private
    // Static for the one instance ever within the game - only starts at given value once.
    // TODO: Make index start from whichever level we're on
    // private static int _nextLevelIndex = 1;
    // Modifyable from Unity editor
    // Multipliers for the score
    // free-play/practice, very easy, easy, normal, hard, very hard, near impossible - from 0 to 6
    [SerializeField] private int difficulty = 3; // normal
    [SerializeField] private int targetMultiplier = 100;
    [SerializeField] private int projectileMultiplier = 100;
    [SerializeField] private int timeMultiplier = 100;
    [SerializeField] private int targetTime = 120;

    private ObjectFinder _of = new ObjectFinder();
    // private ReloadButton _rb = new ReloadButton();
    public Text resultsText; // Appears when level is lossed or won
    public Text projectilesLeftText, targetsLeftText; // Displayed in buttons
    public UnityEngine.UI.Button resultButton; // For making the button disappear, reappear, & change behavior
    // private ReloadButton reloadButton;
    public SceneController sceneController;
    public ReloadButton reloadButton;

    private int _projectilesPerTargets = 3; // 6 - difficulty
    // Counters for the playable levels, with default values
    // TODO: Assign number  of projectiles relative to the number of targets, and difficulty setting
    // _targetsLeft changes based on the actual number of targets in the scene during Start()
    private int _projectilesStart = 0; //9;
    private int _projectilesLeft = 0; //9;
    private int _targetsLeft = 0; //3;
    private int _targetsStart = 0; //3;
    private int _targetsHit = 0;

    // Used to target score
    private Target[] _targets;
    private float _totalTime = 0.0F;
    private bool _levelComplete = false;

    public int GetTargetsCount() { return _targetsLeft; }
    public int GetProjectilesCount() { return _projectilesLeft; }

    #region someRegion 
    // Some comment
    #endregion
    /*
     * Function used for newly-spawned targets
     */
    public int IncrementTargets()
    {
        //
        _targetsLeft++;
        _targetsStart++;
        // To prevent a race condition with the starts, I made the projectile assignment calculate here.
        _projectilesPerTargets = Math.Max(1, 6 - difficulty); 
        _projectilesLeft += _projectilesPerTargets;
        _projectilesStart += _projectilesPerTargets;

        //SetCustomerCountText();
        // SetFoodCountText();

        return _targetsLeft;
    }

    /*
     * Used to increment the number of targets and update the button texts.
     * Used after the Awake and Start behaviors as the text may not be initialized (race conditions)
     */
    public int IncrementTargetsLate()
    {
        //
        IncrementTargets();

        SetCustomerCountText();
        SetFoodCountText();

        return _targetsLeft;
    }

    /*
     * Returns the number of targets left, after the reduction
     */
    public int DecrementTargets()
    {
        _targetsLeft--;
        _targetsHit++;
        SetCustomerCountText();
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
        SetFoodCountText();

        if (_projectilesLeft <= 0) // TODO: Change message to projectiles, if targets also are not "food eaters"
        {
            Debug.Log("Projectiles counter has gone to 0 or below!");
            SetResultsButton(isWin: false, resultReason: "You ran out of food!");
        }
        /*else if(_projectilesLeft < _targetsLeft)
        {
            Debug.Log($"Not enough food for the {_targetsLeft} customers!\n" +
                $"You only have {_projectilesLeft} pieces of food!");
            SetResultsButton(isWin: false, resultReason: "Not enough food!");
        } */
        return _projectilesLeft;
    }

    public void SetResultsButton(bool isWin, string resultReason)
    {
        resultButton.gameObject.SetActive(true);

        resultsText.text = MakeResultText(isWin, resultReason);
        _levelComplete = true;
    }

    private void SetCountdownText(Text textBox, int countCurrent, int countStart, string counterType = "Customers")
    {
        // projectilesLeftText.text = $"Projectiles left: {_projectilesLeft} / {_projectilesStart}";
        textBox.text = $"{counterType} left: {countCurrent} / {countStart}";
    }

    private void SetFoodCountText()
    {
        SetCountdownText(textBox: projectilesLeftText, countCurrent: _projectilesLeft, countStart: _projectilesStart, counterType: "Food");
        return;
    }

    private void SetCustomerCountText()
    {
        // targetsLeftText.text = $"Customers left: {_targetsLeft} / {_targetsStart}";
        SetCountdownText(textBox: targetsLeftText, countCurrent: _targetsLeft, countStart: _targetsStart, counterType: "Customers");
        return;
    }

    private void SetCountTexts()
    {
        SetFoodCountText();
        SetCustomerCountText();
    }

    private void OnEnable()
    {
        // _resultText = resultButton.GetComponentInChildren<Text>();
        // winText.text = "";
    }

    // Start is called before the first frame update
    private void Start()
    {
        Debug.Log("Level Controller has started!");
        Debug.LogFormat("Object name of {0}", name);
        // _projectilesPerTargets = Math.Max(1, 6 - difficulty);
        /*
        _targets = FindObjectsOfType<Target>();
        _targetsLeft = _targetsStart = _targets.Count();        
        _projectilesLeft = _projectilesStart = _targetsLeft * _projectilesPerTargets;
        */
        GameObject sceneObject; // = 
        if (sceneController == null)
        {
            sceneController = FindObjectOfType<SceneController>();
            if (sceneController == null)
            {
                if (_of.FindObject("Scene Controller", out sceneObject))//GameObject.Find("Scene Controller");
                    sceneController = sceneObject.GetComponent<SceneController>();
            }
            else
                Debug.Log("SceneController has been found by FindObjectOfType!");
        }
        // Get the canvas for all the button related stuff
        //  GameObject canvasObject = GameObject.Find("Canvas");
        // buttons
        if (resultButton == null)
        {
            if (_of.FindCanvasObject("Results", out sceneObject))//GameObject.Find("Scene Controller");
                resultButton = sceneObject.GetComponent<UnityEngine.UI.Button>();
        }
        // sceneObject = GameObject.Find("Results");
        if (reloadButton == null)
        {
            if (_of.FindCanvasObject("Reset", out sceneObject))//GameObject.Find("Scene Controller");
                reloadButton = sceneObject.GetComponent<ReloadButton>();
        }
        // sceneObject = GameObject.Find("Reset");
        // Texts from buttons
        if (_of.FindButtonChild("Results", "Win_Lose", out sceneObject)) //GameObject.Find("Scene Controller");
            resultsText = sceneObject.GetComponent<Text>();
        if (_of.FindButtonChild("Projectile Count", "Projectile Count", out sceneObject)) //GameObject.Find("Scene Controller");
            projectilesLeftText = sceneObject.GetComponent<Text>();
        if (_of.FindButtonChild("Customers Left", "Customers Left", out sceneObject)) //GameObject.Find("Scene Controller");
            targetsLeftText = sceneObject.GetComponent<Text>();
        /*
        sceneObject = GameObject.Find("Win_Lose");
        sceneObject = GameObject.Find("Projectile Count");
        sceneObject = GameObject.Find("Customers Left");
        */

        SetCountTexts();

        // Button in this specific path, based on the Hierarchy view, will be returned.
        // resultButton = GameObject.Find("/Canvas/Result button");
        switch (resultButton)
        {
            case null:
                Debug.LogError("Could not find the Result button!");
                // Debug.Log("Could not find the Result button!");
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
        }
    }

    private void LateUpdate()
    {
        SetCountTexts();
    }

    private string MakeResultText(bool isWin, string resultReason)
    {
        // Calculate the level bonuses
        int targetBonus = _targetsHit * targetMultiplier;
        int projectileBonus = Math.Max(0, _projectilesLeft) * projectileMultiplier;
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
            resultsText = "Game Over!";
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
            resultsText = resultsText + $"You need to feed {_targetsLeft} more people next time!\n";
        }

        resultsText = resultsText + $"Difficulty multiplier: {subTotal} X {difficulty}\n" +
            $"Total: {totalScore}\n\n";
        // resultsText = resultsText + $"";

        if (isWin)
        {
            resultsText = resultsText + "Click for next level";
            // TODO: Finish method for loading next level relative to scene
            resultButton.onClick.AddListener(() => sceneController.GoToNextLevel());
        }
        else
        {
            // TODO : Display text based on required win conditions, or auto lose condition
            resultsText = resultsText + "Click to restart";
            resultButton.onClick.AddListener(()=> reloadButton.GetComponent<ReloadButton>().RestartLevel());
        }
        Debug.LogFormat("SetResult text returning a string of {0}", resultsText);
        return resultsText;
    }
}
