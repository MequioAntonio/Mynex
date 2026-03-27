using UnityEngine;
using UnityEngine.Audio;
using static UnityEngine.GraphicsBuffer;

public class Vyre_Attack : MonoBehaviour {

    private const string ATTACK = "Attack";

    [SerializeField] private Vyre vyre;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource audioSource;

    public void StartAttack() {
        if (!vyre.IsPlayingAttackAnim()) {
            vyre.setCanWalkAnimation(false);
            animator.SetTrigger(ATTACK);

            int rand = Random.Range(0, 2);
            if (rand == 0) {
                audioSource.PlayOneShot(Resources.Load<AudioClip>("Sounds/Attacks/Vyre/vyre_attack"));
            }
            else {
                audioSource.PlayOneShot(Resources.Load<AudioClip>("Sounds/Attacks/Vyre/vyre_attack_2"));
            }
        }
    }

    public void HitRegistration() {
        if (!vyre.IsPlayingAttackAnim())
            Player.Instance.TakeDamage(vyre.attackDamage);
    }

    public void EndAttack() {
        if (!vyre.IsPlayingAttackAnim())
            vyre.setCanWalkAnimation(true);
    }
}
