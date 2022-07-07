using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{

    [SerializeField]
    private Text senText;

    private bool isLoading = false;

    public static float Sensitivity = 1;

    public void SetSensitivity(float sen)
    {
        senText.text = string.Format("{0:0.00}", sen);
        Sensitivity = sen;
    }

    public void StartGame()
    {
        if (!isLoading)
        {
            isLoading = true;
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }
    }
}
