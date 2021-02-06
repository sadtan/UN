using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MMGallery : MonoBehaviour {
    public static MMGallery current;
    public GameObject GroupTemplate;
    public GameObject Content;
    // public TextMeshProUGUI title;
    private Obra[] obras;
    private List<List<Obra>> groups;
    List<int> groupNumbers;
    void Awake() {
        current = this;

        string filePath = Application.streamingAssetsPath + "/Text/";
        string []paths = System.IO.Directory.GetFiles(filePath, "*.json");
        string json = System.IO.File.ReadAllText(paths[0]);

        obras = JsonUtility.FromJson<Obras>(json).obras;
    }
    void Start() {
        // current = this;
       
    }
    // public List<List<Obra>> GetGroupsBySala(int sala) {
    public void GetGroupsBySala(int sala) {
        // List<Obra> gallery = new List<Obra>();
        groups = new List<List<Obra>>();
        
        groupNumbers = new List<int>();
        foreach (Obra obra in obras) {
            // if (!groupNumbers.Contains(obra.grupo) && obra.grupo != 0 && obra.sala == UIEvents.current.currentSala) {
            if (!groupNumbers.Contains(obra.grupo) && obra.sala == UIEvents.current.currentSala) {
                groupNumbers.Add(obra.grupo);
            }
        }

        groupNumbers.Sort();
        
        for (int i = 0; i < groupNumbers.ToArray().Length; ++i) {
            groups.Add(new List<Obra>());
            foreach (Obra obra in obras) 
                if (obra.grupo == groupNumbers.ToArray()[i] && obra.sala == UIEvents.current.currentSala) 
                    groups.ToArray()[i].Add(obra);
        }

        // for (int i = 0; i < groups.ToArray().Length; ++i) {
        //     Debug.Log("[MMGallery] : " + "Group: " + groupNumbers.ToArray()[i]);
        //     foreach(Obra obra in groups.ToArray()[i]) 
        //         Debug.Log("[MMGallery] : " + obra.nombre);
        // }
    }

    public void LoadGallery(int sala) {
        Unload();
        GetGroupsBySala(sala);
        // intantiate objects
        for (int i = 1; i < groups.ToArray().Length; ++i) {
            // Debug.Log("[MMGallery] : " + "Intantiate group: " + groupNumbers.ToArray()[i]);
            Instantiate<GameObject>(GroupTemplate, Content.transform);
        }

        for (int i = 0; i < groups.ToArray().Length; ++i) {
            Content.transform.GetChild(i).name = ("Grupo " + groupNumbers.ToArray()[i]);
            Content.transform.GetChild(i).GetComponent<MMGalleryItem>().LoadContent(groups.ToArray()[i], groupNumbers.ToArray()[i]);
        }


        // for (int i = 1; i < gallery.ToArray().Length; ++i) 
        //     Instantiate<GameObject>(Template, Content.transform);
    }

    // show new gallery 
    public void SetGallery() { 
        // for (int i = 0; i < gallery.ToArray().Length; ++i) {
        //     Content.transform.GetChild(i).name = gallery.ToArray()[i].nombre;
        //     Content.transform.GetChild(i).GetComponent<GalleryItem>().LoadContent(gallery.ToArray()[i]);
        // }
    }

    public void Unload() {
        Content.GetComponent<SwipeMenu>().UnloadChildren();
    }
}
