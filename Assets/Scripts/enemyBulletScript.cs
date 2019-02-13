using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBulletScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private float speed;
    private Vector2 movement = Vector2.down;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        speed = Random.Range(3, 6);
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
        if (collider.gameObject.name == "player")
        {
            playerScript player = collider.gameObject.GetComponent<playerScript>();
            player.LoseLife();
            Destroy(gameObject);
        }
    }
}
