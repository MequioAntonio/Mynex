using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Door1Trigger5 : MonoBehaviour
{
    [SerializeField] private GameObject vyrePrefab;

    private bool alreadyTriggered = false;

    void OnTriggerEnter2D(Collider2D other) {

        if (alreadyTriggered) return;

        if (other.CompareTag("Player"))
        {
            alreadyTriggered = true;
            Instantiate(vyrePrefab, new Vector2(-23.25f, 27.58f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(-23.25f, 29.59f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(-25.92f, 29.59f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(-21.87f, 31.76f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(-23.02f, 40.84f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(-23.02f, 42.97f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(-26.88f, 42.97f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(17.86f, 38.19f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(19.13f, 38.19f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(20.7f, 38.19f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(20.7f, 42.21f), Quaternion.identity);
        }
    }
}
