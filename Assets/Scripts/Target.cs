using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.Physics2DModule;

public class Target : MonoBehaviour
{
	//Variables
	// Public
	//Private
	[SerializeField] private GameObject _cloudParticlePrefab = null;

	private void OnCollisionEnter2D(Collision2D collision)
	{
		Debug.Log("Collision started!");
		// TODO: Change the name of the object, and extend if portion to outside
		FlappyFatBirdBehavior2 projectile = collision.collider.GetComponent<FlappyFatBirdBehavior2>();
		// Checks if the object is of defined type (in sharp brackets <>)
		if (projectile !=null)
		{
			// Destroy the current object
			Debug.Log("The projectile has destroyed the target!");
            MakePuffOfClouds();
			Destroy(gameObject);
			return;
		}

		// Check if the target was just hit by another target
		Target target = collision.collider.GetComponent<Target>();
		if(target != null)
			return; // we stop checking
		
		// We check if the target was collided from above just enough
		if( collision.contacts[0].normal.y < -0.5 )
		{
			Debug.Log("Something that isn't a projectile or a target has destroyed the target from above!");
			MakePuffOfClouds();
			Destroy(gameObject);
		}
		
		return;
		
	}

	private void MakePuffOfClouds()
    {
		Instantiate(_cloudParticlePrefab, transform.position, Quaternion.identity);
		return;
    }

	private void DestroyWithPuff()
	{
        MakePuffOfClouds();
		Destroy(gameObject);
	}
}