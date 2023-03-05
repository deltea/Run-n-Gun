using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float maxHealth = 100;
    public float health = 100;

    Shake camShake;

    void Start() {
        camShake = Camera.main.GetComponent<Shake>();
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            GetHurt(20);
        }
    }

    private void GetHurt(float damage) {
        health -= damage;
        if (health <= 0)
        {
            Die();
        } else
        {
            camShake.ShakeIt(0.05f, 0.05f);
        }
    }

    private void Die() {
        camShake.ShakeIt(0.2f, 0.3f);
        Destroy(gameObject);
    }

}
