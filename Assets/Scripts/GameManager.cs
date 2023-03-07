using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    public int startingEnemyCount = 5;
    [System.NonSerialized] public int enemyCount;
    [System.NonSerialized] public bool roomCleared = false;
    public GameObject portalPrefab;

    FollowCamera followCam;

    #region Singleton
    
    static public GameManager Instance = null;
    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else if (Instance != this) {
            Destroy(gameObject);
        }
    }
    
    #endregion

    void Start() {
        enemyCount = startingEnemyCount;
        followCam = Camera.main.GetComponent<FollowCamera>();
        // Spawn enemies
    }

    public void ClearedRoom() {
        print("Room cleared.");
        Instantiate(portalPrefab, Vector3.zero, Quaternion.identity);
    }
    
    public void EnterPortal() {
        print("Entered portal.");

        followCam.isFollowing = false;
        PlayerMovement.Instance.StopMoving();
        PlayerShooting.Instance.canShoot = false;

        SceneManager.LoadScene("Swirly");
    }

    public void RemoveEnemy() {
        enemyCount--;
        if (enemyCount <= 0)
        {
            roomCleared = true;
            ClearedRoom();
        }
    }

}
