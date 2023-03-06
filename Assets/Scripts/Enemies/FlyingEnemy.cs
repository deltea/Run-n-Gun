using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{

    public float speed = 5;
    public float abovePlayerOffset = 1;
    public EnemyBullet bulletPrefab;
    public float delay = 1;

    Transform player;
    Rigidbody2D enemyBody;

    void Start() {
        enemyBody = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        StartCoroutine(ShootRoutine());
    }

    void FixedUpdate() {
        Vector3 abovePlayer = player.position + Vector3.up * abovePlayerOffset;
        Vector2 direction = abovePlayer - transform.position;
        enemyBody.AddForce(speed * direction);
    }

    private IEnumerator ShootRoutine() {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            Vector2 direction = player.position - transform.position;
            Instantiate(bulletPrefab.gameObject, transform.position, Quaternion.LookRotation(Vector3.forward, Quaternion.identity * direction));
        }

    }

}
