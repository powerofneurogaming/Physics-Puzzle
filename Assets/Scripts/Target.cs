using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.Physics2DModule;

public class Target : MonoBehaviour
{
	//Variables
	// Public
	// public LevelController levelController;
	//Private
	[SerializeField] private GameObject _cloudParticlePrefab = null;
	private LevelController _lc;

    private void Start()
    {
		Debug.Log("Starting up a target!");
		Debug.LogFormat("Object name of {0}", name);
		// GameObject 
		_lc = GameObject.Find("Level Controller").GetComponent<LevelController>();
		//if (_lc == null)
			// Debug.LogError("Error! Target didn't find Level Controller!");
		_lc.IncrementTargets();
    }
    private void OnCollisionEnter2D(Collision2D collision)
	{
		Debug.Log("Collision started!");
		Projectile projectile = collision.collider.GetComponent<Projectile>();
		// Checks if the object is of defined type (in sharp brackets <>)
		if (projectile !=null)
		{
			// Destroy the current object
			Debug.Log("The projectile has destroyed the target!");
			projectile.ResetProjectile();
			DestroyWithPuff();
			return;
		}

		// Check if the target was just hit by another target
		Target target = collision.collider.GetComponent<Target>();
		if (target != null)
		{
			// Debug.Log("Target bumped against another target. It's fine");
			return; // we stop checking
		}

		// We check if the target was collided from above just enough
		if (collision.contacts[0].normal.y < -0.5)
		{
			Debug.Log("Something that isn't a projectile or a target has destroyed the target from above!");
			DestroyWithPuff();
		}
		
		return;
		
	}

	private void MakePuffOfClouds()
    {
		// TODO: Stop particle effect after first puff - currently keeps looping
		GameObject puff = Instantiate(_cloudParticlePrefab, transform.position, Quaternion.identity);
		Destroy(puff, 3);
		return;
    }

	private void DestroyWithPuff()
	{
		// TODO: call upon being destroyed/deactivated, whichever applies.
        MakePuffOfClouds();
		// TODO: either deactivate or move target - for infinite play mode.
		Destroy(gameObject);
		_lc.DecrementTargets();
	}
}