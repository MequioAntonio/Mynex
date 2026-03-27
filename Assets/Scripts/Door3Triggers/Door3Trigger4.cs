using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Door3Trigger4 : MonoBehaviour
{
    [SerializeField] private GameObject vyrePrefab;

    private bool alreadyTriggered = false;

    void OnTriggerEnter2D(Collider2D other) {

        if (alreadyTriggered) return;

        if (other.CompareTag("Player"))
        {
            alreadyTriggered = true;
            Instantiate(vyrePrefab, new Vector2(28.78f, 18.14f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(28.78f, 20.1f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(32.05f, 21.65f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(-53.08f, 24.85f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(-49.98f, 24.85f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(-45.68f, 24.85f), Quaternion.identity);
        }
    }
}
