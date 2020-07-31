using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadProjectile : MonoBehaviour
{
    [SerializeField]  private Projectile food;
    private ObjectFinder _of = new ObjectFinder();

    public void CallProjectileReload()
    {
        food.ResetProjectile();
    }
    // Start is called before the first frame update
    void Start()
    {
        if (food == null)
        {
            GameObject foodObject;
            if (_of.FindObject("Food", out foodObject))
            {
                food = foodObject.GetComponent<Projectile>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
