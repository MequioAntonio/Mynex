using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Door2Trigger3 : MonoBehaviour
{
    [SerializeField] private GameObject vyrePrefab;

    private bool alreadyTriggered = false;

    void OnTriggerEnter2D(Collider2D other) {

        if (alreadyTriggered) return;

        if (other.CompareTag("Player"))
        {
            alreadyTriggered = true;
            Instantiate(vyrePrefab, new Vector2(-5.57f, 29.45f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(-3.31f, 29.45f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(-3.31f, 31.18f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(0.37f, 30.69f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(4.71f, 30.69f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(4.71f, 27.22f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(-14.04f, 26.73f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(-16.22f, 26.73f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(-18.58f, 24.77f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(-22.63f, 24.77f), Quaternion.identity);
        }
    }
}
