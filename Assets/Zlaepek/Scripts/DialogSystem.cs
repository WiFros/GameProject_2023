using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    /* UI Game Object */
    // 대사
    public TextMeshProUGUI dialogText;
    // 캐릭터 이름
    public TextMeshProUGUI characterName;

    /* 대사 Class */
    [System.Serializable]
    public class Dialog
    {
        [Tooltip("캐릭터 ID")] // 초상화 변경
        public string characterID;

        [Tooltip("대사 내용")]
        public string context;

        //[Tooltip("이벤트 번호")] // 대사 순서, 분기점 선택지 시 출력 관리
        //public string[] dialogEventID;

        //[Tooltip("컷신 ID")] // 컷신 삽입용
        //public string[] cutSceneID;
    }

    /* 파일 읽어오기
    Input: 파일이름.csv (string)
    Output: 대사 array (Dialog[]) */
    public Dialog[] ParseDialog(string _CSVFileName)
    {
        List<Dialog> dialogList = new List<Dialog>(); // 대사 리스트 생성
        TextAsset csvData = Resources.Load<TextAsset>(_CSVFileName); // CSV 데이터를 받기 위한 그릇 

        string[] data = csvData.text.Split(new char[] { '\n' }); //엔터를 만나면 쪼개어 넣음

        for (int i = 1; i < data.Length; i++) // i++는 대한 내용은 그다음 내용은 조건문을 통해서 
        {
            string[] row = data[i].Split(new char[] { ',' }); //, 단위로 row 줄에 저장

            Dialog dialog = new Dialog(); // 이번 열에 해당하는 대사
            dialog.characterID = row[0];
            dialog.context = row[1];

            dialogList.Add(dialog); // 이번 열 추가
        }

        return dialogList.ToArray();
    }

    // Start is called before the first frame update
    void Start()
    {
        Dialog[] currentEventDialog = ParseDialog("Sample.csv");
        StartCoroutine(StartDialogueCoroutine(currentEventDialog));
    }

    // ref) https://onecoke.tistory.com/entry/%EC%9C%A0%EB%8B%88%ED%8B%B02D-%EB%8C%80%ED%99%94-%EC%9D%B4%EB%B2%A4%ED%8A%B8-%EA%B5%AC%ED%98%84-%EC%97%B0%EC%8A%B5
    /* Dialog 한 문장 씩 출력 */
    IEnumerator StartDialogueCoroutine(Dialog[] currentEventDialog)
    {
        // 한문장 당
        for (int i = 0; i < currentEventDialog.Length; i++)
        {
            // 캐릭터 //
            characterName.text = currentEventDialog[i].characterID;
            // 대사 //
            for (int j = 0; j < currentEventDialog[i].context.Length; j++)
            {
                dialogText.text += currentEventDialog[i].context[j]; // 한글자 씩 출력
                yield return new WaitForSeconds(0.01f);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
