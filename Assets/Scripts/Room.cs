using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{

    public bool roomCleared = false;
    public Door entranceDoor;
    public Door exitDoor;
    public Transform bounds;
    public Vector2 size = new Vector2(70, 45);
    public LayerMask roomsLayer;

    private int enemyCount;

    Enemy[] enemies;

    void Start() {
        enemies = transform.GetComponentsInChildren<Enemy>();
        enemyCount = enemies.Length;
        entranceDoor.gameObject.SetActive(true);
        exitDoor.gameObject.SetActive(false);
    }

    void Update() {
        Collider2D touchingOtherRooms = Physics2D.OverlapBox(transform.position, size, 0, roomsLayer);
        if (touchingOtherRooms != null && touchingOtherRooms != GetComponent<Collider2D>())
        {
            print("dfdadfdas");
            transform.position = Random.insideUnitCircle.normalized * 200;
        }
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

    void OnDrawGizmosSelected() {
        Gizmos.DrawWireCube(transform.position, size);
    }

}
