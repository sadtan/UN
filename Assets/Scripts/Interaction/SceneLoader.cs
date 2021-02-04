using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadSala(int index) {
        // StaticClass.SalaTargetIndex = index;
        SceneManager.LoadScene("Salas");
    }

    public void LoadMain() {
        // GameEvents.current.SetGlobalState("CamInteraction");
        SceneManager.LoadScene("Main Menu");
        Cursor.visible = true;
    }
}
