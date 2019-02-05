using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private float speed;
    private Vector2 movement = Vector2.down;

    // Start is called before the first frame update
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
        string colliderName = collider.gameObject.name;

        switch (colliderName)
        {
            case ("bullet(Clone)"):
                {
                    scoreScript.Score += 100;
                    Destroy(gameObject);
                    Destroy(collider.gameObject);
                    break;
                }
            case ("player"):
                {
                    scoreScript.Score = 0;
                    Destroy(gameObject);
                    break;
                }
        }

    }
}
