using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Door1Trigger3 : MonoBehaviour
{
    [SerializeField] private GameObject vyrePrefab;

    private bool alreadyTriggered = false;

    void OnTriggerEnter2D(Collider2D other) {

        if (alreadyTriggered) return;

        if (other.CompareTag("Player"))
        {
            alreadyTriggered = true;
            Instantiate(vyrePrefab, new Vector2(-15.65f, 28.13f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(-17.41f, 28.13f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(-19.76f, 28.13f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(-7.96f, 28.13f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(-6.02f, 28.13f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(-7.6f, 30.62f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(26.17f, 7.06f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(26.17f, 9.36f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(28.02f, 11.01f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(28.41f, 6.75f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(-45.06f, 7.19f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(-46.83f, 7.19f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(-46.83f, 5.13f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(-50.9f, 5.13f), Quaternion.identity);
        }
    }
}
