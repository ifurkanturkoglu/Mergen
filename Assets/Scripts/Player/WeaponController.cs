using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    bool isStrafe = false;

    private void Update()
    {
        HandleInput();
        HandleStrafe();
    }

    private void HandleStrafe()
    {
        PController.Instance.Anim.SetBool("IsStrafe", isStrafe);

        if (isStrafe == true)
        {
            GetComponent<PController>().movementType = PController.MovementType.Strafe;
        }
        if (isStrafe == false)
        {
            GetComponent<PController>().movementType = PController.MovementType.Directional;
        }
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            isStrafe = !isStrafe;
        }
    }
}
