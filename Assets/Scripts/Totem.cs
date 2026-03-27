using UnityEngine;

public class Totem : MonoBehaviour
{
    public string totemType;

    void OnTriggerEnter2D(Collider2D other) {

        if (other.CompareTag("Player")) {
            PersistentUI.Instance.PickButton.SetActive(true);
            Player.Instance.totemRaccoglibile = this;
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            PersistentUI.Instance.PickButton.SetActive(false);
        }
    }
}
