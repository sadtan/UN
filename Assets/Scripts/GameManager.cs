using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Obra selected;
    private Obras obrasJson;
    void Start() {
        string filePath = Application.streamingAssetsPath + "/Text/";
        string []paths = System.IO.Directory.GetFiles(filePath, "*.json");
        Debug.Log(paths[0]);
        string json = System.IO.File.ReadAllText(paths[0]);
        
        obrasJson = JsonUtility.FromJson<Obras>(json);
    }
    void Update()
    {
        if ( Input.GetMouseButtonDown(0)) {
            
            var ray = Camera.main.ScreenPointToRay( Input.mousePosition );
            RaycastHit hitInfo;
            if ( Physics.Raycast(ray, out hitInfo) && GameEvents.current.GetGlobalState() == "CamInteraction" ) { 

                if (hitInfo.collider.gameObject.name.Contains("3D") ) 
                    Interactable3DOpen(hitInfo);

                else if (hitInfo.collider.gameObject.name.Contains("Gallery"))  
                    GalleryOpenInteraction(hitInfo);

                else if (hitInfo.collider.gameObject.name.Contains("ID")) 
                    ModalOpenInteraction(hitInfo);
                    // ModalOpenDada(hitInfo);
                    
                else if ( hitInfo.collider.gameObject.name.Contains ("Transition")) 
                    RoomTransition(hitInfo);

            }
        }

    }

    public void GalleryOpenInteraction(RaycastHit hitInfo) {
        hitInfo.collider.gameObject.SetActive(false);
        GameEvents.current.currentReticle = hitInfo.collider.gameObject;

        CameraMovement.current.zoom = true;

        if (hitInfo.collider.gameObject.GetComponent<FocusPoint>() != null)
            CameraMovement.current.SetTarget(hitInfo.collider.gameObject.GetComponent<FocusPoint>().focus);
        else 
            CameraMovement.current.SetTarget(hitInfo.collider.gameObject.transform);

        GameEvents.current.SetGlobalState("Focusing");
        StartCoroutine(GalleryOpen(hitInfo.collider.gameObject.GetComponent<FocusPoint>().galleryNames, hitInfo.collider.gameObject.GetComponent<FocusPoint>().individuals));
        
    }

    public IEnumerator GalleryOpen(string[] galleryNames, string[] individuals) {
        while(GameEvents.current.GetGlobalState() != "Done") {
            yield return null;
        }

        TooltipSystem.current.ShowSpecialGallery(galleryNames, individuals);
        // Debug.LogAssertion("[Game Manager] open modal");
        GameEvents.current.SetGlobalState("ModalInteraction");
    }

    public void ModalOpenInteraction(RaycastHit hitInfo) {
        hitInfo.collider.gameObject.SetActive(false);
        GameEvents.current.currentReticle = hitInfo.collider.gameObject;

        if (hitInfo.collider.gameObject.name == "ID_Reg. 3190")
            CameraMovement.current.zoom = false;
        else CameraMovement.current.zoom = true;
        
        if (hitInfo.collider.gameObject.GetComponent<FocusPoint>() != null)
            CameraMovement.current.SetTarget(hitInfo.collider.gameObject.GetComponent<FocusPoint>().focus);
        else 
            CameraMovement.current.SetTarget(hitInfo.collider.gameObject.transform);

        GameEvents.current.SetGlobalState("Focusing");
        // CameraMovement.current.cRunning = true;

        OpenModal(hitInfo.collider.gameObject.GetComponent<FocusPoint>().name);
    }

    public void OpenModal(string name) {
        StartCoroutine(ModalOpen(name));
    }

    public IEnumerator ModalOpen(string name) {
        while(GameEvents.current.GetGlobalState() != "Done") {
            yield return null;
        }
        
        // GameEvents.current.ModalOpen(name);
        foreach (Obra obra in obrasJson.obras)
        {
            if (obra.id == name) {
                TooltipSystem.current.OpenDataSheet(obra, true); 
            }
        }
        GameEvents.current.SetGlobalState("ModalInteraction");
    }

    public void RoomTransition(RaycastHit hitInfo) {
        Transform targetSphere = hitInfo.collider.gameObject.GetComponent<SphereTarget>().target;
        int targetIndex = hitInfo.collider.gameObject.GetComponent<SphereTarget>().targetIndex;
        GameObject targetInteractables  = hitInfo.collider.gameObject.GetComponent<SphereTarget>().targetInteractables;
        GameObject currentInteractables = hitInfo.collider.gameObject.GetComponent<SphereTarget>().currentInteractables;

        ChangeSphere(targetSphere, currentInteractables, targetInteractables, targetIndex);
    }

    public IEnumerator Open3DInteractable(Interactable3D interactable3D) {
        
        while(GameEvents.current.GetGlobalState() != "Done") {
            yield return null;
        }

        
        interactable3D.PositionInCamera();
        StartCoroutine(interactable3D.Scale(30f, 2f));
        yield return new WaitForSeconds(2f);

        GameEvents.current.SetGlobalState("ObjInteraction");
        Cursor.visible = true;

    }

    public void ChangeSphere(Transform target, GameObject currentInteractables, GameObject targetInteractables, int targetIndex) {
        StartCoroutine(SphereChange(target, currentInteractables, targetInteractables, targetIndex));
    }
    
    public IEnumerator SphereChange(Transform target, GameObject currentInte, GameObject targetInte, int targetIndex) {

        GameEvents.current.SetGlobalState("ModalInteraction");

        StartCoroutine( SphereChanger.current.FadeIn(0.7f) );
        yield return new WaitForSeconds(0.7f);
        
        SphereChanger.current.transform.position = target.transform.position;
        
        Input.ResetInputAxes();
        currentInte.SetActive(false);
        targetInte.SetActive(true);
        // targetInte.gameObject.GetComponentsInChildren<FocusPoint>();
        // FocusPoint points[] = targetInte.gameObject.GetComponentsInChildren<FocusPoint>();
        
        foreach (Component focusPoint in targetInte.gameObject.GetComponentsInChildren<FocusPoint>()) {
            // focusPoint.gameObject.GetComponent<ReticlePointer>().LookAtCamera();
            if (focusPoint.GetComponentInChildren<ReticlePointer>() != null)
                focusPoint.GetComponentInChildren<ReticlePointer>().LookAtCamera();
        }

        StaticClass.SalaTargetIndex = targetIndex;

        yield return new WaitForSeconds(0.2f);

        StartCoroutine( SphereChanger.current.FadeOut(0.7f) );
        yield return new WaitForSeconds(0.7f);

        GameEvents.current.SetGlobalState("CamInteraction");
    }

    public void Interactable3DOpen(RaycastHit hitInfo) {
        // hitInfo.collider.gameObject.GetComponentInChildren<ReticlePointer>().gameObject.SetActive(false);
        GameEvents.current.currentReticle = hitInfo.collider.gameObject;

        // Disable reticles 
        
        foreach(Component reticle in SphereChanger.current.interactables[StaticClass.SalaTargetIndex].GetComponentsInChildren<FocusPoint>()) {
            Debug.Log(reticle.gameObject.name);
            // if (reticle.gameObject != hitInfo.collider.gameObject)
            reticle.GetComponentInChildren<ReticlePointer>().gameObject.SetActive(false);
        }

        // Start object interaction
        // Set camera focus target
        CameraMovement.current.zoom = false;
        if (hitInfo.collider.gameObject.GetComponent<FocusPoint>() != null)
            CameraMovement.current.SetTarget(hitInfo.collider.gameObject.GetComponent<FocusPoint>().focus);
        else 
            CameraMovement.current.SetTarget(hitInfo.collider.gameObject.transform);

        GameEvents.current.SetGlobalState("Focusing");

        StartCoroutine(Open3DInteractable(hitInfo.collider.gameObject.GetComponentInChildren<Interactable3D>()));
    }

}

    // GameObject[] currentInteractables;
    // GameObject[] targetInteractables;

    // targetInteractables  = GameObject.FindGameObjectsWithTag(targetTag);
    // currentInteractables = GameObject.FindGameObjectsWithTag(tag);

    // foreach (GameObject currentInteractable in currentInteractables)
    // {
    //     currentInteractable.SetActive(false);
    // }

    // foreach (GameObject targetInteractable in targetInteractables)
    // {
    //     Debug.Log("???");
    //     targetInteractable.SetActive(true);
    // }

    // SphereChanger.current.transform.rotation = Quaternion.identity;
    // SphereChanger.current.GetComponentInChildren<CameraMovement>().transform.rotation = target.transform.rotation;
    
    // CameraMovement.current.mouseX = 0;
        // CameraMovement.current.mouseY = 0;
        // CameraMovement.current.mouseZ = 0;