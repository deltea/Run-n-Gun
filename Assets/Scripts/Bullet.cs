using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [System.NonSerialized] public Gun gun;

    void Start() {
        gun = PlayerShooting.Instance.currentGun;
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(Instantiate(ParticleManager.Instance.bulletMiss.gameObject, transform.position, Quaternion.identity), 1);
        }
        
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D trigger) {
        if (trigger.CompareTag("Bullet Bounds"))
        {
            Destroy(gameObject);
        }
    }

}
