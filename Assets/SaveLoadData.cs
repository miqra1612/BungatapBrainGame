using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveLoadData : MonoBehaviour
{
    public SavedData savedData;
    private string SavePlace;

    public static SaveLoadData instance;

    private void Awake()
    {
        instance = this;
        if (Application.platform == RuntimePlatform.Android)
        {
            SavePlace = "//BungatapData.JSON";
        }
        else if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            SavePlace = "/BungatapData.JSON";
        }

        if (File.Exists(Application.persistentDataPath + SavePlace))
        {
            SaveLoadData.instance.OpenData();
        }
    }


    // Use this for initialization
    public void SavingData(List<int> scoreDatas)
    {
        savedData.touchGameScore = scoreDatas;

        SavedData gd = new SavedData();
        
        gd = savedData;

        string saveTheData = JsonUtility.ToJson(gd, true);

        File.WriteAllText(Application.persistentDataPath + SavePlace, saveTheData);

    }

    // Update is called once per frame
    public void OpenData()
    {

        if (File.Exists(Application.persistentDataPath + SavePlace))
        {
            string jd = File.ReadAllText(Application.persistentDataPath + SavePlace);
            savedData = JsonUtility.FromJson<SavedData>(jd);
        }

    }
}
