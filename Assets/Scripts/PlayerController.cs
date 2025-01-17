using System;
using System.Collections;
using System.Collections.Generic;
//using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Controls the player character.
/// </summary>
public class PlayerController : MonoBehaviour
{
    // Public variables
    public GameOverController gameOverController;
    public Animator animator;
    public BoxCollider2D playerCollider;
    public Vector2 standingColliderSize = new Vector2(0.6987874f, 2.111073f);
    public Vector2 crouchingColliderSize = new Vector2(0.9461729f, 1.321176f);
    public Vector2 standingColliderOffset = new Vector2(0.006101638f, 0.9829593f);
    public Vector2 crouchingColliderOffset = new Vector2(-0.1175911f, 0.5880105f);
    public float speed;
    public ScoreController scoreController;
    public Image heartImage;
    public float maxHealthFillAmount = 1f;
    public float healthFillAmount;
    public float damageAmount = 0.1f;
    public Animator playerAnimator;
    public GameObject deathUIPanel;

    [Header("Speed")]
    [SerializeField] private float jump;
    [SerializeField] private int health;
    [SerializeField] private Image[] hearts;

    // Private variables
    private Rigidbody2D rb2d;
    private bool isDead = false;

    /// <summary>
    /// Initializes the player's health and other settings at the start of the game.
    /// </summary>
    private void Start()
    {
        health = hearts.Length;
        FindObjectOfType<SoundManager>().Play("BGmusic");
    }

    /// <summary>
    /// Decreases the player's health and updates the health UI.
    /// If health is depleted, triggers the player's death.
    /// </summary>
    public void DecreaseHealth()
    {
        health--;
        ReduceHealthUI();
        if (health <= 0)
        {
            Debug.Log("Health decreased, player is dead.");
            Die();
        }
    }

    /// <summary>
    /// Updates the health UI to reflect the player's current health.
    /// </summary>
    private void ReduceHealthUI()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].color = (i < health) ? Color.red : Color.black;
        }
    }

    /// <summary>
    /// Triggers the player's death sequence.
    /// </summary>
    private void Die()
    {
        playerAnimator.SetTrigger("Die");
        StartCoroutine(WaitTimer());
        //gameOverController.PlayerDied();
    }

    /// <summary>
    /// Waits for a specified time, then activates the death UI panel.
    /// </summary>
    private IEnumerator WaitTimer()
    {
        yield return new WaitForSeconds(1.8f);
        deathUIPanel.SetActive(true);
        FindObjectOfType<SoundManager>().Play("PlayerGameOver");
        //SceneManager.LoadScene(1);
        this.enabled = false;
    }

    /// <summary>
    /// Initializes the Rigidbody2D component and logs a message when the player is awakened.
    /// </summary>
    private void Awake()
    {
        Debug.Log("Player controller awake");
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        
        Debug.Log("bg music played");
    }

    /// <summary>
    /// Updates the player's movement and actions every frame.
    /// </summary>
    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxis("Jump");
        PlayerMovementAnimation(horizontal, vertical);
        MoveCharacter(horizontal, vertical);
        if (isDead)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            CrouchMovement(true);
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            CrouchMovement(false);
        }
    }

    /// <summary>
    /// Moves the player character based on horizontal and vertical input.
    /// </summary>
    private void MoveCharacter(float horizontal, float vertical)
    {
        /* Vector3 position = transform.position;
        position.x += horizontal * speed * Time.deltaTime;
        transform.position = position; */
        Vector2 velocity = rb2d.velocity;
        velocity.x = horizontal*speed;
        rb2d.velocity = velocity;

        if (vertical > 0)
        {
            rb2d.AddForce(new Vector2(0f, jump), ForceMode2D.Impulse);
            FindObjectOfType<SoundManager>().Play("PlayerJump");
            
        }
    }

    /// <summary>
    /// Handles the player's crouch movement.
    /// </summary>
    private void CrouchMovement(bool crouch)
    {
        animator.SetBool("Crouch", crouch);

        if (crouch)
        {
            playerCollider.size = crouchingColliderSize;
            playerCollider.offset = crouchingColliderOffset;
        }
        else
        {
            playerCollider.size = standingColliderSize;
            playerCollider.offset = standingColliderOffset;
        }
    }

    /// <summary>
    /// Updates the player's movement animation based on input.
    /// </summary>
    public void PlayerMovementAnimation(float horizontal, float vertical)
    {
        animator.SetFloat("Speed", Mathf.Abs(horizontal));
        Vector3 scale = transform.localScale;
        if (horizontal < 0)
        {
            scale.x = -1f * Mathf.Abs(scale.x);
        }
        else if (horizontal > 0)
        {
            scale.x = Mathf.Abs(scale.x);
        }
        transform.localScale = scale;

        animator.SetBool("Jump", vertical > 0);
    }

    /// <summary>
    /// Increases the player's score when a key is picked up.
    /// </summary>
    public void PickUpkey()
    {
        Debug.Log("Player picked up the key.");
        scoreController.IncreaseScore(10);
    }
}
