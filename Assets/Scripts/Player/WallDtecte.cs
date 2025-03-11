using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDtecte : MonoBehaviour
{
    public bool isTouchingWall;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Flatform"))
        {
            isTouchingWall = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Flatform"))
        {
            isTouchingWall= false;
        }
    }
}
