using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipSystem : MonoBehaviour {
    public static TooltipSystem current;
    public GameObject[] Menus;
    private bool goTo360 = false;
    private bool goToSwipeObra = true;

    // priavate GameObject currentModal;
    private void Awake() {
        current = this;
        // Show("ID_AUTOR_N_02");
    }

    private void Start() {
        ResetMenus();
    }

    private void ResetMenus() {
        foreach(GameObject menu in Menus) {
            menu.SetActive(false);
        }
    }
    public void ShowSpecialGallery(string[] galleryNames, string[] individuals) {
        ResetMenus();

        Menus[0].SetActive(true);
        // MMSpecialGallery.current.Unload();
        MMSpecialGallery.current.LoadGallery(galleryNames, individuals);
        MMSpecialGallery.current.SetGallery();
        GameEvents.current.closeButton.SetActive(true);
    }

    public void OpenDataSheet(Obra obra, bool goTo360 = false) {
        ResetMenus();
        Menus[1].SetActive(true);
        GameEvents.current.closeButton.SetActive(true);
        
        Menus[1].GetComponent<MMDataSheetMenu>().LoadContent(obra);

        if (goTo360)
            this.goTo360 = true;

        
        // current.currentSelected = null;
    }

    public void OpenSwipeObraMenu(List<Obra> obras) {
        ResetMenus();
        Menus[2].SetActive(true);

        MMSwipeObraMenu.current.ResetScroll();
        MMSwipeObraMenu.current.LoadGallery(obras);
    }

    public void GoBack() {
        // ResetMenus();
        // Utilites.current.Log("TooltipSystem", "" + goTo360);
        if (!goTo360) {
            for (int i = 0; i < Menus.Length; ++i) {
                
                if (Menus[i].activeSelf) {
                    if (i == 0) {
                        GameEvents.current.currentReticle.SetActive(true);
                        GameEvents.current.SetGlobalState("CamInteraction");
                        GameEvents.current.closeButton.SetActive(false);
                        CameraMovement.current.LerpFoV();
                        Menus[i].SetActive(false);
                        MMSpecialGallery.current.UnloadSpecial();
                        MMSpecialGallery.current.ResetScroll();
                        break;
                    }

                    Menus[i].SetActive(false);
                    // Utilites.current.Log("TooltipSystem", "" + i);
                    if (i == 1) {
                        if (MMSwipeObraMenu.current != null)
                            MMSwipeObraMenu.current.Unload();
                        // MMSwipeObraMenu.current.ResetScroll();
                    }

                    if (i == 2) {
                        MMSwipeObraMenu.current.Unload();
                        MMSwipeObraMenu.current.ResetScroll();
                        --i;   
                    } 
                    
                    Menus[i-1].SetActive(true);
                    break;
                }
            }
        } else {
            ResetMenus();
            GameEvents.current.closeButton.SetActive(false);
            GameEvents.current.currentReticle.SetActive(true);
            GameEvents.current.SetGlobalState("CamInteraction");
            CameraMovement.current.LerpFoV();
            goTo360 = false;
        }
    }
    
}
