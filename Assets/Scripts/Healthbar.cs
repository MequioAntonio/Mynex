using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour {

    [SerializeField] private Image fillImage;

    private float currentHealth;
    private float maxHealth;

    /*private void Start() {
        maxHealth = Player.Instance.GetMaxHealth();
        currentHealth = Player.Instance.GetHealth();
    }*/

    public void SetHealth(float health) {

        currentHealth = Mathf.Clamp(health, 0, maxHealth);

        fillImage.fillAmount = currentHealth / maxHealth;
    }

    public void Show(bool var) {
        gameObject.SetActive(var);
    }

    public float GetCurrentHealth() {
        return currentHealth;
    }
    public void SetCurrentHealth(float health) {
        currentHealth = health;
    }
    public float GetMaxHealth() {
        return maxHealth;
    }
    public void SetMaxHealth(float health) {
        maxHealth = health;
    }

}