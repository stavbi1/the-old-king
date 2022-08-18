using UnityEngine;

public class MobSpawn : MonoBehaviour
{
    public GameObject mobToSpawn;
    public int maxMobCount;

    private const float SPAWN_DELAY = 5f;
    private Vector3 spawnPosition;
    private float maxSpawnPositionX;

    public void OnMobKilled()
    {
        Invoke("SpawnMob", SPAWN_DELAY);
    }

    private void Start()
    {
        InitSpawnPosition();

        SpawnMobs(maxMobCount);
    }

    private void InitSpawnPosition()
    {
        Collider2D areaCollider = GetComponent<Collider2D>();

        spawnPosition = new Vector3(
            areaCollider.bounds.min.x,
            areaCollider.bounds.min.y + mobToSpawn.transform.localScale.y / 2,
            0f
        );
        maxSpawnPositionX = areaCollider.bounds.size.x;
    }

    private void SpawnMobs(int mobsCount)
    {
        for (int i = 0; i < mobsCount; i++)
        {
            SpawnMob();
        }
    }

    private void SpawnMob()
    {
        Vector3 spawnNoise = new Vector3(Random.Range(0, maxSpawnPositionX), 0, 0);
        Instantiate(mobToSpawn, spawnPosition + spawnNoise, Quaternion.identity, transform);
    }
}
