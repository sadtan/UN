using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Gallery : MonoBehaviour {
    public static Gallery current;
    private TextAsset jsonFile;
    private Obras obrasJson;
    public GameObject Template;
    public GameObject Content;

    private List<Obra> gallery = null;
    void Awake() {
        current = this;

        string filePath = Application.streamingAssetsPath + "/Text/";
        string []paths = System.IO.Directory.GetFiles(filePath, "*.json");
        Debug.Log(paths[0]);
        string json = System.IO.File.ReadAllText(paths[0]);
        
        obrasJson = JsonUtility.FromJson<Obras>(json);
    }
    public List<Obra> GetGalleryByName(string galleryName) {
        List<Obra> gallery = new List<Obra>();
        
        foreach (Obra obra in obrasJson.obras)
        {
            if (obra.galeria == galleryName) {
                gallery.Add(obra);
            }
        }

        return gallery;
    }

    public void LoadGallery(string galleryName) {
        
        gallery = GetGalleryByName(galleryName);
        
        for (int i = 1; i < gallery.ToArray().Length; ++i) 
            Instantiate<GameObject>(Template, Content.transform);
    }

    // show new gallery 
    public void SetGallery() { 
        for (int i = 0; i < gallery.ToArray().Length; ++i) {
            Content.transform.GetChild(i).name = gallery.ToArray()[i].nombre;
            Content.transform.GetChild(i).GetComponent<GalleryItem>().LoadContent(gallery.ToArray()[i]);
        }
    }

    public void Unload() {
        Content.GetComponent<SwipeMenu>().UnloadChildren();
    }
}
