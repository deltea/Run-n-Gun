using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public int maxHealth = 100;
    public int health = 100;
    
    #region Singleton
    
    static public PlayerHealth Instance = null;
    void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }
    
    #endregion

    public void GetHurt(int damage) {
        TimeManager.Instance.Hitstop(0.08f);

        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die() {
        print("You died.");
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GetHurt(20);
        }
    }

}
