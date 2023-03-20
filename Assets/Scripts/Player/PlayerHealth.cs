using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int health = 100;
    
    #region Singleton
    
    static public PlayerHealth Instance = null;
    void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }
    
    #endregion
    
    Slider healthBar;

    void Start() {
        healthBar = GameObject.Find("Health Bar").GetComponent<Slider>();
        UpdateHealthBar();
    }

    public void GetHurt(int damage) {
        // TimeManager.Instance.Hitstop(0.08f);

        health -= damage;
        UpdateHealthBar();
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die() {
        print("You died.");
    }

    private void UpdateHealthBar() {
        healthBar.value = (float)health / (float)maxHealth;
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GetHurt(20);
        }
    }

}
