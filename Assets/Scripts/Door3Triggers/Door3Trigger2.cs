using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Door3Trigger2 : MonoBehaviour
{
    [SerializeField] private GameObject vyrePrefab;

    private bool alreadyTriggered = false;

    void OnTriggerEnter2D(Collider2D other) {

        if (alreadyTriggered) return;

        if (other.CompareTag("Player"))
        {
            alreadyTriggered = true;
            Instantiate(vyrePrefab, new Vector2(-53.08f, 24.85f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(-49.98f, 24.85f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(-45.68f, 24.85f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(-41.79f, 24.85f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(-39.13f, 33.89f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(-39.13f, 37.74f), Quaternion.identity);
        }
    }
}
