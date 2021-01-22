using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text scoreDisplay;
    public Text timeDisplay;
    public GameObject offPanel;
    public GameObject startButton;

    public int score = 0;
    public int timer = 60;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator RunTimer()
    {
        scoreDisplay.text = "Score:\n" + score.ToString();
        for(int i =0; i < 60; i++)
        {
            timer--;
            timeDisplay.text = "Time:\n" + timer.ToString();
            yield return new WaitForSeconds(1);
        }

        offPanel.SetActive(true);
    }

    public void StartGame()
    {
        StartCoroutine(RunTimer());
    }
}
