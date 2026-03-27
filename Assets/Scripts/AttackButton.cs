using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AttackButton : MonoBehaviour
{

    private Button button;

    private void Awake() {
        button = GetComponent<Button>();
    }

    private void Start() {
        button.onClick.RemoveAllListeners();

        button.onClick.AddListener(() => {
            GameInput.Instance?.TriggerAttackButton();
        });
    }

    public void Show(bool show) {
        gameObject.SetActive(show);
    }
}
