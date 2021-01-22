using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitController : MonoBehaviour
{
    public Sprite emptySprite;
    public string fruitName;
    public bool isPlaying = false;
    private SpriteRenderer fruits;

    public Sprite[] fruitSprite;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetFruit(string name)
    {
        fruits = gameObject.GetComponent<SpriteRenderer>();
        fruitName = name;

        if(name == "apple")
        {
            fruits.sprite = fruitSprite[1];
        }
        else if(name == "grape")
        {
            fruits.sprite = fruitSprite[0];
        }
        //gameObject.SetActive(true);
    }



    private void OnMouseDown()
    {
        if (isPlaying)
        {
            if (fruitName == "apple")
            {
                TouchGameController.instance.GetApple(gameObject);

            }

            TouchGameController.instance.RemixFruits();
            gameObject.SetActive(false);

        }

    }

   
}
