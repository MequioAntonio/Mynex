using System.Collections;
using UnityEngine;

public class TriggerArea3 : MonoBehaviour
{

    [SerializeField] private MissionManager missionManager;

    private bool alreadyTriggered = true; //DEFAULT è true

    void OnTriggerEnter2D(Collider2D other) {

        if (alreadyTriggered) return;

        if (other.CompareTag("Player"))
        {
            Trigger();
        }
    }

    private void Trigger() {

        alreadyTriggered = true;

        PersistentUI.Instance.ShowUI(false);

        Loader.Load(Loader.Scene.Cave);

    }

    public void AttivaArea() {
        alreadyTriggered = false;
    }

}
