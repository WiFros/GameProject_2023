using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    // ���
    public TextMeshProUGUI dialogText;
    // ĳ���� �̸�
    public TextMeshProUGUI characterName;

    [System.Serializable]
    public class Dialog
    {
        [Tooltip("ĳ���� ID")] // �ʻ�ȭ ����
        public string characterID;

        [Tooltip("��� ����")]
        public string[] contexts;

        [Tooltip("�̺�Ʈ ��ȣ")] // ��� ����, �б��� ������ �� ��� ����
        public string[] dialogEventID;

        [Tooltip("�ƽ� ID")] // �ƽ� ���Կ�
        public string[] cutSceneID;
    }

    public Dialog[] ParseDialog(string _CSVFileName)
    {
        List<Dialog> dialogList = new List<Dialog>(); // ��� ����Ʈ ����
        TextAsset csvData = Resources.Load<TextAsset>(_CSVFileName); // CSV �����͸� �ޱ� ���� �׸� 

        string[] data = csvData.text.Split(new char[] { '\n' }); //���͸� ������ �ɰ��� ����

        for (int i = 1; i < data.Length; i++) // i++�� ���� ������ �״��� ������ ���ǹ��� ���ؼ� 
        {
            string[] row = data[i].Split(new char[] { ',' }); //, ������ row �ٿ� ����

            Dialog dialog = new Dialog(); // ��� ����Ʈ ����
            dialog.characterID = row[1];
            List<string> contextList = new List<string>(); // ��� ����Ʈ ����
            List<string> eventList = new List<string>(); // �̺�Ʈ �ѹ� ����
            List<string> cutSceneList = new List<string>(); // �ƽ� ������ ǥ���

            do
            {
                contextList.Add(row[2]);
                eventList.Add(row[3]);
                cutSceneList.Add(row[4]);

                if (++i < data.Length) // i�� �̸� ������ ���¿��� �����ش� dataLength���� �۴ٸ�
                {
                    row = data[i].Split(new char[] { ',' });
                }
                else
                {
                    break;
                }

            } while (row[0].ToString() == ""); // ���� 1ȸ ���� �� ���� �� ���� �����Ű�� ���ǹ��� ��
                                               // row 0��° �ٿ��� ID�� �� �ְ� Tostring���� �� �������� ������


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
