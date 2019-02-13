using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnScript : MonoBehaviour
{
    public GameObject enemy;
    public float spawnTime = 2;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("spawnEnemy", 0, spawnTime);
    }

    private void spawnEnemy()
    {
        var renderer = GetComponent<Renderer>();
        var top = transform.position.y - (renderer.bounds.size.y / 2);
        var bottom = transform.position.y + (renderer.bounds.size.y / 2);
        var spawnPoint = new Vector2(transform.position.x, Random.Range(top, bottom));

        Instantiate(enemy, spawnPoint, Quaternion.identity);
    }
}
