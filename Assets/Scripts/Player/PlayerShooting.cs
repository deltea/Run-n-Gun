using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{

    public Transform gunPivot;
    public Transform gun;

    void FixedUpdate() {
        RotateGun();
    }

    private void RotateGun() {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 direction = mousePosition - gunPivot.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        gunPivot.eulerAngles = angle * Vector3.forward;
        
        if (gunPivot.eulerAngles.z < 180)
        {
            gun.localEulerAngles = new Vector3(0, 180, 0);
        } else
        {
            gun.localEulerAngles = new Vector3(0, 0, 0);
        }
    }

}
