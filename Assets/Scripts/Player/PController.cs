using System;
using UnityEngine;
public class PController : MonoBehaviour
{
    float damp = 3f;
    float inputX;
    float inputY;
    float maxSpeed;
    float rotationSpeed = 12f;
    float speedFactor = 1f;

    public Transform Model;
    public static PController Instance;

    Camera mainCamera;
    Rigidbody playerRb;
    Vector2 input;
    Vector3 StickDirection;

    [SerializeField] KeyCode SprintButton;
    [SerializeField] KeyCode WalkButton;
    [SerializeField] float jumpForce;
    [SerializeField] float StrafeTurnSpeed = 6f;
    [HideInInspector] public Animator Anim;

    public enum MovementType
    {
        Directional,
        Strafe
    };
    [HideInInspector] public MovementType movementType;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Anim = GetComponent<Animator>();
        mainCamera = Camera.main;
    }

    private void LateUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        if (Input.GetKey(SprintButton))
        {
            speedFactor = 2;
        }
        else if (Input.GetKey(WalkButton))
        {
            speedFactor = 0.4f;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Anim.SetTrigger("Jump");
            playerRb.AddForce(Vector3.up * jumpForce);
        }
        switch (movementType)
        {
            case MovementType.Directional:
                DirectionalMovement();
                DirectionalRotation();
                break;

            case MovementType.Strafe:
                StrafeMovement();
                break;
        }
        
    }

    private void StrafeMovement()
    {
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");
        Anim.SetFloat("InputX", input.x);
        Anim.SetFloat("InputY", input.y);

        Vector3 strafeMovement = new Vector3(input.x, 0, input.y);
        float yawCamera = mainCamera.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), StrafeTurnSpeed * Time.deltaTime);
        Anim.SetBool("StrafeMoving", input.x != 0 || input.y != 0);
        Anim.SetFloat("speed", Vector3.ClampMagnitude(strafeMovement, 1).magnitude);
    }
    void DirectionalMovement()
    {
        inputX = speedFactor * Input.GetAxis("Horizontal");
        inputY = speedFactor * Input.GetAxis("Vertical");
        StickDirection = new Vector3(inputX, 0, inputY);
        maxSpeed = speedFactor;
        Anim.SetFloat("speed", Vector3.ClampMagnitude(StickDirection, maxSpeed).magnitude, damp, Time.deltaTime * 10);
    }
    void DirectionalRotation()
    {
        Vector3 rotOfSet = mainCamera.transform.TransformDirection(StickDirection);
        rotOfSet.y = 0;

        if (StickDirection != Vector3.zero)
        {
            Model.forward = Vector3.Slerp(Model.forward, rotOfSet, Time.deltaTime * rotationSpeed);
        }
    }
}