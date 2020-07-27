using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DisplayCurrentWorld : MonoBehaviour
{
    public SceneController sceneController;
    // Start is called before the first frame update
    void Start()
    {
        string worldName = "Level Select\n" +
            $"(World {sceneController.getWorld()})";
        Text displayedText = GetComponent<Text>();
        // gameObject.GetComponent<Text>()
        displayedText.text = worldName;
        // GetComponent<Text>().text = ""
    }
}
