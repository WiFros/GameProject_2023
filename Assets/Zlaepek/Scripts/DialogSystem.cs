using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    /* UI Game Object */
    // ���
    public TextMeshProUGUI dialogText;
    // ĳ���� �̸�
    public TextMeshProUGUI characterName;

    /* ��� Class */
    [System.Serializable]
    public class Dialog
    {
        [Tooltip("ĳ���� ID")] // �ʻ�ȭ ����
        public string characterID;

        [Tooltip("��� ����")]
        public string context;

        //[Tooltip("�̺�Ʈ ��ȣ")] // ��� ����, �б��� ������ �� ��� ����
        //public string[] dialogEventID;

        //[Tooltip("�ƽ� ID")] // �ƽ� ���Կ�
        //public string[] cutSceneID;
    }

    /* ���� �о����
    Input: �����̸�.csv (string)
    Output: ��� array (Dialog[]) */
    public Dialog[] ParseDialog(string _CSVFileName)
    {
        List<Dialog> dialogList = new List<Dialog>(); // ��� ����Ʈ ����
        TextAsset csvData = Resources.Load<TextAsset>(_CSVFileName); // CSV �����͸� �ޱ� ���� �׸� 

        string[] data = csvData.text.Split(new char[] { '\n' }); //���͸� ������ �ɰ��� ����

        for (int i = 1; i < data.Length; i++) // i++�� ���� ������ �״��� ������ ���ǹ��� ���ؼ� 
        {
            string[] row = data[i].Split(new char[] { ',' }); //, ������ row �ٿ� ����

            Dialog dialog = new Dialog(); // �̹� ���� �ش��ϴ� ���
            dialog.characterID = row[0];
            dialog.context = row[1];

            dialogList.Add(dialog); // �̹� �� �߰�
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
    /* Dialog �� ���� �� ��� */
    IEnumerator StartDialogueCoroutine(Dialog[] currentEventDialog)
    {
        // �ѹ��� ��
        for (int i = 0; i < currentEventDialog.Length; i++)
        {
            // ĳ���� //
            characterName.text = currentEventDialog[i].characterID;
            // ��� //
            for (int j = 0; j < currentEventDialog[i].context.Length; j++)
            {
                dialogText.text += currentEventDialog[i].context[j]; // �ѱ��� �� ���
                yield return new WaitForSeconds(0.01f);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
