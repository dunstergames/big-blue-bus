using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private float speed;
    private Vector2 movement = Vector2.right;

    public GameObject poop;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        speed = Random.Range(3, 6);
        float firstPoopInterval = Random.Range(0.5f, 1.5f);
        float poopInterval = Random.Range(1, 4);

        InvokeRepeating("firePoop", firstPoopInterval, poopInterval);
    }

    private void firePoop()
    {
        var renderer = GetComponent<Renderer>();
        var poopPoint = new Vector2(transform.position.x, transform.position.y);
        Instantiate(poop, poopPoint, Quaternion.identity);
    }

    private void Update()
    {
        rb.MovePosition(rb.position + (movement * speed * Time.deltaTime));
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name == "bullet(Clone)")
        {
            scoreScript.Score += 50;
            Destroy(gameObject);
            Destroy(collider.gameObject);
        }
    }
}
