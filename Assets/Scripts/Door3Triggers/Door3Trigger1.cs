using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Door3Trigger1 : MonoBehaviour
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
            Instantiate(vyrePrefab, new Vector2(38.01f, 16.04f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(38.65f, 13.41f), Quaternion.identity);
        }
    }
}
