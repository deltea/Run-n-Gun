using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    [SerializeField] private float speed = 8;
    [SerializeField] private int damage = 10;

    Rigidbody2D bulletBody;

    void Start() {
        bulletBody = GetComponent<Rigidbody2D>();

        bulletBody.AddRelativeForce(Vector2.up * speed, ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth.Instance.GetHurt(damage);
        }
        
        Destroy(gameObject);

    }

}
