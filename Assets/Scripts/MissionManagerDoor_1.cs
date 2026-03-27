using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MissionManagerDoor_1 : MonoBehaviour {

    [SerializeField] private GameObject vyrePrefab;

    int[] indiciMessaggi;
    int[] indiciImmagini;

    void Start() {
        GameInput.Instance?.SetPlayer(Player.Instance);
        GameInput.Instance?.SetJoystick(PersistentUI.Instance.joystick);

        Player.Instance.GetComponentInChildren<CharacterEffects>().SetInvisible();

        for (int i = 0; i < 4; i++) {
            Debug.Log("Colore " + i + ": " + Loader.colori[i]);
        }

        StartCoroutine(MissioneCoroutine());
    }

    IEnumerator MissioneCoroutine() {

        if (Loader.difficulty == "Facile") {
            if (Loader.statoPlayerDoor1[0] == 0) { //se è la prima volta che arrivi alla door1
                Loader.statoPlayerDoor1[0] = 1;
                Loader.statoPlayerDoor1[1] = Player.Instance.GetHealth();
                Loader.statoPlayerDoor1[2] = Player.Instance.pozioni;
                if (PersistentUI.Instance.Elisir1.activeInHierarchy) {
                    Loader.statoPlayerDoor1[3] = 1;
                }
                else {
                    Loader.statoPlayerDoor1[3] = 0;
                }
                if (PersistentUI.Instance.Elisir2.activeInHierarchy) {
                    Loader.statoPlayerDoor1[4] = 1;
                }
                else {
                    Loader.statoPlayerDoor1[4] = 0;
                }
                if (PersistentUI.Instance.Elisir3.activeInHierarchy) {
                    Loader.statoPlayerDoor1[5] = 1;
                }
                else {
                    Loader.statoPlayerDoor1[5] = 0;
                }

                //Loader.fraseDoor1 = LLMManager.loaderAnswers[0];
            }
            else {
                Player.Instance.SetHealth(Loader.statoPlayerDoor1[1]);
                PersistentUI.Instance.healthbar.SetHealth(Loader.statoPlayerDoor1[1]);
                Player.Instance.pozioni = (int)Loader.statoPlayerDoor1[2];
                PersistentUI.Instance.potionButton.GetComponentInChildren<PotionButton>().text.text = Loader.statoPlayerDoor1[2].ToString();
                if (Loader.statoPlayerDoor1[3] == 1) {
                    PersistentUI.Instance.Elisir1.SetActive(true);
                    Player.Instance.elisir1 = true;
                }
                else {
                    PersistentUI.Instance.Elisir1.SetActive(false);
                    Player.Instance.elisir1 = false;
                }
                if (Loader.statoPlayerDoor1[4] == 1) {
                    PersistentUI.Instance.Elisir2.SetActive(true);
                    Player.Instance.elisir2 = true;
                }
                else {
                    PersistentUI.Instance.Elisir2.SetActive(false);
                    Player.Instance.elisir2 = false;
                }
                if (Loader.statoPlayerDoor1[5] == 1) {
                    PersistentUI.Instance.Elisir3.SetActive(true);
                    Player.Instance.elisir3 = true;
                }
                else {
                    PersistentUI.Instance.Elisir3.SetActive(false);
                    Player.Instance.elisir3 = false;
                }

                //LLMManager.loaderAnswers[0] = Loader.fraseDoor1;
            }
        }

        Player.Instance.SetCanControl(false);
        Player.Instance.GetComponentInChildren<CharacterEffects>().StartFadeIn();
        yield return new WaitForSeconds(1f);

        PersistentUI.Instance.messaggioManager.messaggiLLM.Add(LLMManager.loaderAnswers[0]);
        indiciImmagini = new int[] { 2 };
        PersistentUI.Instance.messaggioManager.setIndiciLLM(indiciImmagini);
        PersistentUI.Instance.messaggioManager.MostraDialogoLLM();
        yield return new WaitUntil(() => PersistentUI.Instance.messaggioManager.GetProgressoDialogo() == 0);

        Player.Instance.SetCanControl(true);
        PersistentUI.Instance.ShowUI(true);
        PersistentUI.Instance.joystick.ResetJoystick();
    }
}
