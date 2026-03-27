using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Chest : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other) {

        if (other.CompareTag("Player")) {
            PersistentUI.Instance.potionButton.GetComponentInChildren<PotionButton>().addPotion();
            Player.Instance.audioSource.PlayOneShot(Resources.Load<AudioClip>("Sounds/Open_Chest"));
            Destroy(gameObject);
        }
    }

}
