using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{

    public Transform gunPivot;
    public Transform gun;
    public Transform firePoint;
    public Bullet bulletPrefab;

    [Header("Gun Stats")]
    public float fireRate = 5;
    public float fireForce = 5;

    private bool firing;
    private float nextTimeToFire;

    void Update() {
        RotateGun();

        if (firing && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1 / fireRate;
            Fire();
        }
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

    private void Fire() {
        GameObject bullet = Instantiate(bulletPrefab.gameObject, firePoint.position, gunPivot.rotation);
        Rigidbody2D bulletBody = bullet.GetComponent<Rigidbody2D>();
        bulletBody.AddRelativeForce(fireForce * Vector2.up, ForceMode2D.Impulse);
    }

    // Input system
    void OnShoot(InputValue value) {
        firing = value.Get<float>() > 0 ? true : false;
    }

}
