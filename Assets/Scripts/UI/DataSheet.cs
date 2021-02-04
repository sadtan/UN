using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DataSheet : MonoBehaviour
{
    // 
    public static DataSheet current;
    public TextMeshProUGUI autorField;
    public TextMeshProUGUI nombreField;
    public TextMeshProUGUI fechaField;
    public TextMeshProUGUI tecField;
    public TextMeshProUGUI idField;
    public TextMeshProUGUI credField;
    public TextMeshProUGUI descField;
    public Sprite imageSprite;
    public GameObject image;
    void Awake() {
        current = this;
    }

    // Update is called once per frame
    void Update()
    {
        // imageField.g
    }

    public void SetContent(Obra obra) {
        // Debug.Log("[DataSheet] : Set Content: " + obra.nombre);
        autorField.text = obra.autor;
        nombreField.text = obra.nombre;
        fechaField.text = obra.anio;
        tecField.text = obra.tecnica;
        if (obra.id.Contains("ID"))
            idField.text = obra.id.Substring(3);
        else idField.text = obra.id;
        credField.text = obra.cred;
        descField.text = obra.descripcion;

        // load image
        Image imgField = image.GetComponent<Image>();
        LayoutElement imgLayout = image.GetComponent<LayoutElement>();

        imageSprite = Utilites.current.loadImage(obra.imagenPath);
        // imageSprite = Resources.Load<Sprite>("Sprites/Test/" + obra.imagenPath);
        // if (imageSprite == null)
        //     imageSprite = Resources.Load<Sprite>("Sprites/" + obra.imagenPath);

        // if (imageSprite == null) Debug.Log("[Gallery Item] : Image not found " + obra.id); // load a not found image

        // set default image layout values
        imgLayout.preferredWidth = 866;
        imgLayout.preferredHeight = 875;

        float maxHeight = 900f;
        float newHeight = imageSprite.rect.height * imgLayout.preferredWidth / imageSprite.rect.width;

        if (newHeight > maxHeight) { // 
            imgLayout.preferredHeight = maxHeight;
            imgLayout.preferredWidth = imageSprite.rect.width * maxHeight / imageSprite.rect.height;
        } else  
            imgLayout.preferredHeight = imageSprite.rect.height * imgLayout.preferredWidth / imageSprite.rect.width;

        imgField.sprite = imageSprite;
    }
}
