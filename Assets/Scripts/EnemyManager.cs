using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
    public static EnemyManager Instance;

    private List<GameObject> activeEnemies = new List<GameObject>();

    void Awake() {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void RegisterEnemy(GameObject enemy) {
        if (!activeEnemies.Contains(enemy))
            activeEnemies.Add(enemy);
    }

    public void UnregisterEnemy(GameObject enemy) {
        if (activeEnemies.Contains(enemy))
            activeEnemies.Remove(enemy);
    }

    public bool AreAllEnemiesDead() {
        return activeEnemies.Count == 0;
    }

    public int GetAliveEnemyCount() {
        return activeEnemies.Count;
    }
}