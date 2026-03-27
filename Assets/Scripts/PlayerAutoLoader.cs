using UnityEngine;

public class PlayerAutoLoader : MonoBehaviour {
    [SerializeField] private GameObject playerPrefab;

    private void Awake() {

        if (Player.Instance == null) {
            GameObject player = Instantiate(playerPrefab);
            player.name = "Player";
        }
    }
}