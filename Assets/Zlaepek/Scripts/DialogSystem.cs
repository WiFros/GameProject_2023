using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    // 대사
    public TextMeshProUGUI dialogText;
    // 캐릭터 이름
    public TextMeshProUGUI characterName;

    [System.Serializable]
    public class Dialog
    {
        [Tooltip("캐릭터 ID")] // 초상화 변경
        public string characterID;

        [Tooltip("대사 내용")]
        public string[] contexts;

        [Tooltip("이벤트 번호")] // 대사 순서, 분기점 선택지 시 출력 관리
        public string[] dialogEventID;

        [Tooltip("컷신 ID")] // 컷신 삽입용
        public string[] cutSceneID;
    }

    public Dialog[] ParseDialog(string _CSVFileName)
    {
        List<Dialog> dialogList = new List<Dialog>(); // 대사 리스트 생성
        TextAsset csvData = Resources.Load<TextAsset>(_CSVFileName); // CSV 데이터를 받기 위한 그릇 

        string[] data = csvData.text.Split(new char[] { '\n' }); //엔터를 만나면 쪼개어 넣음

        for (int i = 1; i < data.Length; i++) // i++는 대한 내용은 그다음 내용은 조건문을 통해서 
        {
            string[] row = data[i].Split(new char[] { ',' }); //, 단위로 row 줄에 저장

            Dialog dialog = new Dialog(); // 대사 리스트 생성
            dialog.characterID = row[1];
            List<string> contextList = new List<string>(); // 대사 리스트 생성
            List<string> eventList = new List<string>(); // 이벤트 넘버 생성
            List<string> cutSceneList = new List<string>(); // 컷신 있을때 표기용

            do
            {
                contextList.Add(row[2]);
                eventList.Add(row[3]);
                cutSceneList.Add(row[4]);

                if (++i < data.Length) // i가 미리 증가한 상태에서 비교해준다 dataLength보다 작다면
                {
                    row = data[i].Split(new char[] { ',' });
                }
                else
                {
                    break;
                }

            } while (row[0].ToString() == ""); // 최초 1회 조건 비교 없이 한 차례 실행시키고 조건문을 비교
                                               // row 0번째 줄에는 ID가 들어가 있고 Tostring으로 빈 공간인지 비교해줌


            dialog.contexts = contextList.ToArray();
            dialog.dialogEventID = eventList.ToArray();
            dialog.cutSceneID = cutSceneList.ToArray();

            dialogList.Add(dialog);

            GameObject obj = GameObject.Find("DialogManager");
            // obj.GetComponent<interactionEvent>().lineY = dialgoueList.Count;

        }

        return dialogList.ToArray();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PushText();
        }
    }

    private void PushText()
    {
        Dialog[] currentEventDialog = ParseDialog("Sample.csv");

        for (int i = 0; i < currentEventDialog.Length; i++)
        {
            dialogText.text = currentEventDialog[i].contexts[0];
        }
        /*
        currentTextLine += 1;
        if (currentTextLine >= currentDialogue.line.Count)
        {
            Conclude();
        }
        else
        {
            targetText.text = currentDialogue.line[currentTextLine];
        }
        */
    }
}
