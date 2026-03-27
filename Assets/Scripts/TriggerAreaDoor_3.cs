using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class TriggerAreaDoor_3 : MonoBehaviour
{

    private bool alreadyTriggered = false;
    private bool over = false;

    void OnTriggerEnter2D(Collider2D other) {

        if (alreadyTriggered) return;

        if (other.CompareTag("Player") && !Loader.Door3Clear)
        {
            PersistentUI.Instance.ShowUI(false);
            alreadyTriggered = true;
            over = true;
            Loader.Load(Loader.Scene.Door_3);
        }
    }

    public bool IsOver() {
        return over;
    }

}
