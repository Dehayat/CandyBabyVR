using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddLights : MonoBehaviour
{
    public GameObject[] lights;
    public GameObject pointLight;
    public Transform parent;

    public bool doThing = false;

    private void OnValidate()
    {
        if (lights == null || pointLight == null || parent == null) return;
        if (doThing)
        {
            doThing = false;
        }
        else
        {
            return;
        }
        foreach (var item in lights)
        {
            Instantiate(pointLight, item.transform.position,Quaternion.identity, parent);
        }
    }
}
