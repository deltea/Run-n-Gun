using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemy : MonoBehaviour
{

    public EnemyBullet bulletPrefab;
    public float delay = 1;
    public float groupDelay = 0.1f;
    public int group = 5;
    public Transform graphics;
    public Transform firePoint;
    public float rotationRange = 45;
    public float rotationSmoothing = 0.05f;

    Transform player;
    Quaternion rotationToPlayer;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        StartCoroutine(ShootRoutine());
    }

    void Update() {
        Vector2 direction = player.position - transform.position;
        rotationToPlayer = Quaternion.LookRotation(Vector3.forward, Quaternion.identity * direction);
        graphics.rotation = Quaternion.Lerp(graphics.rotation, rotationToPlayer, rotationSmoothing);
    }

    private IEnumerator ShootRoutine() {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            for (int i = 0; i < group; i++)
            {
                rotationToPlayer *= Quaternion.Euler(0, 0, (rotationRange / group * i) - rotationRange / 2);
                Instantiate(bulletPrefab.gameObject, firePoint.position, rotationToPlayer);
                yield return new WaitForSeconds(groupDelay);
            }
        }
    }

}
