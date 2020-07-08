using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect1Page1 : MonoBehaviour
{
    public void GoToLevelSelect1Page1()
    {
        // TODO: Make 
        // TODO: check that scene exists
        Debug.Log("Going to first page of World 1");
        SceneManager.LoadScene("Level Select 1-1");
    }
}
