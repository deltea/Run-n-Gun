using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BitManager : MonoBehaviour
{

    private int bits;
    public TMP_Text bitText;
    [SerializeField] private float smoothing = 0.2f;
    [SerializeField] private float gainAnimation = 10;

    Vector2 originalPosition;

    #region Singleton
    
    static public BitManager Instance = null;
    void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }
    
    #endregion
    
    void Start() {
        originalPosition = bitText.transform.localPosition;
    }

    void Update() {
        bitText.transform.localPosition = Vector2.Lerp(bitText.transform.localPosition, originalPosition, smoothing);
    }

    public void GainBits(int newBits) {
        bits += newBits * 5;
        bitText.transform.localPosition = originalPosition + Vector2.up * gainAnimation;
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
