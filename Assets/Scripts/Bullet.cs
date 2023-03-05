using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(Instantiate(ParticleManager.Instance.bulletMiss.gameObject, transform.position, Quaternion.identity), 1);
            Destroy(gameObject);
        }
    }

}
