using UnityEngine;
using System.Collections;

public class FlashOnly : MonoBehaviour {
    private Color flashColor = Color.red;
    private float flashDuration = 0.1f;

    private Material material;
    private Coroutine flashCoroutine;

    void Start() {
        material = GetComponent<SpriteRenderer>().material;
        material.SetColor("_FlashColor", flashColor);
        material.SetFloat("_FlashAmount", 0f);
        material.SetFloat("_Alpha", 1f); // Sempre visibile
    }

    public void FlashRed() {
        if (flashCoroutine != null)
            StopCoroutine(flashCoroutine);

        flashCoroutine = StartCoroutine(FlashCoroutine());
    }

    IEnumerator FlashCoroutine() {
        material.SetFloat("_FlashAmount", 1f);
        yield return new WaitForSeconds(flashDuration);
        material.SetFloat("_FlashAmount", 0f);
    }
}
