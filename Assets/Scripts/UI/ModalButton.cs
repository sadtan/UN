using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ModalButton : MonoBehaviour, IPointerClickHandler
{
    public Obra obra;
    public void Update() {
        // UpdatePosition();
    }
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (pointerEventData.button == PointerEventData.InputButton.Left)
        {
            TooltipSystem.current.OpenDataSheet(obra);
        }
    }


}
