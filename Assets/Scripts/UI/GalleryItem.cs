using System.Collections;
using System.Collections.Generic;
using TMPro;

using UnityEngine;
using UnityEngine.UI;


public class GalleryItem : MonoBehaviour {
    // Start is called before the first frame update
    public TextMeshProUGUI headerField;
    public TextMeshProUGUI contentField;
    public LayoutElement contentLayout;
    public LayoutElement imgLayout;
    public Sprite imageSprite;
    public Image imageField;
    public ModalButton button;
    public void Awake() {
       
    }
    public void LoadContent(Obra obra) {
        // button = transform.gameObject.GetComponent<ModalButton>();
        button.obra = obra;
        // set the UI componentes
        imageField   = transform.GetChild(0).GetChild(0).GetComponent<Image>();
        headerField  = transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
        contentField = transform.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>();

        // set the UI layot components
        imgLayout     = transform.GetChild(0).GetComponent<LayoutElement>();
        contentLayout = transform.GetChild(1).GetComponent<LayoutElement>();

        // set default image layout values
        imgLayout.preferredWidth = 690;
        imgLayout.preferredHeight = 495;

        // load image

        imageSprite = Utilites.current.loadImage(obra.imagenPath);
        // imageSprite = Resources.Load<Sprite>("Sprites/Test/" + obra.imagenPath);
        // if (imageSprite == null)
        //     imageSprite = Resources.Load<Sprite>("Sprites/" + obra.imagenPath);

        // if (imageSprite == null) Debug.Log("[Gallery Item] : Image not found " + obra.id); // load a not found image

        float maxHeight = 600f;
        float newHeight = imageSprite.rect.height * imgLayout.preferredWidth / imageSprite.rect.width;
        
        if (newHeight > maxHeight) { // 
            imgLayout.preferredHeight = maxHeight;
            imgLayout.preferredWidth = imageSprite.rect.width * maxHeight / imageSprite.rect.height;
        } else 
            imgLayout.preferredHeight = imageSprite.rect.height * imgLayout.preferredWidth / imageSprite.rect.width;

        // load the new image
        imageField.sprite = imageSprite;

        // set the new text on UI
        headerField.text  = obra.nombre;
        contentField.text = obra.descripcion;
    }
}
