using UnityEngine;
using UnityEngine.UI;

public class PickButton : MonoBehaviour
{
    public Button button;

    void OnEnable() {
        button.onClick.AddListener(OnButtonClicked);
    }

    void OnDisable() {
        button.onClick.RemoveListener(OnButtonClicked);
    }

    void OnButtonClicked() {
        Player.Instance.RaccogliTotem();
    }
}