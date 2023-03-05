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
    public float gunKick = 0.2f;
    public float gunKickSmoothing = 0.2f;
    public float gunKickRandomnes = 0.1f;
    public float gunKickRotationRandomness = 90;

    [Header("Gun Stats")]
    public float fireRate = 5;
    public float fireForce = 5;

    private bool firing;
    private float nextTimeToFire;

    Vector2 originalGunPos;
    float originalGunRotation;

    void Start() {
        originalGunPos = gun.localPosition;
        originalGunRotation = gun.localRotation.z;
    }

    void Update() {
        RotateGun();

        if (firing && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1 / fireRate;
            Fire();
        }

        gun.transform.localPosition = Vector2.Lerp(gun.transform.localPosition, originalGunPos, gunKickSmoothing);
        gun.transform.localRotation = Quaternion.Lerp(gun.transform.localRotation, Quaternion.Euler(gun.transform.localRotation.x, gun.transform.localRotation.y, originalGunRotation), gunKickSmoothing);
    }

    private void RotateGun() {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 direction = mousePosition - gunPivot.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        gunPivot.eulerAngles = angle * Vector3.forward;
        
        if (gunPivot.eulerAngles.z < 180)
        {
            gun.localEulerAngles = new Vector3(gun.localEulerAngles.x, 180, gun.localEulerAngles.z);
        } else
        {
            gun.localEulerAngles = new Vector3(gun.localEulerAngles.x, 0, gun.localEulerAngles.z);
        }
    }

    private void Fire() {
        GameObject bullet = Instantiate(bulletPrefab.gameObject, firePoint.position, gunPivot.rotation);
        Rigidbody2D bulletBody = bullet.GetComponent<Rigidbody2D>();
        bulletBody.AddRelativeForce(fireForce * Vector2.up, ForceMode2D.Impulse);

        gun.transform.localPosition = originalGunPos + (Vector2.down * gunKick) + Random.insideUnitCircle.normalized * gunKickRandomnes;
        gun.transform.localRotation = Quaternion.Euler(gun.transform.localRotation.x, gun.transform.localRotation.y, Random.Range(-gunKickRotationRandomness, gunKickRotationRandomness));
    }

    // Input system
    void OnShoot(InputValue value) {
        firing = value.Get<float>() > 0 ? true : false;
    }

}
