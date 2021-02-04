using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CloseButton : MonoBehaviour, IPointerClickHandler
{
    public void Update() {
        // UpdatePosition();
    }
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (pointerEventData.button == PointerEventData.InputButton.Left)
        {
            TooltipSystem.current.GoBack();
        }
    }

}
