using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class Card : MonoBehaviour, IPointerClickHandler
{

    public TMP_Text buffText;
    public TMP_Text nerfText;
    public Image cardImage;

    Buffs cardBuff;
    Nerfs cardNerf;

    void Awake() {
        VariableManager.Instance.RandomBuff(out cardBuff, out string buffDescription);
        VariableManager.Instance.RandomNerf(out cardNerf, out string nerfDescription);

        buffText.text = buffDescription;
        nerfText.text = nerfDescription;
    }

    public void OnPointerClick(PointerEventData data) {
        VariableManager.Instance.ActivateBuff(cardBuff);
        VariableManager.Instance.ActivateNerf(cardNerf);
        GameManager.Instance.ChooseCard();
    }

}
