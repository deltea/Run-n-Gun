using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public float gunKick = 0.2f;
    public float gunKickSmoothing = 0.2f;
    public float gunKickRandomnes = 0.1f;
    public float gunKickRotationRandomness = 90;
    public Transform firePoint;

    [Header("Stats")]
    public float fireRate = 5;
    public float fireForce = 30;
    public float randomness = 5;
    public float damage = 20;
    public Bullet bulletPrefab;

    Vector2 originalGunPos;
    float originalGunRotation;

    void Start() {
        originalGunPos = transform.localPosition;
        originalGunRotation = transform.localRotation.z;
    }

    void Update() {
        transform.localPosition = Vector2.Lerp(transform.localPosition, originalGunPos, gunKickSmoothing);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(transform.localRotation.x, transform.localRotation.y, originalGunRotation), gunKickSmoothing);
    }

    public void Kick() {
        transform.localPosition = originalGunPos + (Vector2.down * gunKick) + Random.insideUnitCircle.normalized * gunKickRandomnes;
        transform.localRotation = Quaternion.Euler(transform.localRotation.x, transform.localRotation.y, Random.Range(-gunKickRotationRandomness, gunKickRotationRandomness));
    }

}
