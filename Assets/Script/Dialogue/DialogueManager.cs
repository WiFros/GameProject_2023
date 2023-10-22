using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    private Queue<string> sentences;
    private SampleCameraManager scm;
    public GameObject dialogueBox;
    public Animator animator;
    void Start()
    {
        sentences = new Queue<string>();
        scm = GameObject.Find("TestingCameraManager").GetComponent<SampleCameraManager>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        dialogueBox.SetActive(true);
        animator.SetBool("IsOpen", true);
        //Debug.Log("Starting conversation with " + dialogue.name);
        
        nameText.text = dialogue.name;
        sentences.Clear();
        
        //���ϴ� ĳ������ �̸��� ������ ī�޶� ����
        switch (nameText.text)
        {
            case "":
                scm.CameraChaingeToCharacter(0);
                break;
            case " ":
                scm.CameraChaingeToCharacter(1);
                break;
        }

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));

        //dialogueText.text = sentence;
        //Debug.Log(sentence);
    }
    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }
    public void EndDialogue()
    {
        scm.CameraChaingeToMain();
        animator.SetBool("IsOpen", false);
        dialogueBox.SetActive(false);
        //Debug.Log("End of converstion");
    }
}
