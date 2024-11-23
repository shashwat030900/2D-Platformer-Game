    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    public class PlayerController : MonoBehaviour
    {
        public Animator animator;
        public BoxCollider2D playerCollider;
        public Vector2 standingColliderSize = new Vector2(0.6987874f, 2.111073f); 
        public Vector2 crouchingColliderSize = new Vector2(0.9461729f, 1.321176f); 
        public Vector2 standingColliderOffset = new Vector2(0.006101638f, 0.9829593f);
        public Vector2 crouchingColliderOffset = new Vector2(-0.1175911f, 0.5880105f);
        public float speed;
        private Rigidbody2D rb2d;
        public float jump;
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
        
        
       
           // bool jump = Input.GetAxisRaw("Vertical") > 0;
            //animator.SetBool("Jump", jump);

            bool crouch = Input.GetKey(KeyCode.LeftControl);
            CrouchMovement(crouch);

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
    }
