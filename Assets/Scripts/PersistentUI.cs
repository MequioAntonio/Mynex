using UnityEngine;

public class PersistentUI : MonoBehaviour {
    public static PersistentUI Instance { get; set; }


    [Header("UI References")]
    public MessaggioManager messaggioManager;
    public MessaggioManager MessaggioManagerDestra;
    public Healthbar healthbar;
    public Joystick joystick;
    public AttackButton attackButton;
    public DeathScreen deathScreen;
    public VictoryScreen victoryScreen;
    public GameObject potionButton;
    public GameObject PickButton;
    public GameObject DropButton;
    public GameObject TurnOffButton;
    public GameObject Elisir1;
    public GameObject Elisir2;
    public GameObject Elisir3;
    public GameObject totemPortatoCuore;
    public GameObject totemPortatoCervo;
    public GameObject totemPortatoCorona;
    public GameObject totemPortatoPorta;


    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void ShowUI(bool show) {
        healthbar.Show(show);
        joystick.Show(show);
        attackButton.Show(show);
        potionButton.SetActive(show);
    }
}