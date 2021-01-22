using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordsManager : MonoBehaviour
{
    public Text wordsisplay;
    private int wordsID;

    public string[] wordsLibrary;

    // Start is called before the first frame update
    void Start()
    {
        ShowRandomWords();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ShowRandomWords()
    {
        wordsID = Random.Range(0, wordsLibrary.Length);

        wordsisplay.text = wordsLibrary[wordsID];
    }

    public void CorrectAnswer()
    {
        ShowRandomWords();
    }

    public void SkipTheWords()
    {
        ShowRandomWords();
    }
}
