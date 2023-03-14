using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsManager : MonoBehaviour
{

    public int currentRoomNum = 1;
    public Room currentRoom;
    public GameObject[] roomPrefabs;
    
    Transform player;

    #region Singleton
    
    static public RoomsManager Instance = null;
    void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }
    
    #endregion

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        CreateRoom();
    }

    public void CreateRoom() {
        currentRoomNum++;

        if (currentRoom != null) Destroy(currentRoom.gameObject);

        GameObject newRoom = Instantiate(roomPrefabs[Random.Range(0, roomPrefabs.Length)], Vector3.zero, Quaternion.identity);
        currentRoom = newRoom.GetComponent<Room>();

        foreach (Bit bit in GameObject.FindObjectsOfType<Bit>())
        {
            Destroy(bit.gameObject);
        }

        player.position = currentRoom.playerSpawnPoint.position;
    }

}
