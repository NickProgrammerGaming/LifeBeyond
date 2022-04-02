using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    Animator cpText;

    private void Start()
    {
        cpText = GameObject.Find("CheckpointReached").GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.transform.tag == "Player")
        {
            PlayerMovement player = GameObject.Find("Player").GetComponent<PlayerMovement>();
            player.Respawnx = transform.position.x;
            player.Respawny = transform.position.y;
            cpText.SetTrigger("CheckpointReached");
            Destroy(gameObject);

        }

        
    }
}
