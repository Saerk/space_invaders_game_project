using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinControllerScript : MonoBehaviour
{
    public Text winText;
    public Text restartText;
    public Text exitText;

    void Start()
    {
        winText.text = "Congratulations!";
        restartText.text = "Press R to restart";
        exitText.text = "Press Q to quit";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Main Scene",LoadSceneMode.Single);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit();
        }
    }

}
