using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private bool platePressed = false;
    public bool PlatePressed
    {
        get { return platePressed; }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        platePressed = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        platePressed = false;
    }

}
