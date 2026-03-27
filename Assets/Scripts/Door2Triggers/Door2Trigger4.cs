using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Door2Trigger4 : MonoBehaviour
{
    [SerializeField] private GameObject vyrePrefab;

    private bool alreadyTriggered = false;

    void OnTriggerEnter2D(Collider2D other) {

        if (alreadyTriggered) return;

        if (other.CompareTag("Player"))
        {
            alreadyTriggered = true;
            Instantiate(vyrePrefab, new Vector2(-24.64f, 39.17f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(-27f, 39.17f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(-25.8f, 36.83f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(-32.75f, 36.83f), Quaternion.identity);
        }
    }
}
