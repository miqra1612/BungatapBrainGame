using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowGraph : MonoBehaviour
{
    public Sprite graphDot;

    private RectTransform graphContainer;
    public RectTransform graphLabelX;
    public RectTransform graphLabelY;
    public RectTransform graphYline;
    public RectTransform graphLabeValue;

    public List<int> scoreData;
    public List<GameObject> graphObject;

    private void Awake()
    {
       

    }

    
    // Start is called before the first frame update
    void Start()
    {
       

    }

    private void OnEnable()
    {
        if(graphContainer == null)
        {
            graphContainer = gameObject.GetComponent<RectTransform>();
        }
        
        scoreData = TouchGameController.instance.scoresData;
        ShowGraph(scoreData);
    }

    private void OnDisable()
    {
        DestroyGraph();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    GameObject CreateCircle(Vector2 anchoredPosition)
    {
        GameObject go = new GameObject("dots", typeof (Image));
        go.transform.SetParent(graphContainer, false);

        RectTransform rct = go.GetComponent<RectTransform>();
        rct.anchoredPosition = anchoredPosition;
        rct.sizeDelta = new Vector2(15, 15);
        rct.anchorMin = new Vector2(0, 0);
        rct.anchorMax = new Vector2(0, 0);

        Image imgo = go.GetComponent<Image>();
        imgo.sprite = graphDot;
        imgo.color = Color.blue;
        graphObject.Add(go);
        
        return go;
    }

    void ShowGraph(List<int> scoresList)
    {
        float graphHeigh = 200;////graphContainer.sizeDelta.y;
        float yMax = 100f;
        float xMax = 50f;

        GameObject lastDot = null;
        int showedData = 0;

        if(scoreData.Count > 10)
        {
            showedData = scoreData.Count - 10;
        }

        for(int i = 0; i< scoresList.Count; i++)
        {
            float xPos = 0;
            float yPos = 0;

            if (scoresList.Count <= 10)
            {
                xPos = xMax + i * xMax;
                yPos = (scoresList[i] / yMax) * graphHeigh;
            }
            else
            {
                
                xPos = xMax + i * xMax;
                yPos = (scoresList[showedData] / yMax) * graphHeigh;
            }

            GameObject theDot = CreateCircle(new Vector2(xPos, yPos));
            if(lastDot != null)
            {
                GraphLine(lastDot.GetComponent<RectTransform>().anchoredPosition, theDot.GetComponent<RectTransform>().anchoredPosition);
                //Debug.Log(lastDot.GetComponent<RectTransform>().anchoredPosition);
            }

            lastDot = theDot;

            RectTransform rgl = Instantiate(graphLabelX);
            rgl.SetParent(graphContainer);
            rgl.anchoredPosition = new Vector2(xPos - 10, -40);
            rgl.anchorMin = new Vector2(0, 0);
            rgl.anchorMax = new Vector2(0, 0);
            rgl.localScale = Vector3.one;
            if (scoresList.Count <= 10)
            {
                rgl.GetComponent<Text>().text = (i + 1).ToString();
            }
            else
            {
                rgl.GetComponent<Text>().text = (showedData + 1).ToString();
            }
            

            graphObject.Add(rgl.gameObject);

            RectTransform val = Instantiate(graphLabeValue);
            val.SetParent(theDot.transform);
            val.anchoredPosition = new Vector2(0, 25);
            val.localScale = Vector3.one;
            Text dis = val.GetComponent<Text>();
            if (scoresList.Count <= 10)
            {
                dis.text = scoresList[i].ToString();
            }
            else
            {
                dis.text = scoresList[showedData].ToString();
            }
            

            graphObject.Add(val.gameObject);
            if(showedData >= scoresList.Count - 1)
            {
                break;
            }
            showedData++;
        }


        int separator = 4;

        for (int i = 0; i <= separator; i++)
        {
            RectTransform rgl = Instantiate(graphLabelY);
            rgl.SetParent(graphContainer);
            float normalizer = i * 1.9f / separator;
            float normalVal = i * 1f / separator;
            rgl.anchoredPosition = new Vector2(-55, normalizer * graphHeigh);
            rgl.anchorMin = new Vector2(0, 0);
            rgl.anchorMax = new Vector2(0, 0);
            rgl.localScale = Vector3.one;
            rgl.GetComponent<Text>().text = Mathf.Round(normalVal * graphHeigh).ToString();

            RectTransform yLine = Instantiate(graphYline);
            yLine.SetParent(graphContainer);
            float lineNormalizer = i * 1.9f / separator;
            yLine.anchoredPosition = new Vector2(graphContainer.localScale.x/2, (normalizer * graphHeigh)+15);
            yLine.anchorMin = new Vector2(0, 0);
            yLine.anchorMax = new Vector2(0, 0);
            yLine.localScale = Vector3.one;


        }

    }

    void GraphLine( Vector2 posA, Vector2 posB)
    {
        GameObject go = new GameObject("lines", typeof(Image));
        go.transform.SetParent(graphContainer, false);

        RectTransform rct = go.GetComponent<RectTransform>();
        Vector2 dot = (posB - posA).normalized;
        float dis = Vector2.Distance(posA, posB);

        float angleInRad = Mathf.Atan2(posB.y - posA.y, posB.x - posA.x);
        float angleInDegree = (angleInRad * 180) / Mathf.PI;

        rct.anchorMin = new Vector2(0, 0);
        rct.anchorMax = new Vector2(0, 0);
        rct.sizeDelta = new Vector2(60,3f);
        rct.anchoredPosition = posA + dot * dis * 0.5f;
        rct.localEulerAngles = new Vector3(0, 0, angleInDegree);
        //Debug.Log(rct.localEulerAngles);

        Image imgo = go.GetComponent<Image>();

        imgo.color = Color.blue;
        graphObject.Add(go.gameObject);
    }

    public void DestroyGraph()
    {
        for(int i = 0; i< graphObject.Count; i++)
        {
            Destroy(graphObject[i]);
        }

        graphObject.Clear();
    }
}
