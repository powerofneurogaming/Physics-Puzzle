using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class LevelController : MonoBehaviour
{
    // public variables
    // TODO: Variables set outside of this class, and altered outside
    public Text winText;
    // Counters for the
    public int projectilesLeft;
    public int targetsLeft;
    public int targetsHit = 0;
    // Private
    // Static for the one instance ever within the game - only starts at given value once.
    // TODO: Make index start from whichever level we're on
    private static int _nextLevelIndex = 1;
    // Modifyable from Unity editor
    [SerializeField]  private int targetMultiplyer = 100;
    [SerializeField] private int projectileMultiplyer = 100;
    [SerializeField] private int timeMultiplyer = 100;
    [SerializeField] private int targetTime = 120;

    private Target[] _targets;
    private float _totalTime = 0.0F;
    private bool _levelComplete = false;

    private void OnEnable()
    {
        _targets = FindObjectsOfType<Target>();
        targetsLeft = _targets.Count();
        winText.text = "";
    }

    // Start is called before the first frame update
    /* void Start()
    {
        
    } */

    // Update is called once per frame
    void Update()
    {
        // TODO: put checks in big if block, so that update only has to check the boolean value when level is done)
        // increment timer
        if (!_levelComplete)
        {
            _totalTime = Time.deltaTime;
        }
        // Check if there is at least one target in the list of target objects
        
        foreach (Target target in _targets)
        {
            if (target != null)
                return; // stops here if there is at least one target
        }
        // This part only reached when there are no targets found
        Debug.Log("All the targets are gone!");
        // We enable going to the next level
        if (!_levelComplete)
        {
            _levelComplete = true;
            winText.text = String.Format("You win!\n" +
                "Targets hit: {0}\n" +
                "Shots bonus: \n" +
                "Time bonus: \n" +
                "Total: ",
                targetsLeft);
        }
        // We go to the next level
        /*
        _nextLevelIndex++;
        string nextLevelName = "Level" + _nextLevelIndex;
        SceneManager.LoadScene(nextLevelName);
        */
    }

    private void SetButtonText(string buttonText)
    {
        int targetBonus = targetsHit * targetMultiplyer;
        int projectileBonus = projectilesLeft * projectileMultiplyer;
        float timeBonus = Math.Min(0.00F, targetTime - _totalTime);
        float totalScore = targetBonus + projectileBonus + timeBonus; 
        winText.text = $"{buttonText}\n"; /* +
            $"Targets hit: {targetsHit} x {targetMultiplyer} = {targetBonus}\n" +
            $"Shots bonus: {projectilesLeft} x {projectileMultiplyer}" +
            $"Time bonus: {timeBonus}" +
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
