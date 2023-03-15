using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{

    public bool roomCleared = false;
    public GameObject door;
    public Transform playerSpawnPoint;
    public Transform bounds;
    public LayerMask roomsLayer;
    public Vector2 size = new Vector2(20, 10);

    private int enemyCount;

    Enemy[] enemies;

    void Start() {
        enemies = transform.GetComponentsInChildren<Enemy>();
        enemyCount = enemies.Length;
        door.gameObject.SetActive(false);
    }

    public void ClearedRoom() {
        print("Activate doors");
        door.gameObject.SetActive(true);
    }

    public void RemoveEnemy() {
        enemyCount--;
        print(enemyCount);
        if (enemyCount <= 0)
        {
            roomCleared = true;
            ClearedRoom();
        }
    }
    
}
