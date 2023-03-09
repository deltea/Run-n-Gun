using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bit : MonoBehaviour
{

    public float startForce = 5;

    Rigidbody2D bitBody;

    void Start() {
        bitBody = GetComponent<Rigidbody2D>();

        bitBody.AddForce(transform.up * startForce, ForceMode2D.Impulse);
    }

    void OnTriggerEnter2D(Collider2D trigger) {
        if (trigger.CompareTag("Player"))
        {
            Collect();
        }
    }

    private void Collect() {
        Destroy(gameObject);
        BitManager.Instance.GainBits(1);
    }

}
