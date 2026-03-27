using UnityEngine;
using UnityEngine.UI;

public class DifficultySelector : MonoBehaviour {

    public Button facileButton;
    public Button medioButton;
    public Button difficileButton;

    private Button selectedButton;

    void Start() {

        facileButton.onClick.AddListener(() => SelectDifficulty(facileButton, "Facile"));
        medioButton.onClick.AddListener(() => SelectDifficulty(medioButton, "Medio"));
        difficileButton.onClick.AddListener(() => SelectDifficulty(difficileButton, "Difficile"));

        SelectDifficulty(medioButton, "Medio");
    }

    void SelectDifficulty(Button button, string difficulty) {

        if (selectedButton != null)
            SetButtonColor(selectedButton, false);

        selectedButton = button;
        SetButtonColor(selectedButton, true);

        Loader.difficulty = difficulty;
    }

    void SetButtonColor(Button button, bool selected) {
        Image buttonImage = button.GetComponent<Image>();

        if (buttonImage != null) {
            if (selected) {
                buttonImage.color = new Color(205f / 255f, 133f / 255f, 63f / 255f);
            }
            else {
                buttonImage.color = Color.gray;
            }
        }
    }
}