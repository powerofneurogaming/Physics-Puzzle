using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* Possible mprovements:
 * 
*/
/*
 * Class used to find the GameObjects in the scene hierarchy
 */
public class ObjectFinder // : MonoBehaviour
{

    public bool FindObject(string objectName, out GameObject foundObject)
    {
        foundObject = GameObject.Find(objectName);
        if (foundObject == null)
        {
            Debug.LogWarning($"Warning! Didn't find [{objectName}] in the hierarchy!");
            return false;
        }

        Debug.Log($"Found [{objectName}]!");
        return true;
    }

    public GameObject FindObject(string objectName)
    {
        GameObject foundObject;
        FindObject(objectName, out foundObject);
        return foundObject;
    }

    public bool FindFromParentObject(GameObject parentObject, string child, out GameObject childObject)
    {
        childObject = parentObject.transform.Find(child).gameObject;
        if(childObject==null)
        {
            Debug.LogWarning($"Warning! Couldn't find the child [{child}] of GameObject parent [{parentObject.name}]!");
            return false;
        }

        Debug.Log($"Found the child [{child}] of GameObject parent [{parentObject.name}]!");
        return true;
    }

    public GameObject FindFromParentObject(GameObject parentObject, string child)
    {
        GameObject childObject;
        FindFromParentObject(parentObject, child, out childObject);
        return childObject;
    }

    public bool FindParentsChild(string parent, string child, out GameObject childObject)
    {
        GameObject parentObject;
        childObject = null;

        if (FindObject(parent, out parentObject))
        {
            Debug.Log($"Found the parent object, {parent}!");
            if (FindFromParentObject(parentObject, child, out childObject))
            {
                Debug.Log($"Found the child [{child}]!");
                return true;
            }
        }
        else
            Debug.LogWarning($"Couldn't find the parent [{parent}[!");
        Debug.LogWarning($"Child [{child}] not found!");

        return false;
    }

    public GameObject FindParentsChild(string parent, string child)
    {
        GameObject childObject;
        FindParentsChild(parent, child, out childObject);
        return childObject;
    }

    public bool FindGrandChildObject(string parent, string child, string grandChild, out GameObject grandChildObject)
    {
        grandChildObject = null;
        GameObject childObject;
        if (FindParentsChild(parent, child, out childObject))
        {
            return FindFromParentObject(childObject, grandChild, out grandChildObject);
        }
        return false;
    }

    public GameObject FindGrandChildObject(string parent, string child, string grandChild)
    {
        GameObject grandChildObject;
        FindGrandChildObject(parent,  child, grandChild, out grandChildObject);
        return grandChildObject;
    }

    public bool FindMainCamera(out GameObject cameraObject)
    {
        if (!FindObject("Main Camera", out cameraObject))
            return FindParentsChild("Player (stand-in)", "Main Camera", out cameraObject);
        return true;
    }

    public GameObject FindMainCamera()
    {
        GameObject cameraObject;
        FindMainCamera(out cameraObject);
        return cameraObject;
    }

    public bool FindCanvas(out GameObject canvasObject)
    {
        if( !FindObject("Canvas", out canvasObject))
        {
            GameObject cameraObject;
            if(FindMainCamera(out cameraObject))
                return FindFromParentObject(cameraObject, "Canvas", out canvasObject);
            return false;
        }

        return true;
    }

    public bool FindCanvasObject(string buttonName, out GameObject foundButton)
    {
        if (!FindParentsChild("Canvas", buttonName, out foundButton))
            return FindGrandChildObject("Main Camera", "Canvas", buttonName, out foundButton);
        return true;

    }

    public bool FindButtonChild(string buttonName, string childName, out GameObject childObject)
    {
        childObject = null;
        GameObject buttonObject;

        if(FindCanvasObject(buttonName, out buttonObject))
        {
            if (FindFromParentObject(buttonObject, childName, out childObject))
                return true;
        }
        return false;
    }

    // public bool FindObjectComponent(function Func)
}
