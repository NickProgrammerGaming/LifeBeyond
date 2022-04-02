using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Satellite : MonoBehaviour
{
   private GameObject satteliteObject;
    PlayerMovement playerMovement;

    private void Start()
    {
        satteliteObject = GameObject.Find("Canvas").transform.Find("SatelliteUI").gameObject;
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    public void Interact()
    {
        satteliteObject.SetActive(true);
        playerMovement.enabled = false;
        PlayerShooting.canShoot = false;
        playerMovement.rigbod.velocity = new Vector2(0, playerMovement.rigbod.velocity.y); 
        
    }
}
