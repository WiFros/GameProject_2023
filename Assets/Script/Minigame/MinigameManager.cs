using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    public GameObject NpcGameObject;  // 관리할 NPC
    public NPC npc;  // 관리할 NPC
    public NPCFieldOfView npcFieldOfView;  // 관리할 NPC의 시야
    public Transform playerStartPosition;  // 플레이어 시작 위치
    public Transform npcStartPosition;  // NPC 시작 위치

    public float rotationSpeedMin = 10f;
    public float rotationSpeedMax = 30f;
    public float delayBetweenRotationsMin = 1f;
    public float delayBetweenRotationsMax = 3f;

    private bool isLookingAtPlayer = true;
    
    public void Start()
    {
        npc = NpcGameObject.GetComponent<NPC>();
        npcFieldOfView = NpcGameObject.GetComponent<NPCFieldOfView>();
    }
    public void StartMiniGame()
    {
        Player.Instance.transform.position = playerStartPosition.position;
        GameObject.Find("NPC").transform.position = npcStartPosition.position;

        npc.isInMiniGame = true;
        npc.originalFOV = 60;
        npcFieldOfView.viewRadius = 40;
        npc.miniGameFOV = 120;

        npc.transform.LookAt(Player.Instance.transform);
        StartCoroutine(LookAround());
    }
    IEnumerator LookAround()
    {
        while (npc.isInMiniGame)
        {
            float rotationSpeed = Random.Range(rotationSpeedMin, rotationSpeedMax);
            float delayBetweenRotations = Random.Range(delayBetweenRotationsMin, delayBetweenRotationsMax);

            Vector3 targetDirection = isLookingAtPlayer ? (Player.Instance.transform.position - npc.transform.position).normalized : 
                (npc.transform.position - Player.Instance.transform.position).normalized;

            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

            while (Quaternion.Angle(npc.transform.rotation, targetRotation) > 1f)
            {
                npc.transform.rotation = Quaternion.RotateTowards(npc.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                yield return null;
            }

            if (isLookingAtPlayer)
                yield return new WaitForSeconds(1f);

            yield return new WaitForSeconds(delayBetweenRotations);

            isLookingAtPlayer = !isLookingAtPlayer;
        }
    }

    public void RunningMiniGame()
    {
        if(npcFieldOfView.visibleTargets.Count > 0)
        {
            npc.isDetecting = true;
        }
        else
        {
            npc.isDetecting = false;
        }
    }

    public void EndMiniGame()
    {
        npc.isInMiniGame = false;
        StopCoroutine(LookAround());
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            UIManager.Instance.actionText.text = "Press E to start the minigame";
        }
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartMiniGame();
            }
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            UIManager.Instance.actionText.text = "";
        }
    }
}

