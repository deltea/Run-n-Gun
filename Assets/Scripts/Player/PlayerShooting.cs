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
    public float randomness = 5;

    private bool firing;
    private float nextTimeToFire;

    Gun currentGun;

    void Start() {
        currentGun = gun.GetComponent<Gun>();
    }

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
        gunPivot.rotation = Quaternion.LookRotation(Vector3.forward, Quaternion.identity * direction);
        
        if (gunPivot.eulerAngles.z < 180)
        {
            gun.localEulerAngles = new Vector3(gun.localEulerAngles.x, 180, gun.localEulerAngles.z);
        } else
        {
            gun.localEulerAngles = new Vector3(gun.localEulerAngles.x, 0, gun.localEulerAngles.z);
        }
    }

    private void Fire() {
        GameObject bullet = Instantiate(bulletPrefab.gameObject, firePoint.position, gunPivot.rotation * Quaternion.Euler(0, 0, Random.Range(-randomness, randomness)));
        Rigidbody2D bulletBody = bullet.GetComponent<Rigidbody2D>();
        bulletBody.AddRelativeForce(fireForce * Vector2.up, ForceMode2D.Impulse);
        currentGun.Kick();
    }

    // Input system
    void OnShoot(InputValue value) {
        firing = value.Get<float>() > 0 ? true : false;
    }

}
