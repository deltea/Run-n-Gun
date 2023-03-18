using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{

    public float health = 100;

    Shake shake;

    void Start() {
        shake = GetComponent<Shake>();
    }

    private void GetHurt(float damage) {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die() {
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            shake.ShakeIt(0.1f, 0.2f);
            GetHurt(collision.gameObject.GetComponent<Bullet>().gun.damage);
        }
    }

}
