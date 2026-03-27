using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public static AudioManager Instance;

    private float deathSoundCooldown = 0.1f; // tempo minimo tra suoni
    private float lastDeathSoundTime = -Mathf.Infinity;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void PlaySoundAtPosition(AudioClip clip, Vector3 position, float volume, float spatialBlend, float maxDistance) {

        if (clip.name.Contains("vyre_death")) {
            if (Time.time - lastDeathSoundTime < deathSoundCooldown) return;
            lastDeathSoundTime = Time.time;
        }

        GameObject tempGO = new GameObject("TempAudio");
        tempGO.transform.position = position;

        AudioSource aSource = tempGO.AddComponent<AudioSource>();
        aSource.clip = clip;
        aSource.volume = volume;
        aSource.pitch = Random.Range(0.95f, 1.05f);
        aSource.spatialBlend = spatialBlend;
        aSource.maxDistance = maxDistance;
        aSource.rolloffMode = AudioRolloffMode.Linear;

        aSource.Play();
        Destroy(tempGO, clip.length);
    }
}