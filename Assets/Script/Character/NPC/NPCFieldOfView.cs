using System.Collections.Generic;
using UnityEngine;

public class NPCFieldOfView : MonoBehaviour
{
    [Header("Field of View")]
    public float viewRadius;
    public float viewAngle;
    public LayerMask targetMask;
    public LayerMask obstacleMask;
    public bool visualizeFieldOfView;
    private GameObject player;

    private Vector3 initialPlayerPosition;
    private bool isGameActive = true; 
    // visibleTargets 리스트 추가
    public List<Transform> visibleTargets = new List<Transform>();

    public enum NPCState { Idle, Detected }
    public NPCState currentState = NPCState.Idle;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        initialPlayerPosition = player.transform.position;
    }

    private void Update()
    {
        CheckFieldOfView();
        
        
    }
    private void LoseGame()
    {
        Debug.Log("Game Lost!");
        isGameActive = false;
        player.transform.position = initialPlayerPosition; // 플레이어를 시작 위치로 재설정
    }

    private void EndMiniGame()
    {
        Debug.Log("MiniGame Ended!");
        isGameActive = false;
        // 추가적인 게임 종료 로직
    }
    private void CheckPlayerMovement()
    {
        if (Input.anyKey) // 어떤 키라도 누르면
        {
            LoseGame();
        }
    }
    private void CheckFieldOfView()
    {
        visibleTargets.Clear(); // 매 Update()에서 리스트를 클리어
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        bool playerInSight = false;

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < viewAngle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleMask))
                {
                    visibleTargets.Add(target); // 시야 내의 대상을 리스트에 추가

                    if (target.gameObject == player)
                    {
                        playerInSight = true;
                        currentState = NPCState.Detected;
                        LookAtPlayer();
                    }
                }
            }
        }

        if (!playerInSight)
        {
            currentState = NPCState.Idle;
        }
        
    }

    private void LookAtPlayer()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnDrawGizmos()
    {
        if (visualizeFieldOfView)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, viewRadius);
            Vector3 viewAngleA = DirectionFromAngle(-viewAngle / 2, false);
            Vector3 viewAngleB = DirectionFromAngle(viewAngle / 2, false);
            Gizmos.DrawLine(transform.position, transform.position + viewAngleA * viewRadius);
            Gizmos.DrawLine(transform.position, transform.position + viewAngleB * viewRadius);
        }
    }

    public Vector3 DirectionFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    // 새로운 판단 로직 메소드
    public bool IsTargetVisible(Transform target)
    {
        return visibleTargets.Contains(target);
    }

    public bool IsPlayerInSight()
    {
        return IsTargetVisible(player.transform);
    }
}
