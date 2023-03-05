using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingEnemy : MonoBehaviour
{

    public float startForce = 5;
    public float smoothing = 0.2f;
    public float bumpScale = 0.2f;

    Rigidbody2D enemyBody;
    Vector2 originalScale;

    void Start() {
        enemyBody = GetComponent<Rigidbody2D>();
        originalScale = transform.localScale;

        enemyBody.AddForce(Random.insideUnitCircle.normalized * startForce, ForceMode2D.Impulse);
    }

    void Update() {
        enemyBody.velocity = enemyBody.velocity.normalized * startForce;
        transform.localScale = Vector2.Lerp(transform.localScale, originalScale, smoothing);
    }

    void OnCollisionEnter2D(Collision2D collision) {
        transform.localScale = originalScale + Vector2.one * bumpScale;
    }

}
