using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingEnemy : MonoBehaviour
{

    public float startForce = 5;
    public float smoothing = 0.2f;
    public float bumpScale = 0.2f;
    public Transform graphics;
    public EnemyBullet bulletPrefab;
    public float delay = 5;
    public float pause = 0.5f;

    private Vector2 lastVelocity;

    Rigidbody2D enemyBody;
    Vector2 originalScale;

    void Start() {
        enemyBody = GetComponent<Rigidbody2D>();
        originalScale = graphics.localScale;
        
        StartCoroutine(ShootRoutine());
        enemyBody.AddForce(Random.insideUnitCircle.normalized * startForce, ForceMode2D.Impulse);
    }

    void Update() {
        enemyBody.velocity = enemyBody.velocity.normalized * startForce;
        graphics.localScale = Vector2.Lerp(graphics.localScale, originalScale, smoothing);
    }

    private IEnumerator ShootRoutine() {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            lastVelocity = enemyBody.velocity;
            enemyBody.velocity = Vector2.zero;
            yield return new WaitForSeconds(pause);
            Shoot();
            enemyBody.velocity = lastVelocity;
        }
    }

    private void Shoot() {
        Instantiate(bulletPrefab, transform.position, Quaternion.LookRotation(Vector3.forward, Quaternion.identity * -lastVelocity));
    }

    void OnCollisionEnter2D(Collision2D collision) {
        graphics.localScale = originalScale + Vector2.one * bumpScale;
    }

}
