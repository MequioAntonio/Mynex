using System.Collections;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {
    [SerializeField] private AudioClip introClip;
    [SerializeField] private AudioClip loopClip;
    [SerializeField] private AudioSource audioSource;

    private float fadeInDuration = 2f;
    private float fadeOutDuration = 2f;

    public IEnumerator PlayIntroThenLoop() {

        if (audioSource.isPlaying) {
            yield return StartCoroutine(FadeOut(audioSource, fadeOutDuration));
        }

        audioSource.clip = introClip;
        audioSource.loop = false;
        audioSource.volume = 0f;
        audioSource.Play();

        yield return StartCoroutine(FadeIn(audioSource, fadeInDuration));

        yield return new WaitForSeconds(introClip.length - fadeInDuration);

        audioSource.clip = loopClip;
        audioSource.loop = true;
        audioSource.volume = 0.5f;
        audioSource.Play();
    }

    private IEnumerator FadeIn(AudioSource source, float duration) {
        float targetVolume = 0.5f;
        for (float t = 0f; t < duration; t += Time.deltaTime) {
            source.volume = Mathf.Lerp(0f, targetVolume, t / duration);
            yield return null;
        }
        source.volume = targetVolume;
    }

    private IEnumerator FadeOut(AudioSource source, float duration) {
        float startVolume = source.volume;

        for (float t = 0f; t < duration; t += Time.deltaTime) {
            source.volume = Mathf.Lerp(startVolume, 0f, t / duration);
            yield return null;
        }

        source.volume = 0f;
        source.Stop();
    }
}