using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsManager : MonoBehaviour
{

    [HideInInspector] public int currentRoomNum = 0;
    [HideInInspector] public Room currentRoom;
    public GameObject[] roomPrefabs;
    public GameObject[] minibossRoomPrefabs;
    public GameObject bossRoomPrefab;
    public GameObject choosingRoom;
    
    Transform player;
    FollowCamera cam;

    #region Singleton
    
    static public RoomsManager Instance = null;
    void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }
    
    #endregion

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        cam = Camera.main.GetComponent<FollowCamera>();
        CreateRoom();
    }

    public void CreateRoom() {
        currentRoomNum++;
        GameObject roomPrefab = roomPrefabs[Random.Range(0, roomPrefabs.Length)];

        if (currentRoomNum == 5)
        {
            print("Miniboss fight");
            roomPrefab = minibossRoomPrefabs[Random.Range(0, minibossRoomPrefabs.Length)];
        } else if (currentRoomNum == 10) {
            roomPrefab = bossRoomPrefab;
        }

        if (currentRoom != null) Destroy(currentRoom.gameObject);

        GameObject newRoom = Instantiate(roomPrefab, Vector3.zero, Quaternion.identity);
        currentRoom = newRoom.GetComponent<Room>();

        foreach (Bit bit in GameObject.FindObjectsOfType<Bit>())
        {
            Destroy(bit.gameObject);
        }

        player.position = currentRoom.playerSpawnPoint.position;
        cam.ResetCamera();
    }

}
