using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DisplayCurrentWorld : MonoBehaviour
{
    // Variables used in class
    // public
    // public SceneController sceneController;
    // Private
    private SceneController _sc;
    private bool _worldTextSet = false;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Display current world has started!");
        GameObject sceneObject;
        // _sc = GameObject.FindObjectOfType<SceneController>().GetComponent<SceneController>();
        // do
        //{
            sceneObject = GameObject.Find("Scene Controller"); // .GetComponent<SceneController>();
        // } while (sceneObject == null);
        if (sceneObject != null)
            Debug.Log("Found Scene Controller object!");
        else
            Debug.Log("Didn't find Scene controller object...");
        _sc = sceneObject.GetComponent<SceneController>();
        if (_sc != null)
        {
            Debug.Log("Found Scene controller!");
            /*
            int worldNo = _sc.getWorld();
            while( worldNo <0)
                worldNo = _sc.getWorld();

            string worldName = "Level Select\n" +
            $"(World {_sc.getWorld()})"; // $"(World {_sc.getWorld()})";
                                         // $"(World {sceneController.getWorld()})"; // $"(World {_sc.getWorld()})";
            Text displayedText = GetComponent<Text>();
            // gameObject.GetComponent<Text>()
            displayedText.text = worldName;
            // GetComponent<Text>().text = ""
            */
        }
        else
            Debug.Log("Didn't find Scene controller...");  
    }

    private void Update()
    {
        if(!_worldTextSet)
        {
            int worldNo = _sc.getWorld();
            if (worldNo >= 0)
            {
                Text displayedText = gameObject.GetComponent<Text>();
                // gameObject.GetComponent<Text>()
                string worldName = "Level Select\n" +
                    $"(World {_sc.getWorld()})";

                displayedText.text = worldName;
                _worldTextSet = true;
                Debug.Log("Set the displayed world as {_worldName}!");
            }

        }
        
    }
}
