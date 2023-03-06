using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{

    public int coins;

    #region Singleton
    
    static public CoinManager Instance = null;
    void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }
    
    #endregion
    
    public void GainCoins(int newCoins) {
        coins += newCoins;
    }

}
