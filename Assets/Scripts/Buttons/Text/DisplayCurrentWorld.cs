using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DisplayCurrentWorld : MonoBehaviour
{
    public SceneController sceneController;
    private SceneController _sc; 
    // Start is called before the first frame update
    void Start()
    {
        // _sc = GameObject.FindObjectOfType<SceneController>().GetComponent<SceneController>();
        do
        {
            _sc = GameObject.Find("Scene Controller").GetComponent<SceneController>();
        } while (_sc == null);
        string worldName = "Level Select\n" +
             $"(World {sceneController.getWorld()})"; // $"(World {_sc.getWorld()})";
        Text displayedText = GetComponent<Text>();
        // gameObject.GetComponent<Text>()
        displayedText.text = worldName;
        // GetComponent<Text>().text = ""
    }
}
