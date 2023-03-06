using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingEnemy : MonoBehaviour
{

    public float startForce = 5;
    public EnemyBullet bulletPrefab;
    public float delay = 5;
    public float pause = 0.5f;

    private Vector2 lastVelocity;

    Rigidbody2D enemyBody;

    void Start() {
        enemyBody = GetComponent<Rigidbody2D>();
        
        StartCoroutine(ShootRoutine());
        enemyBody.AddForce(Random.insideUnitCircle.normalized * startForce, ForceMode2D.Impulse);
    }

    void Update() {
        enemyBody.velocity = enemyBody.velocity.normalized * startForce;
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

}
