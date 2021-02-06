using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// [ExecuteInEditMode]
public class Tooltip : MonoBehaviour {
    public TextMeshProUGUI headerField;
    public TextMeshProUGUI contentField;
    // public TextMeshProUGUI contentField;
    public LayoutElement layoutElement;
    public Sprite imageSprite;
    public Image imageField;
    public RectTransform contentModalTransform;
    public int characterWrapLimit;
    public bool check = true;
    private TextAsset jsonFile;
    private Obras obrasJson;
    public ModalButton button;

    public void Awake() {

        string filePath = Application.streamingAssetsPath + "/Text/";
        string []paths = System.IO.Directory.GetFiles(filePath, "*.json");
        Debug.Log(paths[0]);
        string json = System.IO.File.ReadAllText(paths[0]);
        
        imageSprite = Resources.Load<Sprite>("Sprites/rose");
        if (check) 
            obrasJson = JsonUtility.FromJson<Obras>(json);
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
    public void Start() {
        // button = transform.gameObject.GetComponent<ModalButton>();
    }
    public void SetText(string ID) {
        if (check) {
            Obra currentObra = GetObraById(ID);
            button.obra = currentObra;
            if (currentObra  != null) {
                headerField.text  = currentObra.nombre;
                contentField.text = currentObra.descripcion;
                // Debug.Log(currentObra.medidas);

                imageSprite = Utilites.current.loadImage(currentObra.imagenPath);
                // imageSprite = Resources.Load<Sprite>("Sprites/Test/" + currentObra.imagenPath);
                // if (imageSprite == null)
                //     imageSprite = Resources.Load<Sprite>("Sprites/" + currentObra.imagenPath);

                LayoutElement parentLayout = imageField.transform.parent.gameObject.GetComponent<LayoutElement>();

                float maxWidth  = 920f;
                float minWidth  = 500f;
                float maxHeight = 600f;
                transform.parent.GetComponent<VerticalLayoutGroup>().padding.top = 0;

                // 
                if (imageSprite.rect.width <= minWidth) {
                    layoutElement.preferredWidth = 920f;
                    parentLayout.preferredWidth = imageSprite.rect.width;
                    parentLayout.preferredHeight = imageSprite.rect.height;
                    transform.parent.GetComponent<VerticalLayoutGroup>().padding.top = 40;

                } else if (imageSprite.rect.height * maxWidth / imageSprite.rect.width > maxHeight) {
                    // Debug.Log(imageSprite.rect.height * maxWidth / imageSprite.rect.width);
                    
                    parentLayout.preferredWidth  = imageSprite.rect.width * maxHeight / imageSprite.rect.height;
                    parentLayout.preferredHeight  = maxHeight;

                    if (imageSprite.rect.width * maxHeight / imageSprite.rect.height < 700) {
                        layoutElement.preferredWidth = 920; 
                        
                        transform.parent.GetComponent<VerticalLayoutGroup>().padding.top = 40;
                    } else {
                        // Debug.Log("?");
                        layoutElement.preferredWidth = imageSprite.rect.width * maxHeight / imageSprite.rect.height;
                    }
                        
                    
                } else if (imageSprite.rect.width > maxWidth) {
                    parentLayout.preferredWidth  = maxWidth;
                    layoutElement.preferredWidth = 920;
                    parentLayout.preferredHeight = imageSprite.rect.height * maxWidth / imageSprite.rect.width;
                // } else if (imageSprite.rect.width <= minWidth) {
                } 
                // else { 
                    
                //     layoutElement.preferredWidth = 920f;
                //     parentLayout.preferredWidth = imageSprite.rect.width;
                //     parentLayout.preferredHeight = imageSprite.rect.height;
                //     transform.parent.GetComponent<VerticalLayoutGroup>().padding.top = 40;
                // } 
                // else {
                //     parentLayout.preferredWidth  = imageSprite.rect.width;
                //     layoutElement.preferredWidth = imageSprite.rect.width;
                //     parentLayout.preferredHeight = imageSprite.rect.height;
                // }
                
                imageField.sprite = imageSprite;

                
            } else {
                headerField.text  = "No encontrado";
                contentField.text = "No encontrado";
                imageField.sprite = null;
            }
        }
    }
}

    // private void Update() {

    //     if ( headerField != null ) {
    //         int headerLenght  = headerField.text.Length;
    //         int contentLenght = contentField.text.Length;
    //         layoutElement.enabled = (headerLenght > characterWrapLimit || contentLenght > characterWrapLimit) ? true : false;
    //     }
    // }

     // if ( headerField != null  && headerField != null ) {
    // if ( headerField != null  && headerField != null ) {
        // Debug.Log("Set text: " + content);
        // if (string.IsNullOrEmpty(header)) 
        //     headerField.gameObject.SetActive(false);
        // else 
        //     headerField.gameObject.SetActive(true);
    // if ( headerField != null )
    