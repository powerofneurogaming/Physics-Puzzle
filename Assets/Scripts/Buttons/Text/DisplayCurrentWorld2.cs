using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Changes the text to "World {worldNo}" upon the scene loading
public class DisplayCurrentWorld2 : MonoBehaviour
{
    public SceneController sceneController;
    // private SceneController _sc; 
    // Start is called before the first frame update
    void Start()
    {
        // _sc = GameObject.FindObjectOfType<SceneController>().GetComponent<SceneController>();
        // _sc = GameObject.Find("Scene Controller").GetComponent<SceneController>();
        string worldName = $"(World {sceneController.getWorld()})"; // $"(World {_sc.getWorld()})";
        Text displayedText = GetComponent<Text>();
        // gameObject.GetComponent<Text>()
        displayedText.text = worldName;
        // GetComponent<Text>().text = ""
    }
}
