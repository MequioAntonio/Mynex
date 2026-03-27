using UnityEngine;

public class TotemSlot : MonoBehaviour
{

    public Transform totemPosition;
    public string totemCorretto;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player") && Player.Instance.totemRaccolto != null) {
            PersistentUI.Instance.DropButton.SetActive(true);
            Player.Instance.slotPiazzabile = this;
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            PersistentUI.Instance.DropButton.SetActive(false);
        }
    }
}
