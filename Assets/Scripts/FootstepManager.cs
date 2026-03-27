using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public enum TerrainType {
    Grass,
    Dirt
}

[System.Serializable]
public class TileTerrainPair {
    public TileBase tile;
    public TerrainType terrainType;
}

public class FootstepManager : MonoBehaviour {

    [Header("Associazioni Tile ↔ Terreno")]
    public List<TileTerrainPair> tileTerrainPairs;

    [Header("Suoni passi")]
    public List<AudioClip> grassSteps;
    public List<AudioClip> dirtSteps;

    private Tilemap groundTilemap;
    private AudioSource audioSource;

    void Awake() {
        if (groundTilemap == null) {
            GameObject tilemapGO = GameObject.Find("Terra");
            if (tilemapGO != null) {
                groundTilemap = tilemapGO.GetComponent<Tilemap>();
            }
        }
    }

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayFootstep(Vector3 worldPosition) {

        Vector3 offsetPosition = worldPosition + Vector3.down * 0.50f;

        Vector3Int tilePosition = groundTilemap.WorldToCell(offsetPosition);
        TileBase tile = groundTilemap.GetTile(tilePosition);
        if (tile == null) return;

        TerrainType? terrainType = GetTerrainTypeForTile(tile);
        if (terrainType == null) return;

        AudioClip clipToPlay = GetRandomClipForTerrain(terrainType.Value);
        if (clipToPlay != null) {
            audioSource.pitch = Random.Range(0.95f, 1.05f);
            audioSource.PlayOneShot(clipToPlay);
        }
    }

    private TerrainType? GetTerrainTypeForTile(TileBase tile) {
        foreach (var pair in tileTerrainPairs) {
            if (pair.tile == tile)
                return pair.terrainType;
        }
        return null;
    }

    private AudioClip GetRandomClipForTerrain(TerrainType terrainType) {
        List<AudioClip> clips = terrainType switch {
            TerrainType.Grass => grassSteps,
            TerrainType.Dirt => dirtSteps,
            _ => null
        };

        if (clips != null && clips.Count > 0) {
            return clips[Random.Range(0, clips.Count)];
        }

        return null;
    }
}