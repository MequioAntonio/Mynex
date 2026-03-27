using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class UscitaTrigger : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other) {

        if (other.CompareTag("Player"))
        {
            if(Player.Instance.coloriUccisiProgress == 4 || Player.Instance.totemPiazzatiProgress == 4 || Player.Instance.grammofoniSpentiProgress == 4) {
                PersistentUI.Instance.ShowUI(false);
                Loader.Load(Loader.Scene.Cave);
            }
        }
    }
}
