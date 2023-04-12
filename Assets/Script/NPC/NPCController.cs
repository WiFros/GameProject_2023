using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    public Transform target; // 플레이어의 위치를 추적하는 변수
    public float interactionDistance = 3f; // 상호작용을 시작할 거리
    public KeyCode interactionKey = KeyCode.E; // 상호작용을 시작할 키

    private NavMeshAgent agent; // NavMeshAgent 컴포넌트를 참조하는 변수
    private Animator animator;
    public enum State
    {
        Idle,
        Patrol,
        Chase,
        Interact
    }

    private State currentState = State.Idle;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>(); // NPC에 있는 NavMeshAgent 컴포넌트를 가져옵니다.
    }

    private void Update()
    {
        switch (currentState)
        {
            case State.Idle:
                // Idle 상태에서는 아무것도 하지 않습니다.
                break;
            case State.Patrol:
                // Patrol 상태에서는 일정한 경로를 따라 이동합니다.
                Patrol();
                break;
            case State.Chase:
                // Chase 상태에서는 플레이어를 추적합니다.
                ChasePlayer();
                break;
            case State.Interact:
                // Interact 상태에서는 플레이어와 상호작용을 시작합니다.
                Interact();
                break;
        }
    }

    private void Patrol()
    {
        // 경로를 따라 이동하는 로직을 구현합니다.
    }

    private void ChasePlayer()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance < interactionDistance)
        {
            // 플레이어가 상호작용 거리에 있을 때 키를 누르면 대화를 시작합니다.
            if (Input.GetKeyDown(interactionKey))
            {
                SetState(State.Interact);
            }
            else
            {
                agent.SetDestination(target.position);
            }
        }
        else
        {
            agent.SetDestination(target.position);
        }
    }

    private void Interact()
    {
        // 플레이어와의 상호작용 로직을 구현합니다.
        // 예를 들어, 대화 시스템을 호출하거나, 퀘스트를 제공할 수 있습니다.
    }

    // 상태를 변경하는 메서드입니다.
    public void SetState(State newState)
    {
        currentState = newState;
    }
}
