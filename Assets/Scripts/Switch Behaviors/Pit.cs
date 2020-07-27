using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Behavior for a pit object.
 * "Resets" projectiles, disables everything else.
 *  Level ends when a customer or the player hits the pit.
 */
public class Pit : MonoBehaviour
{
    public LevelController levelController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Pit collision started!");
        Debug.Log("Checking if it is a projectile");
        Projectile projectile = collision.collider.GetComponent<Projectile>();
        if (projectile != null)
        {
            projectile.ResetProjectile();
            return;
        }
        else
        {
            // 
            Player player = collision.collider.GetComponent<Player>();
            if (player != null)
            {
                levelController.SetResultsButton(isWin: false, "Player fell into pit!");
                player.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                return; // Cannot destroy/disable player, as that will eliminate the camera!
            }
            else
            {
                Target target = collision.collider.GetComponent<Target>();
                if (target != null)
                {
                    levelController.SetResultsButton(isWin: false, "Customer safety hazard!");
                }
            }
        }

        collision.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
