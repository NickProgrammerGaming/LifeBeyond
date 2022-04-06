using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    public Animator animator;
    public float MovementSpeed;
    public float JumpForce;

    [HideInInspector]public Rigidbody2D rigbod;
    [HideInInspector]public float move;
    //GroundCheck
    private bool isGrounded;
    public Transform groundCheckPos;
    public float groundCheckRadius;
    public LayerMask GroundLayer;
    public IntSO maxHealth;
    public IntSO damageMod;
    [HideInInspector]
    public int currentHealth;
    
    private bool isVoid = false;
    public LayerMask Void;
    
    public float Respawnx;
    public float Respawny;

    public LayerMask checkpointMask;

    public Healthbar playerHealthbar;
    public GameObject gameOverScreen;

    public ParticleSystem Dust;

    // Start is called before the first frame update
    void Start()
    {
        rigbod = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth.Value;
        playerHealthbar.SetMaxHealth(maxHealth.Value);
    }
    

    // Update is called once per frame
    void Update()
    {
            move = Input.GetAxis("Horizontal");
            animator.SetFloat("Running", Mathf.Abs(move));
            Vector3 chScale = transform.localScale;
            if (move < 0)
            {
                chScale.x = -1;
            }
            if (move > 0)
            {

                chScale.x = 1;
            }

            transform.localScale = chScale;
            rigbod.velocity = new Vector2(move * MovementSpeed, rigbod.velocity.y);
            isGrounded = Physics2D.OverlapCircle(groundCheckPos.position, groundCheckRadius, GroundLayer);
            animator.SetBool("IsGrounded", isGrounded);
            if (Input.GetButtonDown("Jump") && isGrounded == true)
            {
                CreateDust();
                rigbod.velocity = new Vector2(rigbod.velocity.x, JumpForce);

            }
            if (Input.GetButtonUp("Jump") && rigbod.velocity.y > 0)
            {
                rigbod.velocity = new Vector2(rigbod.velocity.x, rigbod.velocity.y * .5f);
            }

        if (rigbod.velocity.y < -0.01f)
        {
            animator.SetBool("Jump", false);

        }
        else if (rigbod.velocity.y > 0.1f)
        {
            animator.SetBool("Jump", true);
        }

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckPos.position, groundCheckRadius, Void);
        if (colliders.Length > 0)
        {
            isVoid = true;
            TakeDamage(1);
            Die();
        }

        if(currentHealth <= 0)
        {
            FindObjectOfType<AudioManager>().Play("PlayerDeath");
            gameOverScreen.SetActive(true);
            gameObject.SetActive(false);
        }
            

    }

    public void Restart()
    {
        damageMod.Value = 0;
        maxHealth.Value = 5;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void Die()
    {
        Vector3 Coor = transform.localPosition;
        if (isVoid)
        {
            Coor.y = Respawny;
            Coor.x = Respawnx;
            rigbod.velocity = new Vector2(rigbod.velocity.x, 0);
        }
        transform.localPosition = Coor;
    }

    public void TakeDamage(int damage)
    {
        FindObjectOfType<AudioManager>().Play("Hurt");
        currentHealth -= damage;
        playerHealthbar.SetHealth(currentHealth);

    }

    public void ChangeScene(int sceneNumber)
    {

        switch(sceneNumber)
        {
            case 2:
                FindObjectOfType<AudioManager>().Stop("Level1");
                FindObjectOfType<AudioManager>().Play("Level2");
                break;
            case 3:
                FindObjectOfType<AudioManager>().Stop("Level2");
                FindObjectOfType<AudioManager>().Play("Level3");
                break;
        }


        SceneManager.LoadScene(sceneNumber);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(groundCheckPos.position, groundCheckRadius);
    }

    void CreateDust(){
        Dust.Play();
    }
}