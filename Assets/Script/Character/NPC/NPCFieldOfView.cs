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
    public enum NPCState { Idle, Detected }
    public NPCState currentState = NPCState.Idle;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        CheckFieldOfView();
    }

    private void CheckFieldOfView()
    {
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        bool playerInSight = false; // 이 변수를 추가합니다.

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < viewAngle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleMask))
                {
                    if (target.gameObject == player)
                    {
                        playerInSight = true; // 플레이어를 감지했다면 true로 설정합니다.
                        currentState = NPCState.Detected;
                        LookAtPlayer();
                    }
                }
            }
        }

        if (!playerInSight) // 플레이어를 감지하지 못했다면
        {
            currentState = NPCState.Idle; // 상태를 Idle로 변경합니다.
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
}
