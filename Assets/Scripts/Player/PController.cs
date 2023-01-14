using UnityEngine;
public class PController : MonoBehaviour
{
    public float damp;
    [Range(1f, 20f)]
    public float rotationSpeed;

    float inputX;
    float inputY;
    float maxSpeed;
    float speedFactor = 1;


    public Transform Model;


    Vector3 StickDirection;
    Animator Anim;
    Camera mainCamera;

    [SerializeField] KeyCode SprintButton;
    [SerializeField] KeyCode WalkButton;

    private void Start()
    {
        Anim = GetComponent<Animator>();
        mainCamera = Camera.main;
    }

    private void LateUpdate()
    {
        Movement();
        InputMove(maxSpeed);
        InputRotation();
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
        else
        {
            speedFactor = 1f;
        }
        InputMove(speedFactor);
    }

    void InputMove(float speedFactor)
    {
        inputX = speedFactor * Input.GetAxis("Horizontal");
        inputY = speedFactor * Input.GetAxis("Vertical");
        StickDirection = new Vector3(inputX, 0, inputY);
        maxSpeed = speedFactor;
        Anim.SetFloat("speed", Vector3.ClampMagnitude(StickDirection, maxSpeed).magnitude, damp, Time.deltaTime * 10);
    }

    void InputRotation()
    {
        Vector3 rotOfSet = mainCamera.transform.TransformDirection(StickDirection);
        rotOfSet.y = 0;

        Model.forward = Vector3.Slerp(Model.forward, rotOfSet, Time.deltaTime * rotationSpeed);
    }
}