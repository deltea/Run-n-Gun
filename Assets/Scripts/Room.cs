using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{

    public bool roomCleared = false;
    public Door entranceDoor;
    public Door exitDoor;
    public Transform bounds;

    private int enemyCount;

    Enemy[] enemies;

    void Start() {
        enemies = transform.GetComponentsInChildren<Enemy>();
        enemyCount = enemies.Length;
        entranceDoor.gameObject.SetActive(true);
        exitDoor.gameObject.SetActive(false);
    }

    public void ClearedRoom() {
        print("Room cleared.");
        exitDoor.gameObject.SetActive(true);
        print("Activate doors");
        RoomsManager.Instance.CreateRoom(this);
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
