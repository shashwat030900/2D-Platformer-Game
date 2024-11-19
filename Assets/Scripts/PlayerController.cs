using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public BoxCollider2D playerCollider;
    public Vector2 standingColliderSize = new Vector2(0.6987874f, 2.111073f); 
    public Vector2 crouchingColliderSize = new Vector2(0.9461729f, 1.321176f); 
    public Vector2 standingColliderOffset = new Vector2(0.006101638f, 0.9829593f);
    public Vector2 crouchingColliderOffset = new Vector2(-0.1175911f, 0.5880105f);

    private void Awake()
    {
        Debug.Log("Player controller awake");
    }

    private void Update()
    {
        float speed = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(speed));

        bool jump = Input.GetAxisRaw("Vertical") > 0;
        animator.SetBool("Jump", jump);

        bool crouch = Input.GetKey(KeyCode.LeftControl);
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

        Vector3 scale = transform.localScale;
        if (speed < 0)
        {
            scale.x = -1f * Mathf.Abs(scale.x);
        }
        else if (speed > 0)
        {
            scale.x = Mathf.Abs(scale.x);
        }
        transform.localScale = scale;
    }
}
