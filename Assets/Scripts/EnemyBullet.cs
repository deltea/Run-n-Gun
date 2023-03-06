using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    public float force = 5;
    public int damage = 10;

    Rigidbody2D bulletBody;

    void Start() {
        bulletBody = GetComponent<Rigidbody2D>();

        bulletBody.AddRelativeForce(Vector2.up * force, ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth.Instance.GetHurt(damage);
        }

        Destroy(gameObject);
    }

}
