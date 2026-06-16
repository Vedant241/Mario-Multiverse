using UnityEngine;

public class M_DonkeySpawner : MonoBehaviour
{
    public GameObject DonkeyKong;
    public M_ObstacleSpawner enemySpawner;

    private float timer = 0;
    private bool hasSpawned = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 20 && !hasSpawned)
        {
            enemySpawner.canSpawn = false;
            Instantiate(DonkeyKong, transform.position, Quaternion.identity);
            hasSpawned = true; // Set to true so that it won't spawn again.
        }
        else
        {
            enemySpawner.canSpawn = true;
        }
    }
}
