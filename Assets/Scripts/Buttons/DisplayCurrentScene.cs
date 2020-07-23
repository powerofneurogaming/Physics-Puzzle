using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Script is used for buttons or other game objects that have Text components
// to change the text to the scene name
public class DisplayCurrentScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        gameObject.GetComponent<Text>().text = currentSceneName;
       // GetComponent<Text>().text = ""
    }
}
