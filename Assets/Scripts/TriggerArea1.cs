using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class TriggerArea1 : MonoBehaviour
{
    [SerializeField] private MessaggioManager messaggioManagerDestra;
    [SerializeField] private Vyre vyre;
    [SerializeField] private MusicPlayer musicPlayer;
    [SerializeField] private MissionManager missionManager;

    private bool alreadyTriggered = false;
    private bool over = false;

    void OnTriggerEnter2D(Collider2D other) {

        if (alreadyTriggered) return;

        if (other.CompareTag("Player"))
        {
            PersistentUI.Instance.ShowUI(false);
            StartCoroutine(musicPlayer.PlayIntroThenLoop());

            StartCoroutine(TriggerCoroutine());
        }
    }

    IEnumerator TriggerCoroutine() {
        alreadyTriggered = true;

        PersistentUI.Instance.ShowUI(false);
        Player.Instance.SetCanControl(false);
        Player.Instance.SetIsMoving(false);

        yield return new WaitForSeconds(3f);

        messaggioManagerDestra.messaggiLLM.Add(LLMManager.loaderAnswers[1]);
        int[] indiciImmagini = { 0 };
        messaggioManagerDestra.setIndiciLLM(indiciImmagini);
        messaggioManagerDestra.MostraDialogoLLM();

        yield return new WaitUntil(() => messaggioManagerDestra.GetProgressoDialogo() == 0);
        PersistentUI.Instance.ShowUI(true);
        Player.Instance.SetCanControl(true);
        PersistentUI.Instance.joystick.ResetJoystick();

        vyre.SetPlayingAttackAnim(false);

        over = true;
    }

    public bool IsOver() {
        return over;
    }

}
