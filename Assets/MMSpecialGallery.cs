using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class MMSpecialGallery : MonoBehaviour {
    public static MMSpecialGallery current;
    public GameObject GroupTemplate;
    public GameObject IndividualTemplate;
    public GameObject Content;
    // public TextMeshProUGUI title;
    private Obra[] obras;
    private List<List<Obra>> groups;
    private List<Obra> individualsList;
    List<string> galleryNames;
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
    public void GetGroupsByGallery(string[] gallery, string[] individuals) {
        // List<Obra> gallery = new List<Obra>();
        groups = new List<List<Obra>>();
        individualsList = new List<Obra>();
        
        galleryNames = new List<string>();

        foreach (Obra obra in obras) {
            if (!galleryNames.Contains(obra.galeria) && System.Array.IndexOf(individuals, obra.id) == -1 && System.Array.IndexOf(gallery, obra.galeria) != -1) {
                galleryNames.Add(obra.galeria);
            }
        }
        galleryNames.Sort();
        
        for (int i = 0; i < galleryNames.ToArray().Length; ++i) {
            groups.Add(new List<Obra>());
            foreach (Obra obra in obras) 
                if (obra.galeria == galleryNames.ToArray()[i] && System.Array.IndexOf(individuals, obra.id) == -1 ) 
                    groups.ToArray()[i].Add(obra);
        }

        for (int i = 0; i < individuals.Length; ++i) {
            foreach (Obra obra in obras) 
                if ( System.Array.IndexOf(individuals, obra.id) != -1 && obra.id == individuals[i]) 
                    individualsList.Add(obra);
        }

        // for (int i = 0; i < groups.ToArray().Length; ++i) {
        //     Debug.Log("[MMGallery] : " + "Group: " + groupNumbers.ToArray()[i]);
        //     foreach(Obra obra in groups.ToArray()[i]) 
        //         Debug.Log("[MMGallery] : " + obra.nombre);
        // }
    }

    public void LoadGallery(string[] gallery, string[] individuals) {
        // Unload();

        GetGroupsByGallery(gallery, individuals);
        HorizontalLayoutGroup layout = Content.GetComponent<HorizontalLayoutGroup>();
        layout.padding.right = 756;
        layout.padding.left = 756;
        // intantiate objects

        for (int i = 0; i < galleryNames.ToArray().Length; ++i) {
            Utilites.current.Log("MMSpecialGallery", galleryNames.ToArray()[i]);
        }

        if (groups.ToArray().Length > 0) {
            for (int i = 1; i < groups.ToArray().Length; ++i) {
            // Debug.Log("[MMGallery] : " + "Intantiate group: " + groupNumbers.ToArray()[i]);
                GameObject GalleryGroup = Instantiate<GameObject>(GroupTemplate, Content.transform);
            }
            if (individualsList.ToArray().Length == 0) {
                // layout.padding.right += 408;
                layout.padding.right  += 408;
                Content.GetComponent<SwipeMenu>().Sort(); 
            }
        } else {
            GroupTemplate.SetActive(false);
        } 
        
        if (individualsList.ToArray().Length > 0) {
            for (int i = 1; i < individualsList.ToArray().Length; ++i) {
                // Debug.Log("[MMGallery] : " + "Intantiate group: " + groupNumbers.ToArray()[i]);
                Instantiate<GameObject>(IndividualTemplate, Content.transform);
            }
            if (groups.ToArray().Length == 0) {
                Content.GetComponent<SwipeMenu>().special = true;
                // layout.padding.right += 408;
                layout.padding.left  += 408;
            } else Content.GetComponent<SwipeMenu>().Sort(); 

        } else {
            IndividualTemplate.SetActive(false);
        } 

        
        for (int i = 0; i < groups.ToArray().Length; ++i) {
            // Content.transform.GetChild(i).name = ("Grupo " + groupNumbers.ToArray()[i]);
            Content.transform.GetChild(i).GetComponent<MMGalleryItem>().LoadContent(groups.ToArray()[i], -1, true);
        }

        for (int i = 0; i < individualsList.ToArray().Length; ++i) {
            // Debug.Log("[MMGallery] : " + "Intantiate group: " + groupNumbers.ToArray()[i]);
            // Instantiate<GameObject>(IndividualTemplate, Content.transform);
            if (groups.ToArray().Length > 0)
                Content.transform.GetChild(i + groups.ToArray().Length).GetComponent<MMObraItem>().LoadContent(individualsList.ToArray()[i], true);
            else
                Content.transform.GetChild(i + 1).GetComponent<MMObraItem>().LoadContent(individualsList.ToArray()[i], true);
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

    public void UnloadSpecial() {
        GroupTemplate.SetActive(true);
        IndividualTemplate.SetActive(true);
        Content.GetComponent<SwipeMenu>().UnloadSpecial();
    }

    public void ResetScroll() {
        Content.GetComponent<SwipeMenu>().ResetScroll();
    }
}
