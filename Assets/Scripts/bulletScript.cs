using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 6f;
    private Vector2 movement = Vector2.up;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rb.MovePosition(rb.position + (movement * speed * Time.deltaTime));
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
