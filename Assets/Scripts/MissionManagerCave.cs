using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionManagerCave : MonoBehaviour {

    [SerializeField] private CharacterEffects orwinCharacterEffects;
    [SerializeField] private GameObject vyrePrefab;
    [SerializeField] private Orwin orwin;
    [SerializeField] private Animator orwinAnimator;
    [SerializeField] private SpriteRenderer orwinSpriteRenderer;
    [SerializeField] private TriggerAreaDoor_1 triggerAreaDoor1;
    [SerializeField] private TriggerAreaDoor_2 triggerAreaDoor2;
    [SerializeField] private TriggerAreaDoor_3 triggerAreaDoor3;

    int[] indiciMessaggi;
    int[] indiciImmagini;

    void Start() {
        GameInput.Instance?.SetPlayer(Player.Instance);
        GameInput.Instance?.SetJoystick(PersistentUI.Instance.joystick);

        orwinCharacterEffects.SetInvisible();
        Player.Instance.GetComponentInChildren<CharacterEffects>().SetInvisible();

        StartCoroutine(MissioneCoroutine());
    }

    IEnumerator MissioneCoroutine() {

        if (Player.Instance.coloriUccisiProgress == 4) {
            Loader.Door1Clear = true;
            Player.Instance.transform.position = new Vector3(-4.92f, 5.79f, 0f);
            Destroy(orwin.gameObject);
            Player.Instance.SetCanControl(false);
            yield return new WaitForSeconds(1f);
            Player.Instance.GetComponentInChildren<CharacterEffects>().StartFadeIn();
            yield return new WaitForSeconds(1f);
            Destroy(triggerAreaDoor1.gameObject);
            Player.Instance.SetCanControl(true);
            PersistentUI.Instance.ShowUI(true);
            PersistentUI.Instance.Elisir1.SetActive(true);
            PersistentUI.Instance.joystick.ResetJoystick();
            Player.Instance.coloriUccisiProgress = 0;
            Player.Instance.elisir1 = true;
        }
        else if (Player.Instance.totemPiazzatiProgress == 4) {
            Loader.Door2Clear = true;
            Player.Instance.transform.position = new Vector3(-16.82f, 5.4f, 0f);
            Destroy(orwin.gameObject);
            Player.Instance.SetCanControl(false);
            yield return new WaitForSeconds(1f);
            Player.Instance.GetComponentInChildren<CharacterEffects>().StartFadeIn();
            yield return new WaitForSeconds(1f);
            Destroy(triggerAreaDoor2.gameObject);
            Player.Instance.SetCanControl(true);
            PersistentUI.Instance.ShowUI(true);
            PersistentUI.Instance.Elisir2.SetActive(true);
            PersistentUI.Instance.joystick.ResetJoystick();
            Player.Instance.totemPiazzatiProgress = 0;
            Player.Instance.elisir2 = true;
        }
        else if (Player.Instance.grammofoniSpentiProgress == 4) {
            Loader.Door3Clear = true;
            Player.Instance.transform.position = new Vector3(-30.87f, 5.47f, 0f);
            Destroy(orwin.gameObject);
            Player.Instance.SetCanControl(false);
            yield return new WaitForSeconds(1f);
            Player.Instance.GetComponentInChildren<CharacterEffects>().StartFadeIn();
            yield return new WaitForSeconds(1f);
            Destroy(triggerAreaDoor3.gameObject);
            Player.Instance.SetCanControl(true);
            PersistentUI.Instance.ShowUI(true);
            PersistentUI.Instance.Elisir3.SetActive(true);
            PersistentUI.Instance.joystick.ResetJoystick();
            Player.Instance.grammofoniSpentiProgress = 0;
            Player.Instance.elisir3 = true;
        }
        else {

            if (Loader.difficulty == "Medio") {
                if (Loader.statoPlayerCave[0] == 0) { //se è la prima volta che arrivi alla cava
                    Loader.statoPlayerCave[0] = 1;
                    Loader.statoPlayerCave[1] = Player.Instance.GetHealth();
                    Loader.statoPlayerCave[2] = Player.Instance.pozioni;
                    Loader.fraseCave = LLMManager.loaderAnswers[0];
                }
                else {
                    Player.Instance.SetHealth(Loader.statoPlayerCave[1]);
                    PersistentUI.Instance.healthbar.SetHealth(Loader.statoPlayerCave[1]);
                    Player.Instance.pozioni = (int)Loader.statoPlayerCave[2];
                    PersistentUI.Instance.potionButton.GetComponentInChildren<PotionButton>().text.text = Loader.statoPlayerCave[2].ToString();

                    LLMManager.loaderAnswers[0] = Loader.fraseCave;
                }
            }

            Player.Instance.SetCanControl(false);

            yield return new WaitForSeconds(3f);

            Player.Instance.animator.SetBool("isWalkingLeft", true);
            Player.Instance.GetComponentInChildren<CharacterEffects>().StartFadeIn();
            yield return new WaitForSeconds(1f);
            Player.Instance.animator.SetBool("isWalkingLeft", false);

            Player.Instance.MoveTo(new Vector2(-0.19f, -0.54f));
            Player.Instance.animator.SetBool("isWalking", true);
            Player.Instance.animator.SetBool("isWalking", false);

            yield return new WaitForSeconds(3f);

            orwinCharacterEffects.StartFadeIn();
            yield return new WaitForSeconds(1f);

            orwin.MoveTo(new Vector2(2.07f, -0.6580652f));
            yield return new WaitUntil(() => orwin.getArrivato());

            PersistentUI.Instance.messaggioManager.messaggiLLM.Add(LLMManager.loaderAnswers[0]);
            indiciImmagini = new int[] { 1 };
            PersistentUI.Instance.messaggioManager.setIndiciLLM(indiciImmagini);
            PersistentUI.Instance.messaggioManager.MostraDialogoLLM();
            yield return new WaitUntil(() => PersistentUI.Instance.messaggioManager.GetProgressoDialogo() == 0);

            orwin.MoveTo(new Vector2(6.661379f, -0.6580652f));
            yield return new WaitUntil(() => orwin.getArrivato());

            orwinCharacterEffects.StartFadeOut();
            yield return new WaitForSeconds(1f);

            Destroy(orwin.gameObject);

            Player.Instance.SetCanControl(true);
            PersistentUI.Instance.ShowUI(true);
            PersistentUI.Instance.joystick.ResetJoystick();

        }

        if (Player.Instance.elisir1 && Player.Instance.elisir2 && Player.Instance.elisir3) {
            Time.timeScale = 0;
            PersistentUI.Instance.ShowUI(false);
            PersistentUI.Instance.victoryScreen.Show(true);
        }
    }
}
