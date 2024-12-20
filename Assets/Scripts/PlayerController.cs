using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameOverController gameOverController;
    public Animator animator;
    public BoxCollider2D playerCollider;
    public Vector2 standingColliderSize = new Vector2(0.6987874f, 2.111073f);
    public Vector2 crouchingColliderSize = new Vector2(0.9461729f, 1.321176f);
    public Vector2 standingColliderOffset = new Vector2(0.006101638f, 0.9829593f);
    public Vector2 crouchingColliderOffset = new Vector2(-0.1175911f, 0.5880105f);
    public float speed;
    private Rigidbody2D rb2d;
    [Header("Speed")]
    [SerializeField] private float jump;
    public ScoreController scoreController;
    

    /// <summary>
    /// Health
    /// </summary>
    private bool isDead = false;
    public Image heartImage;
    public float maxHealthFillAmount = 1f;
    public float healthFillAmount;
    public float damageAmount = 0.1f;
    public Animator playerAnimator;
    public GameObject deathUIPanel;
    [SerializeField]  private int health;
    [SerializeField] private Image[] hearts; 

    private void Start()
    {
        health = hearts.Length;
    }

    public void DecreaseHealth()
    {
        health--; 
        // call a function to running health UI.
        ReduceHealthUI();   
        if (health <= 0)
        {
            Debug.Log("Health decreased, player is dead.");
            Die();
        }
    }

    private void ReduceHealthUI()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].color = (i < health) ? Color.red : Color.black;
        }
        
    }

    private void Die()
    {
       
        playerAnimator.SetTrigger("Die");
        StartCoroutine(WaitTimer());
        //gameOverController.PlayerDied();

       
    }


    private IEnumerator WaitTimer()
    {
        yield return new WaitForSeconds(1.8f);
        deathUIPanel.SetActive(true);
        //SceneManager.LoadScene(1);

        this.enabled = false;
    }

    
    private void Awake()
    {

        Debug.Log("Player controller awake");
        rb2d = gameObject.GetComponent<Rigidbody2D>();
    }
    
    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Jump");
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
    private void MoveCharacter(float horizontal, float vertical)
    {
        Vector3 position = transform.position;
        position.x = position.x + horizontal * speed * Time.deltaTime;
        transform.position = position;

        if (vertical > 0)
        {
            rb2d.AddForce(new Vector2(0f, jump), ForceMode2D.Impulse);
        }
    }

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

        //Jump
        if (vertical > 0)
        {
            animator.SetBool("Jump", true);
        }
        else
        {
            animator.SetBool("Jump", false);
        }
    }

    public void PickUpkey()
    {
        Debug.Log("Player picked up the key.");
        scoreController.IncreaseScore(10);
    }

    
}


