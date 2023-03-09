using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BitManager : MonoBehaviour
{

    public int bits;
    public TMP_Text bitText;

    #region Singleton
    
    static public BitManager Instance = null;
    void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }
    
    #endregion
    
    public void GainBits(int newBits) {
        bits += newBits * 5;
        UpdateCounter();
    }

    public void UpdateCounter() {
        string formatted = "";
        for (int i = 0; i < 5 - bits.ToString().Length; i++)
        {
            formatted += "0";
        }
        formatted += bits.ToString();
        bitText.text = formatted;
    }

}
