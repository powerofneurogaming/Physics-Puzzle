using UnityEngine;
using System.Collections;

public class QuitGame : MonoBehaviour
{

    public void QuitTheGame()
    {
        #if UNITY_EDITOR
        {
            // Application.Quit() does not work in the editor so
            // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
            Debug.Log("In the editor, so just going to stop playing.");
            UnityEditor.EditorApplication.isPlaying = false;
        }
        #else
         {
            Debug.Log("Quitting the game now");
            Application.Quit();
         }
        #endif
    }
}
