using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// using UnityEngine.Physics2DModule;

public class FlappyFatBirdBehavior2 : MonoBehaviour
{
	// Variables used in this class - should all start with underscore (_)
	// Local:
	// Currently not modified after Start function
	private Vector3 _initialPosition;
	private string _currentSceneName;
	// Modified
	private bool _objectWasLaunched;
	private float _timeSittingAround;
	// SerializeField enables variable to be adjusted in object inspector (under script variables.
	// The variable name also displays with first letter capped, and spacing beween the first cap and previous letter.
	[SerializeField] private float _launchPower = 500;
	
	
    // Functions automatically called as object behavior - MonoBehavior	
	
	// Called right before Start function, commonly used to set 
	private void Awake()
	{
		Debug.Log("Flappy Fat Bird has awakened!");
		// Set the position to position at start
		_currentSceneName = SceneManager.GetActiveScene().name;
		_initialPosition = transform.position;
		Debug.LogFormat("The initial position is {0}", _initialPosition);
	}
    // Start is called before the first frame update
    private void Start()
    {
        Debug.Log("Flappy Fat Bird has started!");
    }
	
	 
    // Update is called once per frame
    void Update()
    {
		// of the object is moving at an incredibly slow rate after launching (namely hitting another object)
		if(_objectWasLaunched &&
		GetComponent<Rigidbody2D>().velocity.magnitude <= 0.1) 
		{
			_timeSittingAround += Time.deltaTime; // deltaTiem is the second/framerate
		}
			
		// Reload scene when object moves too far up or down
        if(transform.position.x > 20 || 
		transform.position.x < -20 || 
		transform.position.y > 10 || 
		transform.position.y < -10 ||
		_timeSittingAround > 3)
			SceneManager.LoadScene(_currentSceneName); // reload current scene
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
		// Launch the object
		// Debug.LogFormat("The transform.position is {0}", transform.position);
		// Since miving the object back puts it in a negative position compared to the start and we want positive force
		// (in that case), we 
		Vector2 directionToInitialPosition = _initialPosition - transform.position;
		// Debug.LogFormat("The directionToInitialPosition is {0}", directionToInitialPosition);
		GetComponent<Rigidbody2D>().AddForce(directionToInitialPosition * _launchPower);
		GetComponent<Rigidbody2D>().gravityScale = 1;
		_objectWasLaunched = true;
    }
}

