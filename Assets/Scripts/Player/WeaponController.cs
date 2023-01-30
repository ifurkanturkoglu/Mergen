using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    bool isStrafe = false;
    [SerializeField] GameObject handWeapon;
    [SerializeField] GameObject backWeapon;

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

        if (Input.GetKeyDown(KeyCode.Mouse0) && isStrafe == true)
        {
            PController.Instance.Anim.SetTrigger("Attack");
        }
    }

    void Equip()
    {
        backWeapon.SetActive(false);
        handWeapon.SetActive(true);
    }
    void Unequip()
    {
        backWeapon.SetActive(true);
        handWeapon.SetActive(false);
    }
}
