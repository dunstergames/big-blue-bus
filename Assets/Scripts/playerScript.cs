using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerScript : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 10f;
    private Vector2 movement = Vector2.zero;
    public GameObject bullet;
    private int startingHealth;
    private int currentHealth;
    public Sprite[] Sprites;
    public uint MaxNumberOfBullets;

    private GameObject[] gameOverObjects;

    public void LoseLife()
    {
        this.currentHealth--;

        if (currentHealth == 0)
        {
            GameOver();
            return;
        }

        int spriteIndex = startingHealth - currentHealth;
        SpriteRenderer myRenderer = gameObject.GetComponent<SpriteRenderer>();
        myRenderer.sprite = Sprites[spriteIndex];
    }

    private void GameOver()
    {
        foreach (GameObject gameObject in gameOverObjects)
        {
            gameObject.SetActive(true);
        }

        Time.timeScale = 0;
    }

    private void HideGameOverObjects()
    {
        foreach (GameObject gameObject in gameOverObjects)
        {
            gameObject.SetActive(false);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameOverObjects = GameObject.FindGameObjectsWithTag("ShowOnGameOver");
        startingHealth = Sprites.Length;
        HideGameOverObjects();
        currentHealth = startingHealth;
    }

    private void fireBullet()
    {
        if (GameObject.FindGameObjectsWithTag("Bullet").Length < MaxNumberOfBullets)
        {
            Instantiate(bullet, transform.position, Quaternion.identity);
        }
    }

    private void KeyboardUpdate()
    {
        if (Input.GetKeyDown("space"))
        {
            fireBullet();
        }

        movement.x = Input.GetAxisRaw("Horizontal");

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

    private void TouchUpdate()
    {
        foreach (Touch touch in Input.touches)
        {
            double cutScreenY = Screen.height * 0.25;

            if (touch.position.y > cutScreenY)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    fireBullet();
                }
            }
            else
            {
                Vector2 screenTouchPoint = Camera.main.ScreenToWorldPoint(new Vector2(touch.position.x, 0));
                Vector2 newPosition = new Vector2(screenTouchPoint.x, rb.position.y);
                rb.MovePosition(newPosition);
            }
        }
    }

    void Update()
    {
        KeyboardUpdate();
        TouchUpdate();
    }
}
