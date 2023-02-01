using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ref: https://lefthanddeveloper.tistory.com/15
// ref: https://geojun.tistory.com/65
[System.Serializable]
public class SaveData
{
    public string current_savePoint = "0";
    public List<string> tutorial = new List<string>();
    public List<string> stage1 = new List<string>();
    public List<string> stage2 = new List<string>();
}

public class SavingSystem : MonoBehaviour
{
    // 파일 저장 경로
    string path;

    void Start()
    {
        path = Path.Combine(Application.dataPath, "gamesave.json");
        JsonSave();
        JsonLoad();
    }

    // 불러오기
    public void JsonLoad()
    {
        SaveData saveData = new SaveData();

        if (!File.Exists(path))
        {
            //GameManager.instance.playerGold = 100;
            //GameManager.instance.playerPower = 4;
            //SaveData();
        }
        else
        {
            string loadJson = File.ReadAllText(path);
            saveData = JsonUtility.FromJson<SaveData>(loadJson);

            if (saveData != null)
            {
                for (int i = 0; i < saveData.stage1.Count; i++)
                {
                    
                }
                for (int i = 0; i < saveData.stage2.Count; i++)
                {
                    
                }
            }
        }
    }

    // 저장
    public void JsonSave()
    {
        SaveData saveData = new SaveData();

        for (int i = 0; i < 10; i++)
        {
            saveData.stage1.Add("테스트 데이터 no " + i);
        }

        for (int i = 0; i < 10; i++)
        {
            saveData.stage2.Add("테스트 데이터 no " + i);
        }

        // json 형태로 변환
        string json = JsonUtility.ToJson(saveData, true);

        // 파일에 저장
        File.WriteAllText(path, json);
    }
}
