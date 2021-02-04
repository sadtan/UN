using System.Collections;
using System.Collections.Generic;
using TMPro;

using UnityEngine;
using UnityEngine.UI;


public class MMGalleryItem : MonoBehaviour {
    // Start is called before the first frame update
    public TextMeshProUGUI headerField;
    public TextMeshProUGUI contentField;
    public LayoutElement contentLayout;
    public LayoutElement imgLayout;
    public Sprite[] imageSprites;
    public Image[] imageFields;
    public GalleryButton button;

    // public ItemButton button;
    public void Awake() {
       imageFields = new Image[4];
       imageSprites = new Sprite[4];
    }
    public void LoadContent(List<Obra> obras, int group, bool special = false) {
        // button = transform.gameObject.GetComponent<ModalButton>();
        if (!special) {
            button.obras = obras;
            button.special = false;
        }
        else {
            button.special = true;
            button.obras = obras;
        }
        
        // set the UI componentes
        imageFields[0]  = transform.GetChild(0).GetChild(0).GetComponent<Image>();
        imageFields[1]  = transform.GetChild(0).GetChild(1).GetComponent<Image>();
        imageFields[2]  = transform.GetChild(0).GetChild(2).GetComponent<Image>();
        imageFields[3]  = transform.GetChild(0).GetChild(3).GetComponent<Image>();
        headerField  = transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        // set the UI layot components
        // imgLayout     = transform.GetChild(0).GetComponent<LayoutElement>();
        // contentLayout = transform.GetChild(1).GetComponent<LayoutElement>();
        
        imageFields[0].sprite = null;
        imageFields[1].sprite = null;
        imageFields[2].sprite = null;
        imageFields[3].sprite = null;
        
        
        for (int i = 0; i < obras.ToArray().Length; ++i) {
            if (i < 4) {
                imageSprites[i] = Utilites.current.loadImage(obras.ToArray()[i].imagenPath);
                imageFields[i].sprite = imageSprites[i];
            }
        }

        if (!special)
            headerField.text  = "Grupo " + group;
        else
            headerField.text  = obras.ToArray()[0].galeria;
    }
}
