using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Door1Trigger1 : MonoBehaviour
{
    [SerializeField] private GameObject vyrePrefab;

    private bool alreadyTriggered = false;

    void OnTriggerEnter2D(Collider2D other) {

        if (alreadyTriggered) return;

        if (other.CompareTag("Player"))
        {
            alreadyTriggered = true;
            Instantiate(vyrePrefab, new Vector2(26.17f, 7.06f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(26.17f, 9.36f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(28.02f, 11.01f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(28.41f, 6.75f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(-12.95f, 23.17f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(-10.13f, 23.17f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(-7.67f, 23.17f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(-18.07f, 23.17f), Quaternion.identity);
        }
    }
}
