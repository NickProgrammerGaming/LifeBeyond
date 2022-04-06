using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShootingStaticEnemy : MonoBehaviour
{
    public new string name;
    public float fireRate;
    float nextTimeToShoot;
    public GameObject bulletPrefab;
    public Transform playerTransform;
    public float projectileSpeed;
    bool hasSeenPlayer;
    public float seeDistance;
    public int maxHealth;
    int currentHealth;
    public bool boss;
    public Healthbar bossHealthbar;
    public TMP_Text bossName;
    public GameObject heartObject;
    int drop;

    public GameObject DeadPart;

    private void Start()
    {
        currentHealth = maxHealth;
        if (boss)
        {
            bossHealthbar.SetMaxHealth(maxHealth);
            bossName.text = name;
            bossHealthbar.gameObject.SetActive(true);
            bossName.gameObject.SetActive(true);
        }
    }

    void Update()
    {
        hasSeenPlayer = Vector2.Distance(playerTransform.position, transform.position) <= seeDistance;
        if(hasSeenPlayer == true && Time.time > nextTimeToShoot && playerTransform.GetComponent<PlayerMovement>().currentHealth > 0)
        {
            Shoot();

            nextTimeToShoot = Time.time + 1f / fireRate;
        }

        if(currentHealth <= 0)
        {
            Die();
        }
        
    }

    void Shoot()
    {
        Vector2 shootDirection = (playerTransform.position - transform.position).normalized;
        float bulletAngle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;

        GameObject instantiatedBullet = Instantiate(bulletPrefab, transform.position, Quaternion.AngleAxis(bulletAngle, Vector3.forward));

        Rigidbody2D bulletRb = instantiatedBullet.GetComponent<Rigidbody2D>();

        if (bulletRb != null)
        {
            bulletRb.AddForce(shootDirection * projectileSpeed);
        }

        FindObjectOfType<AudioManager>().Play("Shoot");

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            PlayerMovement player = collision.transform.GetComponent<PlayerMovement>();
            player.TakeDamage(1);
        }
    }

    public void TakeDamage(int damage)
    {
        FindObjectOfType<AudioManager>().Play("Hurt");
        currentHealth -= damage;
        if(boss)
        {
            bossHealthbar.SetHealth(currentHealth);
        }
    }

    public void Die()
    {
        Instantiate(DeadPart,transform.position,Quaternion.identity);
        FindObjectOfType<AudioManager>().Play("EnemyDeath");
        

        if (drop <= 20)
        {
            
            Instantiate(heartObject, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
        if(boss)
        {
            bossHealthbar.gameObject.SetActive(false);
            bossName.gameObject.SetActive(false);
        }
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, seeDistance);
    }
}
