using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    public int damage = 10;

    Rigidbody2D bulletBody;

    void Start() {
        bulletBody = GetComponent<Rigidbody2D>();

        bulletBody.AddRelativeForce(Vector2.up * VariableManager.Instance.enemyBulletSpeed, ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth.Instance.GetHurt(damage);
        }
        
        Destroy(gameObject);

    }

}
