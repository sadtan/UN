using System.Collections;
using System.Collections.Generic;
using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class MMDataSheetMenu : MonoBehaviour {
    // Start is called before the first frame update
    public static MMDataSheetMenu current;
    public TextMeshProUGUI autorField;
    public TextMeshProUGUI titleField;
    public TextMeshProUGUI anioField;
    public TextMeshProUGUI tecField;
    public TextMeshProUGUI idField;
    public TextMeshProUGUI credField;
    public TextMeshProUGUI descField;
    public Sprite imageSprite;
    public Image imageField;
    public void Awake() {
       current = this;
    }
    public void LoadContent(Obra obra) {
        
        imageSprite = Utilites.current.loadImage(obra.imagenPath);
        imageField.sprite = imageSprite;
        float newHeight = 790;
        float maxHeight = 790f;
        if (imageSprite != null)
            newHeight = imageSprite.rect.height * 550 / imageSprite.rect.width;
        
        if (newHeight > maxHeight) { // 
            // imgLayout.preferredHeight = maxHeight;
            // imgLayout.preferredWidth = imageSprite.rect.width * maxHeight / imageSprite.rect.height;
            imageField.rectTransform.sizeDelta = new Vector2(imageSprite.rect.width * maxHeight / imageSprite.rect.height, maxHeight);
        } else if (imageSprite != null)
            imageField.rectTransform.sizeDelta = new Vector2(550, imageSprite.rect.height * 550 / imageSprite.rect.width);
            // imgLayout.preferredHeight = imageSprite.rect.height * imgLayout.preferredWidth / imageSprite.rect.width;
        
        // load the new image
        
        // set the new text on UI
        autorField.text = obra.autor;
        titleField.text  = obra.nombre;
        anioField.text  = obra.anio;
        tecField.text  = obra.tecnica;
        if (obra.id.Contains("ID"))
            idField.text  = obra.id.Substring(3);
        else idField.text  = obra.id;
        credField.text  = obra.cred;
        descField.text  = obra.descripcion;
        
    }
}
