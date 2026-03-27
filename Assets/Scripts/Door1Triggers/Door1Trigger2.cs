using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Door1Trigger2 : MonoBehaviour
{
    [SerializeField] private GameObject vyrePrefab;

    private bool alreadyTriggered = false;

    void OnTriggerEnter2D(Collider2D other) {

        if (alreadyTriggered) return;

        if (other.CompareTag("Player"))
        {
            alreadyTriggered = true;
            Instantiate(vyrePrefab, new Vector2(-45.06f, 7.19f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(-46.83f, 7.19f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(-46.83f, 5.13f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(-50.9f, 5.13f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(-51.72f, 3.6f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(-12.95f, 23.17f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(-10.13f, 23.17f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(-7.67f, 23.17f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(-18.07f, 23.17f), Quaternion.identity);
        }
    }
}
