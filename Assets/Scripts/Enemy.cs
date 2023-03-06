using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float maxHealth = 100;
    public float health = 100;
    public int coinsDropMin = 3;
    public int coinsDropMax = 6;
    public Coin coinPrefab;

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
        TimeManager.Instance.Hitstop(0.05f);
        Destroy(gameObject);

        for (int i = 0; i < Random.Range(coinsDropMin, coinsDropMax); i++)
        {
            Instantiate(coinPrefab.gameObject, transform.position, Quaternion.Euler(0, 0, Random.Range(-90, 90)));
        }
    }

}
