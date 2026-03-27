using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Door1Trigger4 : MonoBehaviour
{
    [SerializeField] private GameObject vyrePrefab;

    private bool alreadyTriggered = false;

    void OnTriggerEnter2D(Collider2D other) {

        if (alreadyTriggered) return;

        if (other.CompareTag("Player"))
        {
            alreadyTriggered = true;
            Instantiate(vyrePrefab, new Vector2(17.86f, 38.19f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(19.13f, 38.19f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(20.7f, 38.19f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(20.7f, 42.21f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(18.37f, 42.21f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(19.55f, 48.65f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(21.24f, 49.68f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(27.04f, 25.97f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(29.55f, 24.64f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(31.57f, 23.17f), Quaternion.identity);
        }
    }
}
