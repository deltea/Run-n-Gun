using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GiveUp : MonoBehaviour, IPointerClickHandler
{

    public void OnPointerClick(PointerEventData data) {
        GameManager.Instance.GiveUp();
    }


}
