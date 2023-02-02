using System.Collections;
using UnityEngine;
public class Spider : MonoBehaviour
{
    [SerializeField] Transform player, target1, target2, target3, target4;
    Rigidbody rb;
    public float radius = .5f;
    public float angle;
    public float speed = 2f;
    float frontRightDistance, frontLeftDistance;
    bool isMove = true;
    bool rightLeg, leftLeg;
    float count;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //InvokeRepeating(nameof(legPosUpdate),0,1);
    }


    void FixedUpdate()
    {
        // float z = Mathf.Cos(Time.time*speed)*radius;
        // float y = Mathf.Sin(Time.time*speed)*radius;
        // target1.position = new Vector3(target1.position.x,y,z+target1.position.z);
        
        transform.position += Vector3.forward*.5f * Time.fixedDeltaTime;
        if(isMove)
            StartCoroutine(LegOrder());
       
        
        frontRightDistance = Vector3.Distance(target1.position, target3.position);
        frontLeftDistance = Vector3.Distance(target2.position, target4.position);
        //print(frontRightDistance);
        if (frontRightDistance >= 5 && rightLeg)
        {
            target3.position += Vector3.forward * Time.fixedDeltaTime;
        }
        if (frontLeftDistance >= 5 && leftLeg)
        {
            target2.position += Vector3.forward * Time.fixedDeltaTime;
        }

    }

    IEnumerator LegOrder()
    {
        isMove = false;
        while(count <= 1)
        {
            count +=Time.fixedDeltaTime;
            target1.position += Vector3.forward * .5f * Time.fixedDeltaTime;
            leftLeg = false;
            rightLeg = true;
            print("a");
            yield return null;
            // 
            
            // 
            // rightLeg = false;
            // leftLeg = true;
            // target1.position -= Vector3.forward * .5f * Time.fixedDeltaTime;
            // target4.position += Vector3.forward * .5f * Time.fixedDeltaTime;
            
            // yield return new WaitForSeconds(1);
            // count = 0;
            // isMove = true;
        }
        StartCoroutine(deneme());
    }
    IEnumerator deneme(){
        yield return new WaitForSeconds(1);
        count =0;
        while(count <= 1)
        {
            print("b");
            count +=Time.fixedDeltaTime;
            rightLeg = false;
            leftLeg = true;
            target1.position -= Vector3.forward * .5f * Time.fixedDeltaTime;
            target4.position += Vector3.forward * .5f * Time.fixedDeltaTime;
            yield return null;
        }//-li position kısmı da yapılacak.
        StartCoroutine(deneme2());
        
    }
    IEnumerator deneme2(){
        yield return new WaitForSeconds(1);
        count = 0;
        isMove = true;
    }

}
