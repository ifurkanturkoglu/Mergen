//using UnityEngine;
//public class CharacterController : MonoBehaviour
//{
//    public float damp;
//    [Range(1f, 20f)]
//    public float rotationSpeed;

//    float inputX;
//    float inputY;
//    float maxSpeed;

//    public Transform Model;

//    Vector3 StickDirection;
//    Animator Anim;
//    Camera mainCamera;

//    [SerializeField] KeyCode SprintButton;
//    [SerializeField] KeyCode WalkButton;

//    private void Start()
//    {
//        Anim = GetComponent<Animator>();
//        mainCamera = Camera.main;
//    }

//    private void LateUpdate()
//    {
//        Movement();
//        InputMove();
//        InputRotation();
//    }

//    private void Movement()
//    {
//        StickDirection = new Vector3(inputX, 0, inputY);

//        if (Input.GetKey(SprintButton))
//        {
//            maxSpeed = 2f;
//            inputX = 2 * Input.GetAxis("Horizontal");
//            inputY = 2 * Input.GetAxis("Vertical");
//        }
//        else if (Input.GetKey(WalkButton))
//        {
//            maxSpeed = 0.4f;
//            inputX = Input.GetAxis("Horizontal");
//            inputY = Input.GetAxis("Vertical");
//        }
//        else
//        {
//            maxSpeed = 1f;
//            inputX = Input.GetAxis("Horizontal");
//            inputY = Input.GetAxis("Vertical");
//        }
//    }

//    void InputMove()
//    {
//        Anim.SetFloat("speed", Vector3.ClampMagnitude(StickDirection, maxSpeed).magnitude, damp, Time.deltaTime * 10);
//    }

//    void InputRotation()
//    {
//        Vector3 rotOfSet = mainCamera.transform.TransformDirection(StickDirection);
//        rotOfSet.y = 0;

//        Model.forward = Vector3.Slerp(Model.forward, rotOfSet, Time.deltaTime * rotationSpeed);
//    }
//}