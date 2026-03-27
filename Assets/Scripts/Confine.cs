using UnityEngine;

public class Confine : MonoBehaviour
{
    void Start() {
        {
            int confineLayer = LayerMask.NameToLayer("Confine");
            int playerLayer = LayerMask.NameToLayer("Player");

            // Disattiva tutte le collisioni con Confine, tranne quella col Player
            for (int i = 0; i < 32; i++) {
                if (i != playerLayer) {
                    Physics2D.IgnoreLayerCollision(i, confineLayer, true);
                }
                else {
                    Physics2D.IgnoreLayerCollision(i, confineLayer, false); // Player può collidere
                }
            }
        }
    }
}
