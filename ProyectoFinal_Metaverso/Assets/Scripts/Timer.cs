using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class Timer : MonoBehaviour
{

    public TextMeshProUGUI timerText;
    public string nombre;
    static float timer;

    public TextMeshProUGUI lapText;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void RestartGame()
    {
        SceneManager.LoadScene("DemoScene");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer - minutes * 60);

        string time = string.Format("{0:0}:{1:00}", minutes, seconds);

        timerText.text = time;

        if (Input.GetKeyDown("9"))
        {
            RestartGame();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (nombre == "Inicio")
        {
            timer = 0.0f;
        }
        else
        {
            lapText.text = "Lap: " + timerText.text;
            timer = 0.0f;
        }
    }

}