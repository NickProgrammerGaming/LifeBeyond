using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interaction : MonoBehaviour
{

    bool inRange = false;
    public UnityEvent interactEvent;
    public bool canInteract = true;
    GameObject interactText;

    private void Start()
    {
        interactText = GameObject.Find("Canvas").transform.Find("InteractText").gameObject;
    }

    void Update()
    {
        if(inRange && canInteract)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                interactEvent.Invoke();
                canInteract = false;
                interactText.SetActive(false);
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player" && canInteract)
        {
            inRange = true;
            interactText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player" && canInteract)
        {
            inRange = false;
            interactText.SetActive(false);
        }
    }
}
