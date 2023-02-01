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
    // ���� ���� ���
    string path;

    void Start()
    {
        path = Path.Combine(Application.dataPath, "gamesave.json");
        JsonSave();
        JsonLoad();
    }

    // �ҷ�����
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

    // ����
    public void JsonSave()
    {
        SaveData saveData = new SaveData();

        for (int i = 0; i < 10; i++)
        {
            saveData.stage1.Add("�׽�Ʈ ������ no " + i);
        }

        for (int i = 0; i < 10; i++)
        {
            saveData.stage2.Add("�׽�Ʈ ������ no " + i);
        }

        // json ���·� ��ȯ
        string json = JsonUtility.ToJson(saveData, true);

        // ���Ͽ� ����
        File.WriteAllText(path, json);
    }
}
