using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 1f;
    private Vector2 movement = Vector2.zero;
    public GameObject bullet;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void fireBullet()
    {
        Instantiate(bullet, transform.position, Quaternion.identity);
    }

    private void KeyboardUpdate()
    {
        movement.x = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown("space"))
        {
            fireBullet();
        }
    }

    private void TouchUpdate()
    {
        foreach (Touch touch in Input.touches)
        {
            double halfScreenX = Screen.width / 2.0;
            double cutScreenY = Screen.height * 0.25;

            if (touch.position.y > cutScreenY)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    fireBullet();
                }
            }
            else if (touch.position.x < halfScreenX)
            {
                if (touch.phase == TouchPhase.Stationary)
                {
                    movement.x = -1;
                }
            }
            else if (touch.position.x > halfScreenX)
            {
                if (touch.phase == TouchPhase.Stationary)
                {
                    movement.x = 1;
                }
            }
        }
    }

    void Update()
    {
        KeyboardUpdate();
        TouchUpdate();
        MovePlayer();
    }

    private void MovePlayer()
    {
        Vector2 newPosition = rb.position + (movement * speed * Time.deltaTime);
        Vector2 screenEdge = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        if (newPosition.x < -screenEdge.x)
        {
            newPosition.x = -screenEdge.x;
        }
        else if (newPosition.x > screenEdge.x)
        {
            newPosition.x = screenEdge.x;
        }
        rb.MovePosition(newPosition);
    }
}
