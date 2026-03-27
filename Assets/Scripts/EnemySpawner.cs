using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    [SerializeField] private GameObject enemyPrefab;

    private float spawnInterval = 1f;
    private bool attivo = false;
    private int maxEnemies = 100;

    void Start() {
        StartCoroutine(SpawnEnemyRoutine());
    }

    IEnumerator SpawnEnemyRoutine() {

        while (true) {
            if (attivo) {
                if (EnemyManager.Instance.GetAliveEnemyCount() < maxEnemies) {
                    Instantiate(enemyPrefab, new Vector2(97f, -23f), Quaternion.identity);
                    Instantiate(enemyPrefab, new Vector2(97f, -24f), Quaternion.identity);
                    Instantiate(enemyPrefab, new Vector2(97f, -25f), Quaternion.identity);
                    Instantiate(enemyPrefab, new Vector2(97f, -26f), Quaternion.identity);
                    Instantiate(enemyPrefab, new Vector2(97f, -27f), Quaternion.identity);
                    Instantiate(enemyPrefab, new Vector2(97f, -28f), Quaternion.identity);
                }
                yield return new WaitForSeconds(spawnInterval);
            }
            else {
                yield return null;
            }
        }
    }

    public void SetAttivo(bool attivo) {
        this.attivo = attivo;
    }
}