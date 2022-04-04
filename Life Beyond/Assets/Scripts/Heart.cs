using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    public int healAmount;
    PlayerMovement player;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = collision.gameObject.GetComponent<PlayerMovement>();

            if (player != null)
            {
                player.currentHealth += healAmount;
            }

            if (player.currentHealth > player.maxHealth.Value)
            {
                player.currentHealth -= (player.currentHealth - player.maxHealth.Value);
            }

            player.playerHealthbar.SetHealth(player.currentHealth);

            FindObjectOfType<AudioManager>().Play("Pickup");

            Destroy(gameObject);
        }
    }
}
