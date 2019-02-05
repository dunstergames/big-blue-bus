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
        var left = transform.position.x - (renderer.bounds.size.x / 2);
        var right = transform.position.x + (renderer.bounds.size.x / 2);
        var spawnPoint = new Vector2(Random.Range(left, right), transform.position.y);

        Instantiate(enemy, spawnPoint, Quaternion.identity);
    }
}
