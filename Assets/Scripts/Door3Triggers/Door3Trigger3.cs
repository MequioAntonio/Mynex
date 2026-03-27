using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Door3Trigger3 : MonoBehaviour
{
    [SerializeField] private GameObject vyrePrefab;

    private bool alreadyTriggered = false;

    void OnTriggerEnter2D(Collider2D other) {

        if (alreadyTriggered) return;

        if (other.CompareTag("Player"))
        {
            alreadyTriggered = true;
            Instantiate(vyrePrefab, new Vector2(-13f, 37.74f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(-9.28f, 37.74f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(-4.09f, 37.74f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(0f, 39.39f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(0f, 42.86f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(-2.82f, 42.86f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(31.49f, 40.13f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(33.55f, 40.13f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(-40.52f, 40.13f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(-40.52f, 38.32f), Quaternion.identity);
        }
    }
}
