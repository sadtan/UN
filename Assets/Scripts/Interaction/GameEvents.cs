using System;
using UnityEngine;
// using UnityEngine.Windows;

public class GameEvents : MonoBehaviour
{
    private TextAsset jsonFile;
    private Obras obrasJson;
    public static GameEvents current;
    public GameObject currentReticle;
    public GameObject closeButton;
    public enum State {
        Paused,
        CameraInteraction,
        ObjectInteraction,
        ModalInteraction,
        Focusing,
        Done
    }

    private State currentState;

    private void Start() {
        // if (!System.IO.Directory.Exists(Application.streamingAssetsPath)) {
        //     System.IO.Directory.CreateDirectory(Application.streamingAssetsPath);
        // }

        // string filePath = Application.streamingAssetsPath + "/Text/";
        // string []paths = System.IO.Directory.GetFiles(filePath, "*.json");
        // Debug.Log(paths[0]);
        // jsonFile = Resources.Load<TextAsset>(paths[0]);

        // string json = System.IO.File.ReadAllText(paths[0]);
        // Debug.Log(json);
        currentState = State.CameraInteraction;
    }

    private void Awake() {
        current = this;
        currentState = State.ModalInteraction;

        
    }

    public void ChangeState (State newState) {
        
        Debug.Log("Exiting State");

        currentState = newState;
    }

    public bool GetGlobalState(string testState) {
        
        // Debug.Log(Enum.GetName(typeof(State), 0) == testState);
        foreach (string state in Enum.GetNames(typeof(State))) {
            // Debug.Log(state);
            if (testState == state)
                return true;
        }

        return false;
    }

    public string GetGlobalState() {
        
        switch (currentState) {
            case State.Paused:
                return "Paused";
            case State.CameraInteraction:
                return "CamInteraction";
            case State.ObjectInteraction:
                return "ObjInteraction";
            case State.ModalInteraction:
                return "ModalInteraction";
            case State.Focusing:
                return "Focusing";
            case State.Done:
                return "Done";
        }

        return "Err";
    }

    public void SetGlobalState(string nextState) {
        switch (nextState) {
            case "Paused":
            
                Debug.Log("[State Set] Paused");
                currentState = State.Paused;
                break;
            case "CamInteraction":
                Debug.Log("[State Set] CamInteraction");
                currentState = State.CameraInteraction;
                break;
            case "ObjInteraction":
                Debug.Log("[State Set] ObjInteraction");
                currentState = State.ObjectInteraction;
                break;
            case "ModalInteraction":
                Debug.Log("[State Set] ModalInteraction");
                currentState = State.ModalInteraction;
                break;
            case "Focusing":
                Debug.Log("[State Set] Focusing");
                currentState = State.Focusing;
                break;
            case "Done":
                Debug.Log("[State Set] Done");
                currentState = State.Done;
                break;
        }


    }

    public event Action onCamInteractionEnter;
    
    public void CamInteractionEnter() {
        if (onCamInteractionEnter != null) {
            onCamInteractionEnter();
        }
    }
    public event Action onCamInteractionExit;
    public void CamInteractionExit() {
        if (onCamInteractionExit != null) {
            onCamInteractionExit();
        }
    }

    // 
    public event Action<string> onModalOpen;
    public void ModalOpen(string ID) {
        if (onModalOpen != null) {
            onModalOpen(ID);
        }
    }

    public event Action onModalClose;
    public void ModalClose() {
        if (onModalClose != null) {
            onModalClose();
        }
    }

    public event Action<string> onGalleryOpen;
    public void GalleryOpen(string ID) {
        if (onGalleryOpen != null) {
            onGalleryOpen(ID);
        }
    }
    public event Action onGalleryClose;
    public void GalleryClose() {
        if (onGalleryClose != null) {
            onGalleryClose();
        }
    }
}
