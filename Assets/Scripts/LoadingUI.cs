using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingUI : MonoBehaviour {
    public static LoadingUI Instance;

    [SerializeField] private Slider progressBar;
    [SerializeField] private TextMeshProUGUI progressText;

    private void Awake() {
        Instance = this;
    }

    public void SetProgress(float value) {
        if (progressBar != null)
            progressBar.value = value;
        if (progressText != null)
            progressText.text = "Loading... " + Mathf.RoundToInt(value * 100f) + "%";
    }
}