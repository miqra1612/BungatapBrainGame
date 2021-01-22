using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoManager : MonoBehaviour
{
    static TestHelloUnityVideo app = null;
   
    public TestHome gameController;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<TestHome>();
        //app = gameController.GetComponent<TestHome>().bb;
        //StartCoroutine(initaja());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator initaja()
    {
        yield return new WaitForSeconds(1);
        //gameController.ForceShowVideo(false);
        yield return new WaitForSeconds(1);
       
    }

    public void ForceConncet()
    {
        //gameController.ForceShowVideo(true);
        //waitToConnect.SetActive(false);
    }

    public void ForceDisconnect()
    {
        //gameController.ForceShowVideo(false);
       // waitToConnect.SetActive(true);
    }
}
