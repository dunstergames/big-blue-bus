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
    private GameObject[] pausedMenuObjects;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void LoseLife()
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
        pausedMenuObjects = GameObject.FindGameObjectsWithTag("ShowOnPaused");
        startingHealth = Sprites.Length;
        HideGameOverObjects();
        ResumeGame();
        currentHealth = startingHealth;
    }

    private void fireBullet()
    {
        if (GameObject.FindGameObjectsWithTag("Bullet").Length < MaxNumberOfBullets)
        {
            Instantiate(bullet, transform.position, Quaternion.identity);
            audioSource.Play();
        }
    }

    private void PauseGame()
    {
        foreach (GameObject gameObject in pausedMenuObjects)
        {
            gameObject.SetActive(true);
        }

        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        foreach (GameObject gameObject in pausedMenuObjects)
        {
            gameObject.SetActive(false);
        }

        Time.timeScale = 1;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void KeyboardUpdate()
    {
        if (Time.timeScale == 0)
        {
            return;
        }

        if (Input.GetKeyDown("space"))
        {
            fireBullet();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
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

    private void OnTriggerEnter2D(Collider2D collider)
    {
        string colliderName = collider.gameObject.name;
        if (colliderName == "poop(Clone)")
        {
            LoseLife();
        }
    }
}
