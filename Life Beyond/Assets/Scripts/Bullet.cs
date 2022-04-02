using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    public BulletMode bulletMode;
    private PlayerShooting playerShooting;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(bulletMode == BulletMode.Player)
        {
            if (collision.transform.tag == "Enemy")
            {
                PassiveEnemy pEnemy = collision.gameObject.GetComponent<PassiveEnemy>();
                ShootingStaticEnemy sEnemy = collision.gameObject.GetComponent<ShootingStaticEnemy>();
                Boss boss = collision.gameObject.GetComponent<Boss>();

                playerShooting = GameObject.Find("Player").GetComponent<PlayerShooting>();

                int _damage = damage + playerShooting.damageModifiers;

                if (pEnemy != null)
                {
                    pEnemy.TakeDamage(_damage);
                }
                else if (sEnemy != null)
                {
                    sEnemy.TakeDamage(_damage);
                }
                else
                {
                    boss.TakeDamage(_damage);
                }

            }
        }
        else
        {
            if (collision.transform.tag == "Player")
            {
                PlayerMovement player = collision.transform.GetComponent<PlayerMovement>();
                player.TakeDamage(damage);

            }
        }

        Destroy(gameObject);
    }


    public enum BulletMode { Player, Enemy }
}
