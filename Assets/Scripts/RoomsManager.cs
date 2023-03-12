using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsManager : MonoBehaviour
{

    public int currentRoomNum = 1;
    public Room currentRoom;
    public GameObject[] roomPrefabs;

    #region Singleton
    
    static public RoomsManager Instance = null;
    void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }
    
    #endregion

    public void CreateRoom(Room clearedRoom) {
        currentRoomNum++;
        GameObject newRoom = Instantiate(roomPrefabs[Random.Range(0, roomPrefabs.Length)], Random.insideUnitCircle.normalized * 200, Quaternion.identity);
        Room room = newRoom.GetComponent<Room>();

        Door newEntranceDoor = room.entranceDoor;
        Door oldExitDoor = clearedRoom.exitDoor;
        newEntranceDoor.connectedDoor = oldExitDoor;
        oldExitDoor.connectedDoor = newEntranceDoor;
    }

}
