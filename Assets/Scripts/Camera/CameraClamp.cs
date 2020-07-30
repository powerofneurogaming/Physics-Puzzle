using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attatck to camera.
public class CameraClamp : MonoBehaviour
{
    [SerializeField] private Transform targetToFollow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(
            Mathf.Clamp(targetToFollow.position.x, -5.5f, 5.5f),
            Mathf.Clamp(targetToFollow.position.y, -5f, 5f),
            transform.position.z);
    }
}
