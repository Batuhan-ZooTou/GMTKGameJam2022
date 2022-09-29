using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactor : MonoBehaviour
{
    public LayerMask InteractableLayerMask = 8;
    public PlayerInventory playerInventory;
    public GameManager gm;
    public float reachRange;


    UnityEvent onInteract;

    UnityEvent onDisInteract;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, reachRange, InteractableLayerMask))
        {
            if (hit.collider.GetComponent<Interactable>() != false)
            {
                onInteract = hit.collider.GetComponent<Interactable>().onInteract;
                onDisInteract = hit.collider.GetComponent<Interactable>().onDisInteract;
                if (Input.GetMouseButtonDown(0))
                {
                    onInteract.Invoke();
                }
                else if (Input.GetMouseButtonDown(1))
                {
                    onDisInteract.Invoke();
                }
            }
            else if (hit.collider.GetComponent<Burrito>() != false && Input.GetMouseButton(0))
            {
                playerInventory.TakeItem(hit.collider.gameObject);
            }
            else if (hit.collider.GetComponent<Costumer>() != false && Input.GetMouseButton(0) && playerInventory.itemHolding!=null && playerInventory.itemHolding.GetComponent<Burrito>()!= false)
            {
                hit.collider.GetComponent<Costumer>().GetItem(playerInventory.itemHolding.GetComponent<Burrito>());
                playerInventory.itemHolding.GetComponent<Transform>().SetParent(null);
                playerInventory.GiveBurrito(hit.collider.GetComponent<Costumer>());
            }
            else if (hit.collider.GetComponent<Costumer>() != false && Input.GetMouseButtonDown(1) && hit.collider.GetComponent<Costumer>().hasBurrito == false)
            {
                hit.collider.GetComponent<Costumer>().hasBurrito=true;
                gm.costumerStatus -= 2;
            }
            else if (hit.collider.GetComponent<Trader>() != false && Input.GetMouseButtonDown(0))
            {
                hit.collider.GetComponent<Trader>().CompleteDeal();
                gm.UpdateTraderData();
            }
            else if (hit.collider.GetComponent<Trader>() != false && Input.GetMouseButton(1))
            {
                hit.collider.GetComponent<Trader>().hasDeal=false;
            }
            else if(Input.GetMouseButton(0) &&(hit.collider.CompareTag("Meat") || hit.collider.CompareTag("Chicken") || hit.collider.CompareTag("Corn")|| hit.collider.CompareTag("Bean") || hit.collider.CompareTag("Cabbage") || hit.collider.CompareTag("Burrito")))
            {
                playerInventory.TakeItem(hit.collider.gameObject);
            }
            //Debug.Log(hit.collider.name);
        }
    }
    
}
