using System.Collections;
using System.Collections.Generic;
// using System.Diagnostics; // For Debug static class
using UnityEngine;

public class FlappyFatBirdBehavior : MonoBehaviour
{
    // Functions automatically called as object behavior - MonoBehavior
    // Start is called before the first frame update
    private void Start()
    {
        Debug.Log("Flappy Fat Bird has started!");
    }

    // When the bird is clicked
    private void OnMouseDown()
    {
        // Turn it red\
        // TODO: Replace below GetComponentSpriteRender line & OnMouseUp with helper
        GetComponent<SpriteRenderer>().color = Color.red;
    }

    // While the bird remains clicked
    private void OnMouseDrag()
    {
        // ScreenToWorld converts the position, returning a Vector3 (x, y, z)
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		transform.position = new Vector3(newPosition.x, newPosition.y, 1);
		
        // Changes the position, but the position is based on the camera's z position so the bird fades into it. 
        // transform.position = newPosition;

        // Moves bird in front and above mouse.
        // transform.position = Input.mousePosition; // mouse position is from the bottom-left corner while object position is from center of screen
    }

    // When the mouse button is released - after clicking
    private void OnMouseUp()
    {
        // Turn it white (original color)
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    /*
    // Update is called once per frame
    void Update()
    {
        
    }
    */
}
