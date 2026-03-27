using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class LoopingFadeInAudio : MonoBehaviour {
    private float fadeDuration = 1f;
    private AudioSource audioSource;
    private float targetVolume;
    private float fadeTimer = 0f;
    private float clipTimeLastFrame;

    void Start() {
        Time.timeScale = 1f;
        audioSource = GetComponent<AudioSource>();
        targetVolume = audioSource.volume;
        audioSource.volume = 0f;
        audioSource.Play();
        clipTimeLastFrame = 0f;
    }

    void Update() {

        if (audioSource.isPlaying) {
            float currentTime = audioSource.time;
            if (currentTime < clipTimeLastFrame)
            {
                audioSource.volume = 0f;
                fadeTimer = 0f;
            }
            clipTimeLastFrame = currentTime;

            if (audioSource.volume < targetVolume) {
                fadeTimer += Time.deltaTime;
                audioSource.volume = Mathf.Lerp(0f, targetVolume, fadeTimer / fadeDuration);
            }
        }
    }
}