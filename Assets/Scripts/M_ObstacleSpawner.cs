
using UnityEngine;

public class M_ObstacleSpawner : MonoBehaviour
{
    [System.Serializable]
    public struct spawnableobject
    {
        public GameObject prefab;
        [Range(0f, 1f)]
        public float spawnChanace;
    }

    public spawnableobject[] objects;

    public float minSpawnRate = 1f;
    public float maxSpawnRate = 2f;

    public bool canSpawn = true;

    private void OnEnable()
    {
        Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnRate));
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void Spawn()
    {
        if(!canSpawn) 
        {
            return;
        }
        float spawnChance = Random.value;

        foreach (var obj in objects)
        {
            if (spawnChance < obj.spawnChanace)
            {
                GameObject obstical = Instantiate(obj.prefab);
                obstical.transform.position += transform.position;
                break;
            }

            spawnChance -= obj.spawnChanace;
        }
        Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnRate));
    }
}
