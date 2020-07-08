using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldSelect : MonoBehaviour
{
    public void GoToWorldSelect()
    {
        // TODO: check that scene exists
        Debug.Log("Going to world select scene");
        SceneManager.LoadScene("World Select");
    }
    // Update is called once per frame
    /*void Update()
    {
        
    } */
}
