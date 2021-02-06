using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereChanger : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject m_Fader;
    public static SphereChanger current;
    public Transform target;
    public GameObject[] interactables;
    void Awake()
    {
        current = this;
        // m_Fader = GameObject.Find("Fader");

        if (m_Fader == null)
            Debug.LogWarning("No Fader object found on camera.");
    }

    void Start() {
        // transform.position = targets[StaticClass.SalaTargetIndex].transform.position;
        foreach (GameObject interactable in interactables) {
            // Debug.Log("?");
            interactable.SetActive(false);
        }
        transform.position = interactables[StaticClass.SalaTargetIndex].transform.parent.transform.position;
        interactables[StaticClass.SalaTargetIndex].GetComponentInChildren<Interactables>().gameObject.SetActive(true);
        // transform.position = interactables[0].transform.parent.transform.position;
        // interactables[0].GetComponentInChildren<Interactables>().gameObject.SetActive(true);
        // salas[StaticClass.SalaTargetIndex].GetComponent<Interactables>().gameObject.SetActive(true);
        
    }

    // Update is called once per frame
    public IEnumerator FadeOut(float time) {
        Material mat = m_Fader.GetComponent<Renderer>().material;
        //While we are still visible, remove some of the alpha colour
        while (mat.color.a > 0.0f)
        {
            mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, mat.color.a - (Time.deltaTime / time));
            yield return null;
        }
    }

    public IEnumerator FadeIn(float time) {
        Material mat = m_Fader.GetComponent<Renderer>().material;
        //While we aren't fully visible, add some of the alpha colour
        while (mat.color.a < 1.0f)
        {
            mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, mat.color.a + (Time.deltaTime / time));
            yield return null;
        }
    }
}
