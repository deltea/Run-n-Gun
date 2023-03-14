using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{

    public bool canShoot = true;
    public Transform gunPivot;
    public Transform gun;
    [HideInInspector] public Gun currentGun;

    private bool firing;
    private float nextTimeToFire;

    #region Singleton
    
    static public PlayerShooting Instance = null;
    void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }
    
    #endregion

    void Start() {
        currentGun = gun.GetComponent<Gun>();
        gunPivot.parent = null;
    }

    void Update() {
        RotateGun();

        if (firing && Time.time >= nextTimeToFire && canShoot)
        {
            nextTimeToFire = Time.time + 1 / currentGun.fireRate;
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
        GameObject bullet = Instantiate(currentGun.bulletPrefab.gameObject, currentGun.firePoint.position, gunPivot.rotation * Quaternion.Euler(0, 0, Random.Range(-currentGun.randomness, currentGun.randomness)));
        Rigidbody2D bulletBody = bullet.GetComponent<Rigidbody2D>();
        bulletBody.AddRelativeForce(currentGun.fireForce * Vector2.up, ForceMode2D.Impulse);
        currentGun.Kick();
        // currentGun.Flash();
    }

    // Input system
    void OnShoot(InputValue value) {
        firing = value.Get<float>() > 0 ? true : false;
    }

}
