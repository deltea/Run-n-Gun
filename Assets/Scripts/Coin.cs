using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    public float startForce = 5;

    Rigidbody2D coinBody;

    void Start() {
        coinBody = GetComponent<Rigidbody2D>();

        coinBody.AddForce(transform.up * startForce, ForceMode2D.Impulse);
    }

    void OnTriggerEnter2D(Collider2D trigger) {
        if (trigger.CompareTag("Player"))
        {
            Collect();
        }
    }

    private void Collect() {
        Destroy(gameObject);
        CoinManager.Instance.GainCoins(1);
    }

}
