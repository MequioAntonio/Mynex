using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Door2Trigger1 : MonoBehaviour
{
    [SerializeField] private GameObject vyrePrefab;

    private bool alreadyTriggered = false;

    void OnTriggerEnter2D(Collider2D other) {

        if (alreadyTriggered) return;

        if (other.CompareTag("Player"))
        {
            alreadyTriggered = true;
            Instantiate(vyrePrefab, new Vector2(17f, 17f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(19.51f, 17f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(19.51f, 12.67f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(16.27f, 12.67f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(21.68f, 14.84f), Quaternion.identity);
        }
    }
}
