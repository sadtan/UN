using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEvents : MonoBehaviour
{
    public static UIEvents current;
    // public GameObject MainMenu;
    public GameObject OverlayMenu;
    // public GameObject SalasMenu;
    // public GameObject SwipeMenu;
    public GameObject BackButton;
    public GameObject[] Menus;
    public Obras obrasJson;
    public int currentSala;    
    private Color[] backgroundColors;
    public GameObject[] backgrounds;
    private bool goToSalas = false;
    void Awake() {
        current = this;
        string filePath = Application.streamingAssetsPath + "/Text/";
        string []paths = System.IO.Directory.GetFiles(filePath, "*.json");
        string json = System.IO.File.ReadAllText(paths[0]);
        obrasJson = JsonUtility.FromJson<Obras>(json);
    }
    void Start() {
        // load info
        currentSala = 0;
        Debug.Log("UI Events Started");

        for (int i = 1; i < Menus.Length; ++i) 
            Menus[i].SetActive(false);
        
        OverlayMenu.SetActive(false);

        backgroundColors = new Color[4];

    }
    public void UpdateColors() {
        backgroundColors[0] = new Color32(77, 102, 45, 255);
        backgroundColors[1] = new Color32(129, 86, 28, 255 );
        backgroundColors[2] = new Color32(72 , 96, 114, 255);
        backgroundColors[3] = new Color32(115 , 136, 152, 255);
        Debug.Log("Sala: " + currentSala);
        for (int i = 0; i < backgrounds.Length; ++i) {
            backgrounds[i].GetComponent<Image>().color = backgroundColors[currentSala -1];
        }
    }
    private Obra GetObraById(string ID) {
        foreach (Obra obra in obrasJson.obras)
        {
            if (obra.id == ID) {
                return obra;
            }
        }
        return null;
    }
    public void OpenMainMenu() {
        for (int i = 0; i < Menus.Length; ++i) 
            Menus[i].SetActive(false);

        Menus[0].SetActive(true);
        OverlayMenu.SetActive(false);
    }
    public void OpenSalasMenu() {
        for (int i = 0; i < Menus.Length; ++i) 
            Menus[i].SetActive(false);
        
        Menus[1].SetActive(true);
        OverlayMenu.SetActive(true);
    }
    public void OpenSwipeMenu(int sala) {
        for (int i = 0; i < Menus.Length; ++i) 
            Menus[i].SetActive(false);
        
        Menus[2].SetActive(true);
        BackButton.SetActive(true);

        MMGallery.current.Unload();
        currentSala = sala;
        MMGallery.current.LoadGallery(sala);
        UpdateColors();
    }

    public void OpenSwipeObraMenu(List<Obra> obras) {
        for (int i = 0; i < Menus.Length; ++i) 
            Menus[i].SetActive(false);
        
        Menus[3].SetActive(true);
        BackButton.SetActive(true);
        MMSwipeObraMenu.current.Unload();
        MMSwipeObraMenu.current.LoadGallery(obras);
        // MMGallery.current.Unload();
    }

    public void OpenMMDataSheet(Obra obra, bool go = false) {
        for (int i = 0; i < Menus.Length; ++i) 
            Menus[i].SetActive(false);
        
        Menus[4].SetActive(true);
        MMDataSheetMenu.current.LoadContent(obra);
        BackButton.SetActive(true);
        goToSalas = go;
    }

    public void GoBack() {
        if (!goToSalas) {
            for (int i = 0; i < Menus.Length; ++i) {
                if (Menus[i].activeSelf) {
                    if (i == 2) {
                        BackButton.SetActive(false);
                        MMGallery.current.Content.GetComponent<SwipeMenu>().ResetScroll();
                    }
                    if (i == 1) {
                        OverlayMenu.SetActive(false);
                    }
                    if (i == 2) {
                        MMGallery.current.Unload();
                        // MMSwipeObraMenu.current.Unload();
                    }
                    if (i == 3) {
                        MMSwipeObraMenu.current.Unload();
                        MMSwipeObraMenu.current.Content.GetComponent<SwipeMenu>().ResetScroll();
                    }

                    Menus[i].SetActive(false);
                    Menus[i-1].SetActive(true);
                    break;
                }
            }
        } else {
            goToSalas = false;
            OpenSalasMenu();
        }

    }
}
