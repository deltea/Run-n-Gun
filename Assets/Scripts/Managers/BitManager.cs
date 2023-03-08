using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BitManager : MonoBehaviour
{

    public int bits;

    #region Singleton
    
    static public BitManager Instance = null;
    void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }
    
    #endregion
    
    public void GainBits(int newBits) {
        bits += newBits;
    }

}
