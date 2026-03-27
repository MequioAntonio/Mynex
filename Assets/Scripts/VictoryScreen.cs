using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryScreen : MonoBehaviour {
    [SerializeField] private Button menuButton;

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
    }

    public void Show(bool show) {
        if (show) {
            gameObject.SetActive(true);
        }
        else {
            gameObject.SetActive(false);
        }
    }
}
