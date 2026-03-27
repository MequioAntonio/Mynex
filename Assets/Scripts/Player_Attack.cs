using UnityEngine;

public class Player_Attack : MonoBehaviour {

    public void StartAttack() {
        Player.Instance.setCanWalkAnimation(false);
        Invoke(nameof(PlayAttackSound), 0.04f);
    }
    public void HitRegistration() {
        Player.Instance.Attack();
    }
    public void EndAttack() {
        Player.Instance.setCanWalkAnimation(true);
    }

    void PlayAttackSound() {
        int rand = Random.Range(0, 2);
        if (rand == 0) {
            Player.Instance.audioSource.PlayOneShot(Resources.Load<AudioClip>("Sounds/Attacks/Nex/nex_attack"));
        }
        else {
            Player.Instance.audioSource.PlayOneShot(Resources.Load<AudioClip>("Sounds/Attacks/Nex/nex_attack_2"));
        }
    }
}
