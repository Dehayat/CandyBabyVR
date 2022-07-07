using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public void Rotate(float rotation, Vector3 centerPosition)
    {
        transform.RotateAround(centerPosition, transform.up, rotation);
    }
}
