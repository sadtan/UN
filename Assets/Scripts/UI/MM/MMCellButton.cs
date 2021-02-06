using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MMCellButton : MonoBehaviour, IPointerClickHandler
{
    public Obra obra;
    public string obraID;
    public void Update() {
        // UpdatePosition();
    }

    public void Start() {
        
    }

    private Obra GetObraById(string ID) {
        foreach (Obra obra in UIEvents.current.obrasJson.obras)
        {
            if (obra.id == ID) {
                return obra;
            }
        }
        return null;
    }
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        obra = GetObraById(obraID);
        if (pointerEventData.button == PointerEventData.InputButton.Left)
        {
            UIEvents.current.OpenMMDataSheet(obra, true);
        }
    }
}
