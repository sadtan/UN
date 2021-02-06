using System.Collections;
using System.Collections.Generic;
using TMPro;
using System.IO;
using UnityEngine;
using UnityEngine.UI;


public class MMObraItem : MonoBehaviour {
    // Start is called before the first frame update
    public TextMeshProUGUI headerField;
    public TextMeshProUGUI contentField;
    public Sprite imageSprite;
    public Image imageField;
    public ObraItemButton button;
    public void Awake() {
       
    }
    public void LoadContent(Obra obra, bool special = false) {
        // button = transform.gameObject.GetComponent<ModalButton>();
        if (!special) {
            button.obra = obra;
            button.special = false;
        }
        else {
            button.obra = obra;
            button.special = true;
        }
        // set the UI componentes
        imageField   = transform.GetChild(0).GetComponent<Image>();
        headerField  = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        contentField = transform.GetChild(2).GetComponent<TextMeshProUGUI>();

        // if (!special)

        // set the UI layot components

        // set default image layout values
        // imageField.rectTransform.sizeDelta.x; 
        
        // imgLayout.preferredWidth = 690;
        // imgLayout.preferredHeight = 495;

        // load image
        imageSprite = Utilites.current.loadImage(obra.imagenPath);
        imageField.sprite = imageSprite;

        if (!special) {
            float newHeight = 430;
            float maxHeight = 430f;
            if (imageSprite != null)
                newHeight = imageSprite.rect.height * 430 / imageSprite.rect.width;
            
            if (newHeight > maxHeight) { // 
                // imgLayout.preferredHeight = maxHeight;
                // imgLayout.preferredWidth = imageSprite.rect.width * maxHeight / imageSprite.rect.height;
                imageField.rectTransform.sizeDelta = new Vector2(imageSprite.rect.width * maxHeight / imageSprite.rect.height, maxHeight);
            } else if (imageSprite != null)
                imageField.rectTransform.sizeDelta = new Vector2(430, imageSprite.rect.height * 430 / imageSprite.rect.width);
                // imgLayout.preferredHeight = imageSprite.rect.height * imgLayout.preferredWidth / imageSprite.rect.width;

        } else {
            float newHeight = 408;
            float maxHeight = 408f;
            if (imageSprite != null)
                newHeight = imageSprite.rect.height * 408 / imageSprite.rect.width;
            
            if (newHeight > maxHeight) { // 
                // imgLayout.preferredHeight = maxHeight;
                // imgLayout.preferredWidth = imageSprite.rect.width * maxHeight / imageSprite.rect.height;
                imageField.rectTransform.sizeDelta = new Vector2(imageSprite.rect.width * maxHeight / imageSprite.rect.height, maxHeight);
            } else if (imageSprite != null)
                imageField.rectTransform.sizeDelta = new Vector2(408, imageSprite.rect.height * 408 / imageSprite.rect.width);
        }

        // load the new image
        
        // set the new text on UI
        

        if (!special) {
            if (obra.nombre.Length > 50)
                headerField.text  = obra.nombre.Substring(0, 50) + "...";
            else 
                headerField.text  = obra.nombre;
            if (obra.descripcion.Length > 85)
                contentField.text = obra.descripcion.Substring(0, 85) + "...";
            else 
                contentField.text = obra.descripcion;
        } else {

            if (obra.nombre.Length > 35)
                headerField.text  = obra.nombre.Substring(0, 35) + "...";
            else 
                headerField.text  = obra.nombre;
            
            if (contentField != null) {

                if (obra.descripcion.Length > 85)
                    contentField.text = obra.descripcion.Substring(0, 85) + "...";
                else 
                    contentField.text = obra.descripcion;
            }
        }
    }

}
