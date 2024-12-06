using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float currentHealth {  get; private set; } 
    [SerializeField]  private  float StartingHealth;
    private Animator anim;
    private void Awake()
    {
        currentHealth = StartingHealth;
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(float _damage)
    {

        currentHealth = Mathf.Clamp(currentHealth -  _damage, 0, StartingHealth);
        if (currentHealth < 0)
        {
            //anim.SetTrigger("Hurt");
        }
        else
        {
            anim.SetTrigger("Die ");

        }

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            TakeDamage(1); 
    }

}
