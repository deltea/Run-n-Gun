using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{

    #region Singleton
    
    static public TimeManager Instance = null;
    void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }
    
    #endregion

    public void Hitstop(float delay) {
        StartCoroutine(HitstopRoutine(delay));
    }

    private IEnumerator HitstopRoutine(float delay) {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(delay);
        Time.timeScale = 1;
    }

}
