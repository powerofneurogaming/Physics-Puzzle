using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Projectile : MonoBehaviour
{
    // Variables used in this class - should all start with underscore (_)
    // Public:
    // External objects to modify
    public Player player;
	public LevelController lc;
   // public UnityEngine.UI.Button winButton;
	// Local:
	// Currently not modified after Start function
	// SerializeField enables variable to be adjusted in object inspector (under script variables.
	// The variable name also displays with first letter capped, and spacing beween the first cap and previous letter.
	// [SerializeField] private int _projectilesLeft = 5;
	[SerializeField] private float _launchPower = 500;
	[SerializeField] private float _projectileElevation = 5;
	// TODO: Remove _objectName and just use tags instead.
	[SerializeField] private string _objectName = "Projectile";
	private Vector3 _launchingPoint, _playerPosition;
    private string _currentSceneName;
	private Rigidbody2D _rb2d;
	private LineRenderer _lr;
	private SpriteRenderer _sr;
	// Modified after start

	private bool _objectWasLaunched, _objectDragged;
	private float _timeSittingAround;
    private int _launchCount;

	// Functions automatically called as object behavior - MonoBehavior	

	// Called right before Start function, commonly used to set 
	private void Awake()
	{
		Debug.LogFormat("{0} has awakened!", _objectName);
        Debug.LogFormat("Object name of {0}", name);
		// Assign the GeComponent to the variables, so there aren't so many GeComponent calls.
		_rb2d = GetComponent<Rigidbody2D>();
        _lr = GetComponent<LineRenderer>();
        _sr = GetComponent<SpriteRenderer>();

		_launchCount = 0;
		_currentSceneName = SceneManager.GetActiveScene().name;
		// Set the position to position at start
		//Vector3 newPosition = Camera.main.ScreenToWorldPoint(player.transform.position); //.x, player.transform.y, player.transform.z);
		// transform.position = newPosition; //new Vector3(newPosition.x, newPosition.y, 1);
		PutProjectileAbovePlayer();
		// transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 10, 1);
		Debug.LogFormat("The initial position is {0}", _launchingPoint);
	}
    // Start is called before the first frame update
    private void Start()
    {
		Debug.LogFormat("{0} has started!", _objectName);
		Debug.LogFormat("Object name of {0}", name);
	}


	// Update is called once per frame
	void Update()
	{
		SetProjectileLaunchingPoint();
		// We generate a line based on these two positions
		_lr.SetPosition(0, transform.position);
		_lr.SetPosition(1, _launchingPoint);


		// of the object is moving at an incredibly slow rate after launching (namely hitting another object)
		if (_objectWasLaunched)
		{
			if (_rb2d.velocity.magnitude <= 0.1)
			{
				_timeSittingAround += Time.deltaTime; // deltaTime is the second/framerate
			}
		}
		else if(!_objectDragged)
        {
			PutProjectileAbovePlayer();
		}

		// Reset position when object moves too far to the side or down, or effectively stops moving.
		// TODO: Make x ( and possibly y) range either wider, dependent on a moving camera, or both.
		if (transform.position.y < -10 ||
		_timeSittingAround > 3)  // transform.position.x > 20 ||transform.position.x < -20 ||
		{ // transform.position.y > 10 ||
            ResetProjectile(); 
			// transform.position = _launchingPoint;  // Move back to the starting position
			// SceneManager.LoadScene(_currentSceneName); // reload current scene
        }
	}

	// When the projectile is clicked
	private void OnMouseDown()
    {
		if (!_objectWasLaunched)
		{
			// Enable line
			_lr.enabled = true;
			// Turn it red
			_sr.color = Color.red;
		}
    }

    // While the projectile remains clicked
    private void OnMouseDrag()
    {
		if (!_objectWasLaunched)
		{
			// Move the object when click and dragged.
			// ScreenToWorld converts the position, returning a Vector3 (x, y, z)
			_objectDragged = true;
			Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			transform.position = new Vector3(newPosition.x, newPosition.y, 1);
		}
		
        // Changes the position, but the position is based on the camera's z position so the bird fades into it. 
        // transform.position = newPosition;

        // Moves bird in front and above mouse.
        // transform.position = Input.mousePosition; // mouse position is from the bottom-left corner while object position is from center of screen
    }

    // When the mouse button is released - after clicking
    private void OnMouseUp()
    {
		if (!_objectWasLaunched)
		{
			// Disable line
			_lr.enabled = false;
			// Turn it white (original color)
			_sr.color = Color.white;
			_launchCount++;
			Debug.LogFormat("The projectile has been launched {0} times now", _launchCount);
			// Launch the object
			// Debug.LogFormat("The transform.position is {0}", transform.position);
			// Since miving the object back puts it in a negative position compared to the start and we want positive force
			// (in that case), we 
			Vector2 directionToInitialPosition = _launchingPoint - transform.position;
			// Debug.LogFormat("The directionToInitialPosition is {0}", directionToInitialPosition);
			_rb2d.AddForce(directionToInitialPosition * _launchPower);
			_rb2d.gravityScale = 1;
			_objectWasLaunched = true;
		}
    }

	/* 
	 * Sets the starting point for launching the projectile,
	 * based off of the player's position.
	 */
	private void SetProjectileLaunchingPoint()
    {
		// Vector3 newPosition = new Vector3(player.transform.position.x, player.transform.position.y + 10, player.transform.position0.z);
		// transform.position = newPosition;
		_launchingPoint = new Vector3(player.transform.position.x, player.transform.position.y + _projectileElevation, player.transform.position.z);
	}

	private void MoveProjectileToLaunchingPoint()
    {
		transform.position = _launchingPoint;
	}

	/*
	 * Moves the projectile above the player, after calculating said position ( _launchingPoint)
	 */
	private void PutProjectileAbovePlayer()
    {
        // TODO: changes only when directional keys are held down - only needed when player is moving
        SetProjectileLaunchingPoint();
		MoveProjectileToLaunchingPoint();
	}

    // Non-Monobehavior functions
    /* 
     * Resets the position of the projectile, so it can be launched again
     */
    private void ResetProjectile()
	{
		int projectilesLeft = lc.DecrementProjectiles();
        Debug.Log($"There are {projectilesLeft} projectiles left");
		// TODO: Deactivate projectiles when there are none left
		// Move back to the starting position
		transform.position = _launchingPoint;
		_objectWasLaunched = false;
		_objectDragged = false;
		_timeSittingAround = 0.0F;
		// Set it still
		_rb2d.gravityScale = 0;
		_rb2d.velocity = new Vector3(0, 0, 0);
		_rb2d.angularVelocity = 0.0F; // new Vector3(0, 0, 0);
		transform.rotation = Quaternion.Euler(0, 0, 0);
	}
}

