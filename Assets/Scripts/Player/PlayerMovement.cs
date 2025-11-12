using UnityEngine;


public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public float jumpBlockHeight = 5f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private float originalJumpHeight;
    Vector3 velocity;
    bool isGrounded;

    private Vector3 lastPlatformPos;

    // Update is called once per frame

    private void Awake()
    {
        originalJumpHeight = jumpHeight;
        controller = GetComponent<CharacterController>();
    }
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            controller.slopeLimit = 45.0f;
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        if (move.magnitude > 1)
            move /= move.magnitude;

        //controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }

        velocity.y += gravity * Time.deltaTime;

        Vector3 finalMove = (move * speed) + velocity;

        controller.Move(finalMove * Time.deltaTime);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.gameObject.layer == LayerMask.NameToLayer("JumpBlock"))
        {
            jumpHeight = jumpBlockHeight;
            Jump();
        }
        else if (hit.collider.CompareTag("MovingPlatform"))
        {
            transform.SetParent(hit.collider.transform); //플레이어가 플랫폼 위에 있을 때 잠시 플랫폼의 자식으로 붙여 transform의 값을 변경함.
        }
        else
        {
            transform.SetParent(null);
            jumpHeight = originalJumpHeight;
        }
    }

    private void Jump()
    {
        controller.slopeLimit = 100.0f;
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }
}
