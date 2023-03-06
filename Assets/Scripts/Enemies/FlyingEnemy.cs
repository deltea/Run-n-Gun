using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{

    public float speed = 5;
    public float abovePlayerOffset = 1;
    public EnemyBullet bulletPrefab;
    public float delay = 1;
    public float groupDelay = 0.1f;
    public bool alerted = false;
    public float alertRange = 5;
    public int group = 3;
    public LayerMask playerLayer;

    Transform player;
    Rigidbody2D enemyBody;

    void Start() {
        enemyBody = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        StartCoroutine(ShootRoutine());
    }

    void Update() {
        if (alerted)
        {
            Vector3 abovePlayer = player.position + Vector3.up * abovePlayerOffset;
            Vector2 direction = abovePlayer - transform.position;
            enemyBody.AddForce(speed * direction);
        } else
        {
            alerted = Physics2D.OverlapCircle(transform.position, alertRange, playerLayer);
        }
    }

    private IEnumerator ShootRoutine() {
        while (true)
        {
            if (alerted)
            {
                yield return new WaitForSeconds(delay);
                for (int i = 0; i < group; i++)
                {
                    Vector2 direction = player.position - transform.position;
                    Instantiate(bulletPrefab.gameObject, transform.position, Quaternion.LookRotation(Vector3.forward, Quaternion.identity * direction));
                    yield return new WaitForSeconds(groupDelay);
                }
            } else
            {
                yield return null;
            }
        }

    }

    void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, alertRange);
    }

}
