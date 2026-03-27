using UnityEngine;

public class Grammophone : MonoBehaviour
{
    public string elemento;
    public BoxCollider2D triggerCollider;

    void OnTriggerEnter2D(Collider2D other) {

        if (other.CompareTag("Player")) {
            PersistentUI.Instance.TurnOffButton.SetActive(true);
            Player.Instance.grammofonoSpegnibile = this;
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            PersistentUI.Instance.TurnOffButton.SetActive(false);
        }
    }
}
