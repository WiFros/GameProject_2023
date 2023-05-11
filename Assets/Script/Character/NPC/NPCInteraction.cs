using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPCInteraction : MonoBehaviour
{
    public GameObject player;
    public float interactionDistance = 3.0f;
    public TextMeshProUGUI interactionTextUI;
    public NPC npc;
    private DialogueManager dialogueManager;
    private bool isInRange = false;

    private void Start()
    {
        interactionTextUI.gameObject.SetActive(false);
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    private void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < interactionDistance)
        {
            if (!isInRange)
            {
                isInRange = true;
                interactionTextUI.gameObject.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.F) && !dialogueManager.dialogueBox.activeInHierarchy)
            {
                npc.InteractWithPlayer();
            }
        }
        else
        {
            if (isInRange)
            {
                isInRange = false;
                interactionTextUI.gameObject.SetActive(false);
            }
        }
    }
}