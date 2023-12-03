using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // 캐릭터 이동 속도
    public float jumpSpeed = 1f; // The speed of the jump
    public float smoothTime = 0.1f; // 회전 시 부드러운 이동을 위한 시간
    public bool isGrounded = false; // 땅에 닿아있는지 여부
    public Transform mainCamera; // 메인 카메라 Transform

    private Rigidbody rb; // Rigidbody 컴포넌트
    private Animator animator; // Animator 컴포넌트
    private bool isJumping = false;
    private Vector3 jumpDirection;
    
    public Transform groundCheck; // 땅 체크를 위한 Transform
    public float groundCheckRadius = 0.5f; // 땅 체크 반경
    public LayerMask groundLayer; // 지면 LayerMask
    
    private bool isHoldingObject = false;
    private InteractiveObject heldObject;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Rigidbody 컴포넌트 가져오기
        animator = GetComponent<Animator>(); // Animator 컴포넌트 가져오기

        if (mainCamera == null)
        {
            mainCamera = Camera.main.transform; // 메인 카메라 Transform이 지정되지 않은 경우, 메인 카메라 가져오기
        }
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        if (isJumping)
        {
            // Move the player towards the jump destination
            float step = jumpSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, jumpDirection, step);

            // Check if the player has reached the jump destination
            if (Vector3.Distance(transform.position, jumpDirection) < 0.1f)
            {
                isJumping = false;
            }
        }

        if (isGrounded && !isHoldingObject) // 땅에 닿아있는 경우에만 이동 가능
        {
            HandleMovement();
        }

        if (Input.GetKeyDown(KeyCode.F) && !isHoldingObject)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1.0f);  // 반경 1.0f 내의 모든 오브젝트를 감지
            foreach (var hitCollider in hitColliders)
            {
                InteractiveObject interactiveObject = hitCollider.GetComponent<InteractiveObject>();
                if (interactiveObject != null)
                {
                    isHoldingObject = true;
                    heldObject = interactiveObject;
                    interactiveObject.Interact();
                    break;
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.F) && isHoldingObject)
        {
            isHoldingObject = false;
            heldObject.StopInteract();
            heldObject = null;
        }

        if (isHoldingObject)
        {
            HandleObjectInteraction();
        }
    }
    private void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal"); // 수평 입력 값
        float z = Input.GetAxis("Vertical"); // 수직 입력 값

        Vector3 moveDir = (mainCamera.forward * z + mainCamera.right * x).normalized; // 이동 방향 계산
        moveDir.y = 0; // y 축 값은 유지하지 않음

        rb.velocity = new Vector3(moveDir.x * moveSpeed, rb.velocity.y, moveDir.z * moveSpeed); // 이동 속도 적용

        if (moveDir.magnitude > 0.1f) // 이동 방향이 일정 값보다 큰 경우
        {
            animator.SetBool("isWalking", true); // 걷는 애니메이션 활성화
            transform.rotation =
                Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDir), smoothTime); // 부드러운 회전 적용
        }
        else // 이동 방향이 작은 경우
        {
            animator.SetBool("isWalking", false); // 걷는 애니메이션 비활성화
        }
    }
    private void HandleObjectInteraction()
    {
        Vector3 moveDirection = Vector3.zero;
        float rotateAmount = 0f;

        if (Input.GetKey(KeyCode.W))
        {
            moveDirection = transform.forward;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            moveDirection = -transform.forward;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            if (Vector3.Dot(transform.forward, Vector3.forward) >= 0.5f || Vector3.Dot(transform.forward, Vector3.forward) <= -0.5f)
                moveDirection = -transform.right; // If the player is facing forward/backward, pressing A will move the object left
            else
                rotateAmount = -1f;  // If the player is facing left/right, pressing A will rotate the object left
        }
        else if (Input.GetKey(KeyCode.D))
        {
            if (Vector3.Dot(transform.forward, Vector3.forward) >= 0.5f || Vector3.Dot(transform.forward, Vector3.forward) <= -0.5f)
                moveDirection = transform.right; // If the player is facing forward/backward, pressing D will move the object right
            else
                rotateAmount = 1f;  // If the player is facing left/right, pressing D will rotate the object right
        }

        if (moveDirection != Vector3.zero)
        {
            Vector3 oldPosition = heldObject.transform.position;
            heldObject.Move(moveDirection);
            Vector3 movedDistance = heldObject.transform.position - oldPosition;
            transform.position += movedDistance;  // Player follows the object
        }
        if (rotateAmount != 0f)
        {
            heldObject.Rotate(rotateAmount);
        }
    }

    public void Jump(Vector3 destination)
    {
        isJumping = true;
        jumpDirection = destination;
        //animator.SetTrigger("isJumping"); // 점프 애니메이션 활성화
    }
}
