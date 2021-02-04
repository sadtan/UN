using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class MMSwipeObraMenu : MonoBehaviour {
    public static MMSwipeObraMenu current;
    public GameObject ObraTemplate;
    public GameObject Content;
    void Awake() {
        current = this;
    }
    // public List<List<Obra>> GetGroupsBySala(int sala) {
    public void GetGroupsBySala(int sala) {
        
    }

    public void LoadGallery(List<Obra> obras) {

        Unload();
        // intantiate objects
        for (int i = 1; i < obras.ToArray().Length; ++i) {
            // Debug.Log("[MMSwipeObraMenu] : " + "Intantiate obra: " + obras.ToArray()[i].nombre);
            Instantiate<GameObject>(ObraTemplate, Content.transform);
        }

        for (int i = 0; i < obras.ToArray().Length; ++i) {
            Content.transform.GetChild(i).name = (obras.ToArray()[i].nombre);
            if (GameEvents.current != null)
                Content.transform.GetChild(i).GetComponent<MMObraItem>().LoadContent(obras.ToArray()[i], true);
            else 
                Content.transform.GetChild(i).GetComponent<MMObraItem>().LoadContent(obras.ToArray()[i], false);
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

    public void ResetScroll() {
        Content.GetComponent<SwipeMenu>().ResetScroll();
    }
}
