using System.Collections;
using UnityEngine;

public class TriggerArea2 : MonoBehaviour
{
    [SerializeField] private Vyre[] vyre;

    private bool alreadyTriggered = false;
    private bool over = false;
    private int vyreMorti;

    void OnTriggerEnter2D(Collider2D other) {

        if (alreadyTriggered) return;

        if (other.CompareTag("Player"))
        {
            Trigger();
        }
    }

    private void Trigger() {

        alreadyTriggered = true;

        for (int i = 0; i < 5; i++) {
            vyre[i].SetPlayingAttackAnim(false);
        }
    }

    public bool IsOver() {
        vyreMorti = 0;
        for (int i = 0; i < 5; i++) {
            if(vyre[i].IsDead()) {
                vyreMorti++;
            }
        }
        if(vyreMorti == 5) {
            return true;
        }
        else {
            return false;
        }
    }

}
