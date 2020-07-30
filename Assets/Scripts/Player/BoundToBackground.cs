using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code taken from:
// https://pressstart.vip/tutorials/2018/06/28/41/keep-object-in-bounds.html

public class BoundToBackground : MonoBehaviour
{
    //public Camera MainCamera; //be sure to assign this in the inspector to your main camera
    public SpriteRenderer backgroundSprite;

    private ObjectFinder _oc = new ObjectFinder();

    private Camera MainCamera;

    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;

    // Use this for initialization
    void Start()
    {
        GameObject cameraObject;
        if (_oc.FindMainCamera(out cameraObject))
            MainCamera = cameraObject.GetComponent<Camera>();
        /*
        screenBounds = MainCamera.ScreenToWorldPoint(new Vector3(backgroundSprite., backgroundSprite.height, MainCamera.transform.position.z));
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x; //extents = size of width / 2
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y; //extents = size of height / 2
        */

        screenBounds = MainCamera.ScreenToWorldPoint(new Vector3(backgroundSprite.bounds.size.x, backgroundSprite.bounds.size.y, MainCamera.transform.position.z));
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x; //extents = size of width / 2
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y; //extents = size of height / 2
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 viewPos = transform.position;
        // for Perspective camera
        /*
        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x + objectWidth, screenBounds.x * -1 - objectWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y + objectHeight, screenBounds.y * -1 - objectHeight);
        */
        // For Orthographic camera
        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1 + objectWidth, screenBounds.x - objectWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y * -1 + objectHeight, screenBounds.y - objectHeight);

        // transform.position = viewPos;
    }
}

