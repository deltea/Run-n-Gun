using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float health = 100;
    [SerializeField] private Bit bitPrefab;
    [SerializeField] private int bitsDropMin = 3;
    [SerializeField] private int bitsDropMax = 6;

    Shake camShake;
    SpriteGraphics[] graphicsRenderers;

    void Start() {
        camShake = Camera.main.GetComponent<Shake>();
        graphicsRenderers = GetComponentsInChildren<SpriteGraphics>();
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            GetHurt(collision.gameObject.GetComponent<Bullet>().gun.damage);
        }
    }

    private void GetHurt(float damage) {
        health -= damage;
        RenderHurt();
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
        ParticleManager.Instance.PlayParticle(ParticleManager.Instance.enemySplat, transform.position, Quaternion.identity);
        Destroy(gameObject);
        BossRoom.Instance.ShowDoor();

        for (int i = 0; i < Random.Range(bitsDropMin, bitsDropMax); i++)
        {
            Instantiate(bitPrefab.gameObject, transform.position, Quaternion.Euler(0, 0, Random.Range(-90, 90)));
        }
    }

    private void RenderHurt() {
        foreach (SpriteGraphics graphic in graphicsRenderers)
        {
            graphic.Flash();
            graphic.Bump();
        }
    }

}
