using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchGameController : MonoBehaviour
{
    public GameObject fruitPrefabs;
    public Text appleTakenDisplay;
    public Text timerDisplay;
    private int appleTaken;
    private int timer = 60;
    private int preClicks = 2;

    public GameObject panelGameOver;
    
    public Text GameOverText;


    public List<FruitController> fruits;
    public List<GameObject> apple;
    public List<int> scoresData;

    public static TouchGameController instance;

    private void Awake()
    {
        instance = this;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        scoresData = SaveLoadData.instance.savedData.touchGameScore;
        InitFruits();
        appleTakenDisplay.text = "Apple:\n" + appleTaken;
        timerDisplay.text = "Timer:\n" + timer;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitFruits()
    {
        float xx = -3.9f;
        float yy = -3f;
        

        for(int i = 0; i < 4; i++)
        {
            xx = -3.7f;
            yy += 1.5f;
            for (int j = 0; j < 4; j++)
            {
                GameObject go = Instantiate(fruitPrefabs, new Vector3((xx+=1.5f), yy, 10), Quaternion.identity);
                go.GetComponent<FruitController>().GetFruit("grape");
                fruits.Add(go.GetComponent<FruitController>());
            }
        }

        RunPreClicks();
    }

    IEnumerator RunTimer()
    {
        StartTapping();

        for (int i = 0; i < 60; i++)
        {
            timer--;
            timerDisplay.text = "Time:\n" + timer.ToString();
            yield return new WaitForSeconds(1);
        }

        GameOver();
    }

    public void StartGame()
    {
        StartCoroutine(RunTimer());
       
    }

    void GameOver()
    {
        StopTapping();
        //GameOverText.text = "Game Over" + "\n\n" + "Your score is: " + appleTaken;
        
        scoresData.Add(appleTaken);
        SaveLoadData.instance.SavingData(scoresData);
        SaveLoadData.instance.OpenData();
        panelGameOver.SetActive(true);
    }


    public void GetApple(GameObject apples)
    {
        appleTaken++;
        appleTakenDisplay.text = "Apple:\n" + appleTaken;
        apple.Remove(apples);
        //RemixFruits();
    }

    void RunPreClicks()
    {
        int a = 0;
        int prev = 0;
        for(int i = 0; i < preClicks; i++)
        {
            prev = a;
            
            a = Random.Range(0, fruits.Count);
            if(a == prev)
            {
                for (int j = 0; j < 3; j++)
                {
                    a = Random.Range(0, fruits.Count);
                }
                    
            }


            bool sameObject = false;
            
            if(apple.Count > 1)
            {
                for (int b = 0; b < apple.Count; b++)
                {
                    if (fruits[a].gameObject == apple[b])
                    {
                        sameObject = true;
                        Debug.Log(" sama");
                    }
                }

                if (!sameObject)
                {
                    fruits[a].GetComponent<FruitController>().GetFruit("apple");
                    apple.Add(fruits[a].gameObject);
                }
               
            }
            else
            {
                fruits[a].GetComponent<FruitController>().GetFruit("apple");
                apple.Add(fruits[a].gameObject);
            }
            
            //fruits[a].GetComponent<FruitController>().GetFruit("apple");
            //apple.Add(fruits[a].gameObject);


            //Debug.Log("prev id: " + prev + " a value: " + a);
        }
        if(preClicks < 8)
        {
            preClicks++;
        }
        
        for (int i = 0; i < fruits.Count; i++)
        {

            //fruits[i].gameObject.SetActive(false);
            fruits[i].gameObject.SetActive(true);
        }
    }

    public void RemixFruits()
    {

        StartCoroutine(StartFruit());
    }

    IEnumerator StartFruit()
    {
        yield return new WaitForEndOfFrame();
        if (apple.Count < 1)
        {
            for (int i = 0; i < fruits.Count; i++)
            {

                fruits[i].GetComponent<FruitController>().GetFruit("grape");
                //fruits[i].gameObject.SetActive(true);
            }

            RunPreClicks();
        }

    }

    void StartTapping()
    {
        for(int i =0; i < fruits.Count; i++)
        {
            fruits[i].GetComponent<FruitController>().isPlaying = true;
        }
    }

    void StopTapping()
    {
        for (int i = 0; i < fruits.Count; i++)
        {
            fruits[i].GetComponent<FruitController>().isPlaying = false;
        }
    }

    public void ReplayGame()
    {
        timer = 60;
        timerDisplay.text = "Time:\n" + timer.ToString();

        preClicks = 2;

        appleTaken = 0;
        appleTakenDisplay.text = "Apple:\n" + appleTaken;

        apple.Clear();
        RemixFruits();
        StartGame();
        panelGameOver.SetActive(false);
    }
}
