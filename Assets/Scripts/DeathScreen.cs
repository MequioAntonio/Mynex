using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathScreen : MonoBehaviour {
    [SerializeField] private Button menuButton;
    [SerializeField] private Button checkpointButton;

    private void Awake() {
        menuButton.onClick.AddListener(() => {

            Destroy(Player.Instance.gameObject);
            Destroy(PersistentUI.Instance.gameObject);
            Destroy(GameInput.Instance.gameObject);

            Player.Instance = null;
            PersistentUI.Instance = null;
            GameInput.Instance = null;

            SceneManager.LoadScene(0);
            Show(false);
        });
        checkpointButton.onClick.AddListener(() => {
            caricaCheckpoint();
        });
    }

    private void caricaCheckpoint() {

        Destroy(Player.Instance.gameObject);
        Destroy(PersistentUI.Instance.gameObject);
        Destroy(GameInput.Instance.gameObject);

        Player.Instance = null;
        PersistentUI.Instance = null;
        GameInput.Instance = null;

        if (Loader.targetScene.ToString() == "SampleScene") {
            Loader.Load(Loader.Scene.SampleScene);
            Show(false);
            return;
        }

        if (Loader.difficulty == "Facile") {
            if (Loader.targetScene.ToString() == "Door_1") {
                Loader.Load(Loader.Scene.Door_1);
                Show(false);
            }
            else if (Loader.targetScene.ToString() == "Door_2") {
                Loader.Load(Loader.Scene.Door_2);
                Show(false);
            }
            else if (Loader.targetScene.ToString() == "Door_3") {
                Loader.Load(Loader.Scene.Door_3);
                Show(false);
            }
        }
        else if (Loader.difficulty == "Medio") {
            Loader.Load(Loader.Scene.Cave);
            Show(false);
        }
    }

    public void Show(bool show) {
        if (show) {
            gameObject.SetActive(true);
            if (Loader.difficulty == "Difficile") {
                checkpointButton.gameObject.SetActive(false);
            }
            else {
                checkpointButton.gameObject.SetActive(true);
            }
        }
        else {
            gameObject.SetActive(false);
        }
    }
}
