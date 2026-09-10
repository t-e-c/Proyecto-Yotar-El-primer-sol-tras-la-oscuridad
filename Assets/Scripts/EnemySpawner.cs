using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private float spawnTime = 1f;
    [SerializeField] private WaveManager waveManager;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 1f, spawnTime);
    }

    private void SpawnEnemy()
    {
        if (!waveManager.oleadaActiva)
            return;

        if (waveManager.enemigosGenerados >= waveManager.enemigosPorOleada)
            return;

        Instantiate(enemyPrefab, transform.position, Quaternion.identity);

        waveManager.EnemigoGenerado();
    }
}