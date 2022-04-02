using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Satellite : MonoBehaviour
{
   private GameObject satteliteObject;

    private void Start()
    {
        satteliteObject = GameObject.Find("Canvas").transform.Find("SatelliteUI").gameObject;
    }

    public void Interact()
    {
        satteliteObject.SetActive(true);
        PlayerMovement.canMove = false;
        PlayerShooting.canShoot = false;
    }
}
