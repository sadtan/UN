using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipeMenu : MonoBehaviour {
    public GameObject scrollbar;
    private float scroll_pos = 0;
    float[] pos;

    public bool special = false;
    void Update()
    {
        if (GameEvents.current == null) {
            pos = new float[transform.childCount];
            float distance = 0;
            if (!special)
                distance = 1f / (pos.Length - 1f);
            else distance = 1f / (pos.Length - 2f);

            for (int i = 0; i < pos.Length; i++) pos[i] = distance * i;
                if (Input.GetMouseButton(0)) scroll_pos = scrollbar.GetComponent<Scrollbar>().value;
                else for (int i = 0; i < pos.Length; i++) if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2))
                    scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[i], 5f * Time.deltaTime);
                    

            for (int i = 0; i < pos.Length; i++) if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2)) {
                    transform.GetChild(i).localScale = Vector3.Lerp(new Vector3(transform.GetChild(i).localScale.x, transform.GetChild(i).localScale.y, transform.GetChild(i).localScale.z) , 
                                                                    new Vector3(1.1f, 1.1f, 1.1f), 0.1f);
                    for (int j = 0; j < pos.Length; j++) if (j!=i)
                            transform.GetChild(j).localScale = Vector3.Lerp(transform.GetChild(j).localScale, new Vector3(0.85f, 0.85f, 0.85f), 5f * Time.deltaTime);
            }
            // Debug.Log("[SwipeMenu] : " + scrollbar.GetComponent<Scrollbar>().value);
        } else  if (GameEvents.current.GetGlobalState() == "ModalInteraction") {

            pos = new float[transform.childCount];
            float distance = 0;
            
            distance = 1f / (pos.Length - 1f);
            

            for (int i = 0; i < pos.Length; i++) pos[i] = distance * i;
                if (Input.GetMouseButton(0)) scroll_pos = scrollbar.GetComponent<Scrollbar>().value;
                else for (int i = 0; i < pos.Length; i++) if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2))
                    scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[i], 5f * Time.deltaTime);
                    

            for (int i = 0; i < pos.Length; i++) if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2)) {
                    transform.GetChild(i).localScale = Vector3.Lerp(new Vector3(transform.GetChild(i).localScale.x, transform.GetChild(i).localScale.y, transform.GetChild(i).localScale.z) , 
                                                                    new Vector3(1.1f, 1.1f, 1.1f), 0.1f);
                    for (int j = 0; j < pos.Length; j++) if (j!=i)
                            transform.GetChild(j).localScale = Vector3.Lerp(transform.GetChild(j).localScale, new Vector3(0.85f, 0.85f, 0.85f), 5f * Time.deltaTime);
            }
        } else {
            // scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[0], 5f * Time.deltaTime);
        }
    }
    public void ResetScroll() {
        scrollbar.GetComponent<Scrollbar>().value = 0;
        scroll_pos = 0;
        // Debug.Log("[SwipeMenu] : Scroll reset" + scrollbar.GetComponent<Scrollbar>().value);
    }
    public void UnloadChildren() {
        for (int i = 1; i < transform.childCount; i++) {
            Destroy(transform.GetChild(i).gameObject);
        }
        scrollbar.GetComponent<Scrollbar>().value = 0;
    }

    public void UnloadSpecial() {

        for (int i = 1; i < transform.childCount; i++) {
            if (transform.GetChild(i).gameObject.name.Contains("Clone")) 
                Destroy(transform.GetChild(i).gameObject);
        }
        scrollbar.GetComponent<Scrollbar>().value = 0;
    }

    public void Sort() {
        for (int i = 1; i < transform.childCount; i++) {
            if (transform.GetChild(i).gameObject.name == "ObraTemplate") {
                transform.GetChild(i).SetSiblingIndex(transform.childCount);
            };
        }
    }

}