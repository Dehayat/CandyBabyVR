using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowTo : MonoBehaviour
{
    [SerializeField]
    private GameObject Settings;

    int frameCount = 5;

    void Update()
    {
        if (frameCount > 0)
        {
            frameCount--;
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            Settings.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
