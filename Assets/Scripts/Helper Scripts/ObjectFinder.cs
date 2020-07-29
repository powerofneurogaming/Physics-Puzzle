﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
            Debug.LogWarning($"Warning! Didn't find [{objectName}] in the hierarcy!");
            return false;
        }

        Debug.Log($"Found [{objectName}]!");
        return true;
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

    public bool FindCanvasObject(string buttonName, out GameObject foundButton)
    {
        return FindParentsChild("Canvas", buttonName, out foundButton);
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
}