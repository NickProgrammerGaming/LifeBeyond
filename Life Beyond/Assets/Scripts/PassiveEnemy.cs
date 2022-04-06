using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PassiveEnemy : MonoBehaviour
{
    public new string name;
    public int damage;
    public int maxHealth;
    int currentHealth;
    public float speed;
    public Transform groundDetect;
    public LayerMask ground;
    public float groundDistance;
    public float wallDistance;
    private Rigidbody2D rb;
    public Collider2D bodyCollider;
    bool patrol;
    bool turn;
    public float damageCooldown;
    float nextTimeToAttack;
    public bool boss;
    public Healthbar bossHealthbar;
    public TMP_Text bossName;
    float nextTimeToSpeed;
    public GameObject heartObject;
    int drop;
    public GameObject DeadPart;

    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        patrol = true;
        if (boss)
        {
            bossHealthbar.SetMaxHealth(maxHealth);
            bossName.text = name;
            bossHealthbar.gameObject.SetActive(true);
            bossName.gameObject.SetActive(true);
        }
    }

    private void FixedUpdate()
    {
        turn = !Physics2D.OverlapCircle(groundDetect.position, groundDistance, ground);
    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundDistance, ground);

        if (turn || bodyCollider.IsTouchingLayers(ground))
        {
            Flip();
        }

        if(patrol)
        {
            rb.velocity = new Vector3(speed * Time.fixedDeltaTime, rb.velocity.y);
        }
        
        if(currentHealth <= 0)
        {
            Die();
        }

        if(boss)
        {

            speed = Mathf.Clamp(speed, -2100f, 2100f);

            if(Time.time > nextTimeToSpeed)
            {
                speed *= 1.5f;
                nextTimeToSpeed = Time.time + 3f;
            }

        }
    }
    public void TakeDamage(int damage)
    {
        FindObjectOfType<AudioManager>().Play("Hurt");

        currentHealth -= damage;
        if (boss)
        {
            bossHealthbar.SetHealth(currentHealth);
        }
    }

    public void Die()
    {
        Instantiate(DeadPart,transform.position,Quaternion.identity);
        FindObjectOfType<AudioManager>().Play("EnemyDeath");

        drop = Random.Range(0, 100);

        if(drop <= 20)
        {
            Instantiate(heartObject, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
        if (boss)
        {
            bossHealthbar.gameObject.SetActive(false);
            bossName.gameObject.SetActive(false);
        }
    }
    void Flip()
    {
        patrol = false;
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        speed = -speed;
        patrol = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player" && Time.time > nextTimeToAttack)
        {
            PlayerMovement player = GameObject.Find("Player").GetComponent<PlayerMovement>();
            player.TakeDamage(damage);
            nextTimeToAttack = Time.time + 1f / damageCooldown;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player" && nextTimeToAttack < Time.time)
        {
            PlayerMovement player = GameObject.Find("Player").GetComponent<PlayerMovement>();
            player.TakeDamage(damage);
            nextTimeToAttack = Time.time + 1f / damageCooldown;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(groundDetect.position, groundDistance);
    }


}
