using UnityEngine;
using System.Collections;

public class CharacterEffects : MonoBehaviour {
    [SerializeField] private SpriteRenderer[] spriteRenderers; // corpo + ombre
    [Header("Fade Settings")]
    [SerializeField] private float fadeDuration = 1f;

    [Header("Damage Flash Settings")]
    [SerializeField] private Color flashColor = Color.red;
    [SerializeField] private float flashDuration = 0.1f;

    private Material[] instanceMaterials;
    private Coroutine fadeCoroutine;
    private Coroutine flashCoroutine;

    void Awake() {
        // Se non hai riempito manualmente l'array in inspector,
        // prende automaticamente tutti i SpriteRenderer nel personaggio
        if (spriteRenderers == null || spriteRenderers.Length == 0) {
            spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        }

        instanceMaterials = new Material[spriteRenderers.Length];
        for (int i = 0; i < spriteRenderers.Length; i++) {
            if (spriteRenderers[i] != null) {
                // istanzia materiale per ognuno
                instanceMaterials[i] = Instantiate(spriteRenderers[i].material);
                spriteRenderers[i].material = instanceMaterials[i];

                // setup iniziale
                instanceMaterials[i].SetFloat("_Alpha", 0f);
                instanceMaterials[i].SetColor("_FlashColor", flashColor);
                instanceMaterials[i].SetFloat("_FlashAmount", 0f);
            }
        }
    }

    void OnEnable() {
        SetInvisible();
    }

    public void StartFadeIn() {
        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeRoutine(GetCurrentAlpha(), 1f));
    }

    public void StartFadeOut() {
        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeRoutine(GetCurrentAlpha(), 0f));
    }

    private float GetCurrentAlpha() {
        if (instanceMaterials != null && instanceMaterials.Length > 0 && instanceMaterials[0] != null) {
            return instanceMaterials[0].GetFloat("_Alpha");
        }
        return 1f; // default se non trovi nulla
    }

    private IEnumerator FadeRoutine(float from, float to) {
        float elapsed = 0f;
        SetAlpha(from);
        while (elapsed < fadeDuration) {
            float alpha = Mathf.Lerp(from, to, elapsed / fadeDuration);
            SetAlpha(alpha);
            elapsed += Time.deltaTime;
            yield return null;
        }
        SetAlpha(to);
        fadeCoroutine = null;
    }

    private void SetAlpha(float alpha) {
        foreach (var mat in instanceMaterials) {
            if (mat != null) {
                mat.SetFloat("_Alpha", alpha);
            }
        }
    }

    public void FlashRed() {
        if (flashCoroutine != null) StopCoroutine(flashCoroutine);
        flashCoroutine = StartCoroutine(FlashCoroutine());
    }

    private IEnumerator FlashCoroutine() {
        foreach (var mat in instanceMaterials) {
            if (mat != null) {
                mat.SetColor("_FlashColor", flashColor);
                mat.SetFloat("_FlashAmount", 1f);
            }
        }
        yield return new WaitForSeconds(flashDuration);
        foreach (var mat in instanceMaterials) {
            if (mat != null) {
                mat.SetFloat("_FlashAmount", 0f);
            }
        }
        flashCoroutine = null;
    }

    public void SetInvisible() {
        foreach (var mat in instanceMaterials) {
            if (mat != null) {
                mat.SetFloat("_Alpha", 0f);
                mat.SetColor("_FlashColor", flashColor);
                mat.SetFloat("_FlashAmount", 0f);
            }
        }
    }
}
