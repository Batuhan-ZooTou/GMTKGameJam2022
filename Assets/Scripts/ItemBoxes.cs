using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBoxes : MonoBehaviour
{
    public PlayerInventory playerInventory;
    public ItemOrderList item;
    public int itemCount;
    ObjectPooler objectPooler;
    public Text count;
    public int maxItemCount;
    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
    }
    private void FixedUpdate()
    {
        if (itemCount>maxItemCount)
        {
            itemCount = maxItemCount;
        }
        count.text = itemCount + "/" + maxItemCount;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Rigidbody>().isKinematic == false && playerInventory.moveItem == false)
        {
            if (other.gameObject.CompareTag("Meat") && item == ItemOrderList.Meat && itemCount != maxItemCount)
            {
                other.gameObject.GetComponent<BoxCollider>().isTrigger = true;
                other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                objectPooler.AddToPool(other.gameObject);
                itemCount++;
            }
            else if (other.gameObject.CompareTag("Chicken") && item == ItemOrderList.Chicken && itemCount != maxItemCount)
            {
                other.gameObject.GetComponent<BoxCollider>().isTrigger = true;
                other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                objectPooler.AddToPool(other.gameObject);
                itemCount++;
            }
            else if (other.gameObject.CompareTag("Bean") && item == ItemOrderList.Bean && itemCount != maxItemCount)
            {
                other.gameObject.GetComponent<BoxCollider>().isTrigger = true;
                other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                objectPooler.AddToPool(other.gameObject);
                itemCount++;
            }
            else if (other.gameObject.CompareTag("Corn") && item == ItemOrderList.Corn && itemCount != maxItemCount)
            {
                other.gameObject.GetComponent<BoxCollider>().isTrigger = true;
                other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                objectPooler.AddToPool(other.gameObject);
                itemCount++;
            }
            else if (other.gameObject.CompareTag("Cabbage") && item == ItemOrderList.Cabbage && itemCount != maxItemCount)
            {
                other.gameObject.GetComponent<BoxCollider>().isTrigger = true;
                other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                objectPooler.AddToPool(other.gameObject);
                itemCount++;
            }
            
        }

    }
}
