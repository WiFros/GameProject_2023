using UnityEngine;

public class SimpleCharacterMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // 캐릭터 이동 속도
    public float jumpForce = 5f; // 점프 힘
    public float smoothTime = 0.1f; // 회전 시 부드러운 이동을 위한 시간
    public bool isGrounded = false; // 땅에 닿아있는지 여부
    public Transform mainCamera; // 메인 카메라 Transform

    private Rigidbody rb; // Rigidbody 컴포넌트
    private Animator animator; // Animator 컴포넌트

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Rigidbody 컴포넌트 가져오기
        animator = GetComponent<Animator>(); // Animator 컴포넌트 가져오기

        if (mainCamera == null)
        {
            mainCamera = Camera.main.transform; // 메인 카메라 Transform이 지정되지 않은 경우, 메인 카메라 가져오기
        }
    }

    void Update()
    {
        if (isGrounded) // 땅에 닿아있는 경우에만 이동과 점프 가능
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

            if (Input.GetButtonDown("Jump")) // 점프 버튼 입력
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // 위로 힘을 가해 점프
                isGrounded = false; // 땅에서 벗어남
                animator.SetTrigger("isJumping"); // 점프 애니메이션 활성화
                SoundManager.instance.PlaySoundEffect("Tag2");
            }
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ground") // 충돌한 오브젝트가 "Ground" 태그인 경우
        {
            isGrounded = true; // 땅에 닿아있음
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground") // 충돌한 오브젝트가 "Ground" 태그인 경우
        {
            isGrounded = false; // 땅에서 벗어남
        }
    }
}