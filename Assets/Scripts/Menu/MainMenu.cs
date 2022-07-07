using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject howTo;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            howTo.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
