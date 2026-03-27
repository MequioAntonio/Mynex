using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Door2Trigger2 : MonoBehaviour
{
    [SerializeField] private GameObject vyrePrefab;

    private bool alreadyTriggered = false;

    void OnTriggerEnter2D(Collider2D other) {

        if (alreadyTriggered) return;

        if (other.CompareTag("Player"))
        {
            alreadyTriggered = true;
            Instantiate(vyrePrefab, new Vector2(-36.23f, 18.43f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(-34.45f, 18.43f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(-34.45f, 16.13f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(-51.47f, 14.02f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(-51.47f, 11.53f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(-48.79f, 11.12f), Quaternion.identity);
        }
    }
}
