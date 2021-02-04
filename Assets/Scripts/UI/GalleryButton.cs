using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GalleryButton : MonoBehaviour, IPointerClickHandler
{
    public List<Obra> obras;
    public bool special = false;
    public void Update() {
        // UpdatePosition();
    }
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (pointerEventData.button == PointerEventData.InputButton.Left)
        {
            if (!special)
                UIEvents.current.OpenSwipeObraMenu(obras);
            else 
                TooltipSystem.current.OpenSwipeObraMenu(obras);
        }
    }


}
