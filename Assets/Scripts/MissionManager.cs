using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MissionManager : MonoBehaviour {

    [SerializeField] private MessaggioManager messaggioManager;
    [SerializeField] private MessaggioManager messaggioManagerDestra;
    [SerializeField] private CharacterEffects orwinCharacterEffects;
    [SerializeField] private TriggerArea1 triggerArea1;
    [SerializeField] private TriggerArea2 triggerArea2;
    [SerializeField] private TriggerArea3 triggerArea3;
    [SerializeField] private GameObject vyrePrefab;
    [SerializeField] private Orwin orwin;
    [SerializeField] private Animator orwinAnimator;
    [SerializeField] private SpriteRenderer orwinSpriteRenderer;
    [SerializeField] private EnemySpawner enemySpawner;

    int[] indiciMessaggi;
    int[] indiciImmagini;

    void Start() {
        GameInput.Instance?.SetPlayer(Player.Instance);
        GameInput.Instance?.SetJoystick(PersistentUI.Instance.joystick);

        StartCoroutine(MissioneCoroutine());
    }

    IEnumerator MissioneCoroutine() {

        Player.Instance.SetCanControl(false);
        yield return new WaitForSeconds(3f);
        Player.Instance.animator.SetBool("isWalkingSouth", true);
        Player.Instance.GetComponentInChildren<CharacterEffects>().StartFadeIn();
        yield return new WaitForSeconds(1f);
        Player.Instance.animator.SetBool("isWalkingSouth", false);
        Player.Instance.MoveTo(new Vector2(1.5f, 4f));
        Player.Instance.animator.SetBool("isWalking", true);
        Player.Instance.animator.SetBool("isWalking", false);
        yield return new WaitForSeconds(3f);

        messaggioManager.messaggiLLM.Add(LLMManager.loaderAnswers[0]);
        indiciImmagini = new int[] { 0 };
        messaggioManager.setIndiciLLM(indiciImmagini);
        messaggioManager.MostraDialogoLLM();
        yield return new WaitUntil(() => messaggioManager.GetProgressoDialogo() == 0);
        Player.Instance.SetCanControl(true);
        PersistentUI.Instance.ShowUI(true);
        PersistentUI.Instance.joystick.ResetJoystick();

        //TriggerArea1
        yield return new WaitUntil(() => triggerArea1.IsOver() == true);

        Instantiate(vyrePrefab, new Vector2(26f, -25f), Quaternion.identity);
        Instantiate(vyrePrefab, new Vector2(29f, -25f), Quaternion.identity);
        Instantiate(vyrePrefab, new Vector2(34f, -26f), Quaternion.identity);
        Instantiate(vyrePrefab, new Vector2(32f, -29f), Quaternion.identity);

        Instantiate(vyrePrefab, new Vector2(70f, -25f), Quaternion.identity);
        Instantiate(vyrePrefab, new Vector2(73f, -24f), Quaternion.identity);
        Instantiate(vyrePrefab, new Vector2(69f, -28f), Quaternion.identity);
        Instantiate(vyrePrefab, new Vector2(75f, -27f), Quaternion.identity);
        Instantiate(vyrePrefab, new Vector2(79f, -26f), Quaternion.identity);
        Instantiate(vyrePrefab, new Vector2(83f, -27f), Quaternion.identity);

        //TriggerArea2
        yield return new WaitUntil(() => triggerArea2.IsOver() == true);

        PersistentUI.Instance.ShowUI(false);
        Player.Instance.SetCanControl(false);
        yield return new WaitForSeconds(3f);
        orwinCharacterEffects.StartFadeIn();
        yield return new WaitForSeconds(1f);
        orwin.MoveTo(new Vector2(65.5f, -23f));
        Player.Instance.MoveTo(new Vector2(63f, -23f));
        yield return new WaitUntil(() => orwin.getArrivato() == true);
        orwin.MoveTo(new Vector2(65f, -23f));
        Player.Instance.MoveTo(new Vector2(64f, -23f));
        yield return new WaitForSeconds(2f);

        messaggioManager.messaggiLLM.Add(LLMManager.loaderAnswers[2]);
        messaggioManager.messaggiLLM.Add(LLMManager.loaderAnswers[3]);
        messaggioManager.messaggiLLM.Add(LLMManager.loaderAnswers[4]);
        indiciImmagini = new int[] { 1, 0, 1};
        messaggioManager.setIndiciLLM(indiciImmagini);
        messaggioManager.MostraDialogoLLM();

        yield return new WaitUntil(() => messaggioManager.GetProgressoDialogo() == 0);
        AudioManager.Instance.PlaySoundAtPosition(Resources.Load<AudioClip>("Sounds/General/Vyre/vyre_1"), transform.position, 0.8f, 1f, 20f);
        yield return new WaitForSeconds(2f);

        messaggioManager.messaggiLLM.Add(LLMManager.loaderAnswers[5]);
        indiciImmagini = new int[] { 0 };
        messaggioManager.setIndiciLLM(indiciImmagini);
        messaggioManager.MostraDialogoLLM();

        yield return new WaitUntil(() => messaggioManager.GetProgressoDialogo() == 0);

        orwin.MoveTo(new Vector2(63.46261f, -20.16258f));
        yield return new WaitForSeconds(2f);
        orwinCharacterEffects.StartFadeOut();
        Player.Instance.SetCanControl(true);
        PersistentUI.Instance.ShowUI(true);
        PersistentUI.Instance.joystick.ResetJoystick();

        for (int i = 0; i <= 4; i++) {
            Instantiate(vyrePrefab, new Vector2(97f, -23f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(97f, -24f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(97f, -25f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(97f, -26f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(97f, -27f), Quaternion.identity);
            Instantiate(vyrePrefab, new Vector2(97f, -28f), Quaternion.identity);
            yield return new WaitForSeconds(1f);
        }

        yield return new WaitUntil(() => EnemyManager.Instance.AreAllEnemiesDead() == true);

        PersistentUI.Instance.ShowUI(false);
        Player.Instance.SetCanControl(false);

        messaggioManager.messaggiLLM.Add(LLMManager.loaderAnswers[6]);
        indiciImmagini = new int[] { 0 };
        messaggioManager.setIndiciLLM(indiciImmagini);
        messaggioManager.MostraDialogoLLM();

        yield return new WaitUntil(() => messaggioManager.GetProgressoDialogo() == 0);

        orwinCharacterEffects.StartFadeIn();
        yield return new WaitForSeconds(1f);
        orwin.MoveTo(new Vector2(63.41898f, -22f));
        yield return new WaitForSeconds(2f);

        messaggioManager.messaggiLLM.Add(LLMManager.loaderAnswers[7]);
        indiciImmagini = new int[] { 1 };
        messaggioManager.setIndiciLLM(indiciImmagini);
        messaggioManager.MostraDialogoLLM();

        yield return new WaitUntil(() => messaggioManager.GetProgressoDialogo() == 0);

        Player.Instance.MoveTo(new Vector2(63.41898f, -24f));
        yield return new WaitUntil(() => Player.Instance.getArrivato() == true);

        messaggioManager.messaggiLLM.Add(LLMManager.loaderAnswers[8]);
        indiciImmagini = new int[] { 1 };
        messaggioManager.setIndiciLLM(indiciImmagini);
        messaggioManager.MostraDialogoLLM();

        yield return new WaitUntil(() => messaggioManager.GetProgressoDialogo() == 0);
        AudioManager.Instance.PlaySoundAtPosition(Resources.Load<AudioClip>("Sounds/General/Vyre/vyre_1"), transform.position, 0.8f, 1f, 20f);
        AudioManager.Instance.PlaySoundAtPosition(Resources.Load<AudioClip>("Sounds/General/Vyre/vyre_2"), transform.position, 0.8f, 1f, 20f);
        AudioManager.Instance.PlaySoundAtPosition(Resources.Load<AudioClip>("Sounds/General/Vyre/vyre_3"), transform.position, 0.8f, 1f, 20f);
        Player.Instance.SetCanControl(true);
        PersistentUI.Instance.ShowUI(true);
        PersistentUI.Instance.joystick.ResetJoystick();

        triggerArea3.AttivaArea();

        enemySpawner.SetAttivo(true);

        orwin.MoveTo(new Vector2(23f, -26f));
        yield return new WaitUntil(() => orwin.getArrivato() == true);
        orwin.MoveTo(new Vector2(15f, -34f));
        yield return new WaitUntil(() => orwin.getArrivato() == true);
        orwin.MoveTo(new Vector2(-2.784063f, -27.58018f));
        yield return new WaitUntil(() => orwin.getArrivato() == true);

        orwinCharacterEffects.StartFadeOut();

    }
}
