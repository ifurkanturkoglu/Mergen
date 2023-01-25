using System;
using UnityEngine;
public class PController : MonoBehaviour
{
    public float damp;
    [Range(1f, 20f)]
    public float rotationSpeed;

    float inputX;
    float inputY;
    float maxSpeed;
    float speedFactor = 1f;
    float StrafeTurnSpeed = 6f;

    public Transform Model;

    Vector3 StickDirection;
    Camera mainCamera;

    [SerializeField] KeyCode SprintButton;
    [SerializeField] KeyCode WalkButton;
    Vector2 input;

    public enum MovementType
    {
        Directional,
        Strafe
    };
    public MovementType movementType;

    [HideInInspector] public Animator Anim;


    public static PController Instance;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
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
        input.x = speedFactor * Input.GetAxis("Horizontal");
        input.y = speedFactor * Input.GetAxis("Vertical");

        Anim.SetFloat("InputX", input.x);
        Anim.SetFloat("InputY", input.y);

        float yawCamera = mainCamera.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), StrafeTurnSpeed * Time.fixedDeltaTime);
        Anim.SetBool("StrafeMoving", input.x != 0 || input.y != 0);
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