using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObraItemButton : MonoBehaviour, IPointerClickHandler
{
    public Obra obra;
    public bool special = false;
    public void Update() {
        // UpdatePosition();
    }
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (pointerEventData.button == PointerEventData.InputButton.Left)
        {
            if (!special)
                UIEvents.current.OpenMMDataSheet(obra);
            else if (TooltipSystem.current != null) TooltipSystem.current.OpenDataSheet(obra);
        }
    }

}
