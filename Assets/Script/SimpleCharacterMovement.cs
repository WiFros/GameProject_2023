using UnityEngine;

public class SimpleCharacterMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float smoothTime = 0.1f;
    public bool isGrounded = false;
    public Transform mainCamera;

    private Rigidbody rb;
    private Animator animator;

    private bool isActivated;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        if (mainCamera == null)
        {
            mainCamera = Camera.main.transform;
        }
    }

    void Update()
    {
        if (isGrounded && isActivated)
        {
            float x = Input.GetAxisRaw("Horizontal");
            float z = Input.GetAxisRaw("Vertical");

            Vector3 moveDir = (mainCamera.forward * z + mainCamera.right * x).normalized;
            moveDir.y = 0;

            rb.velocity = new Vector3(moveDir.x * moveSpeed, rb.velocity.y, moveDir.z * moveSpeed);

            if (moveDir.magnitude > 0.1f)
            {
                animator.SetBool("isWalking", true);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDir), smoothTime);
            }
            else
            {
                animator.SetBool("isWalking", false);
            }

            if (Input.GetButtonDown("Jump"))
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isGrounded = false;
                animator.SetTrigger("isJumping");
            }
        }
    }

    public void SetActive(bool value)
    {
        isActivated = value;
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }
}