using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class TriggerAreaRed : MonoBehaviour
{
    [SerializeField] private Vyre vyre;
    [SerializeField] private MissionManagerDoor_1 missionManagerDoor_1;
    [SerializeField] private Transform homePosition;

    private bool inside = false;

    void OnTriggerEnter2D(Collider2D other) {
        if (vyre.IsDead()) return;

        if (vyre != null && other.CompareTag("Player")) {
            if (inside) {
                vyre.idle = true;
                vyre.target = homePosition;
                inside = false;
            }
            else {
                vyre.idle = false;
                vyre.target = Player.Instance.transform;
                inside = true;
            }
        }
    }
}
