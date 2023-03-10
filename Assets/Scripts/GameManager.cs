using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    public int startingEnemyCount = 5;
    public int enemyCount;
    [System.NonSerialized] public bool roomCleared = false;
    public GameObject portalPrefab;
    public int rooms = 2;

    FollowCamera followCam;
    
    static public GameManager Instance = null; 

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else if (Instance != this) {
            Destroy(gameObject);
        }

        Reset();
    }

    public void ClearedRoom() {
        print("Room cleared.");
        Instantiate(portalPrefab, Vector3.zero, Quaternion.identity);
    }
    
    public void EnterPortal() {
        print("Entered portal.");

        Reset();
        if (followCam != null) followCam.isFollowing = false;
        PlayerMovement.Instance.StopMoving();
        PlayerShooting.Instance.canShoot = false;

        SceneManager.LoadScene("Choosing");
    }

    public void ChooseCard() {
        print("Chosen card.");
        SceneManager.LoadScene("Room 1-" + Random.Range(0, rooms));
    }

    public void Reset() {
        startingEnemyCount = GameObject.FindObjectsOfType<Enemy>().Length;
        enemyCount = startingEnemyCount;
        roomCleared = false;
        followCam = Camera.main.GetComponent<FollowCamera>();
        // Spawn enemies
    }

    public void GiveUp() {
        print("Gave up.");
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
