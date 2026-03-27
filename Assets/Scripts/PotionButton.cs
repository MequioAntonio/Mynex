using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PotionButton : MonoBehaviour
{
    public TextMeshProUGUI text;

    private Button button;

    private void Awake() {
        button = GetComponentInChildren<Button>();
        text.text = Player.Instance.pozioni.ToString();
    }

    private void Start() {

        button.onClick.RemoveAllListeners();

        button.onClick.AddListener(() => {
            Player.Instance.drinkPotion();
        });
    }

    public void removePotion() {
        Player.Instance.pozioni--;
        text.text = Player.Instance.pozioni.ToString();
    }

    public void addPotion() {
        Player.Instance.pozioni++;
        text.text = Player.Instance.pozioni.ToString();
    }

    public void Show(bool show) {
        gameObject.SetActive(show);
    }
}
