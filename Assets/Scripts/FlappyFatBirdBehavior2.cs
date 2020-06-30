using UnityEngine;
using UnityEngine.SceneManagement;
// using UnityEngine.Physics2DModule;

public class FlappyFatBirdBehavior2 : MonoBehaviour
{
	// Variables used in this class - should all start with underscore (_)
	// Local:
	// Currently not modified after Start function
	// SerializeField enables variable to be adjusted in object inspector (under script variables.
	// The variable name also displays with first letter capped, and spacing beween the first cap and previous letter.
	[SerializeField] private float _launchPower = 500;
	[SerializeField] private string _objectName = "Projectile";
	private Vector3 _initialPosition;
	private string _currentSceneName;
	private Rigidbody2D _rb2d;
	private LineRenderer _lr;
	private SpriteRenderer _sr;
	// Modified after start
	private bool _objectWasLaunched;
	private float _timeSittingAround;
	
    // Functions automatically called as object behavior - MonoBehavior	
	
	// Called right before Start function, commonly used to set 
	private void Awake()
	{
		Debug.LogFormat("{0} has awakened!", _objectName);
		// Assign the GeComponent to the variables, so there aren't so many GeComponent calls.
		_rb2d = GetComponent<Rigidbody2D>();
		_lr = GetComponent<LineRenderer>();
		_sr = GetComponent<SpriteRenderer>();
		
		_currentSceneName = SceneManager.GetActiveScene().name;
		// Set the position to position at start
		_initialPosition = transform.position;
		Debug.LogFormat("The initial position is {0}", _initialPosition);
	}
    // Start is called before the first frame update
    private void Start()
    {
		Debug.LogFormat("{0} has started!", _objectName);
	}
	
	 
    // Update is called once per frame
    void Update()
    {
		_lr.SetPosition(0, transform.position);
		_lr.SetPosition(1, _initialPosition);
		
		
		// of the object is moving at an incredibly slow rate after launching (namely hitting another object)
		if(_objectWasLaunched &&
		_rb2d.velocity.magnitude <= 0.1) 
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
		// Enable line
		_lr.enabled = true;
        // Turn it red
        _sr.color = Color.red;
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
		// Disable line
		_lr.enabled = false;
        // Turn it white (original color)
        _sr.color = Color.white;
		// Launch the object
		// Debug.LogFormat("The transform.position is {0}", transform.position);
		// Since miving the object back puts it in a negative position compared to the start and we want positive force
		// (in that case), we 
		Vector2 directionToInitialPosition = _initialPosition - transform.position;
		// Debug.LogFormat("The directionToInitialPosition is {0}", directionToInitialPosition);
		_rb2d.AddForce(directionToInitialPosition * _launchPower);
		_rb2d.gravityScale = 1;
		_objectWasLaunched = true;
    }
}

