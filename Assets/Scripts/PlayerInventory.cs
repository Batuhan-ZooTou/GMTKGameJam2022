using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public GameObject itemHolding;
    public GameManager gm;
    public Transform handPoint;
    ObjectPooler objectPooler;
    //[HideInInspector]
   //public int corn;
   //public int cabbage;
   //public int bean;
   //public int meat;
   //public int Chicken;
   //public ItemOrderList meatKind;
   //public ItemOrderList notMeat;
   //public int notMeatCount;
    public GameObject[] itemPrefabs;
    public float itemMoveSpeed;
    public bool moveItem;
    private bool takeItemToHand;

    private Transform moveItemPos;

    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
    }
    private void Update()
    {
        InputManager();
        MoveItem(moveItemPos);
    }
    public void GiveBurrito(Costumer costumer)
    {
        costumer.TakeBurrito(itemHolding);
        itemHolding = null;
    }
    public void TakeItem(GameObject item)
    {
        if (itemHolding==null)
        {
            itemHolding = item;
            moveItemPos = handPoint;
            takeItemToHand = true;
            moveItem = true;
            item.transform.SetParent(transform);
            
            if (itemHolding.gameObject.CompareTag("Meat") || itemHolding.gameObject.CompareTag("Chicken"))
            {
                item.transform.localScale = new Vector3(0.25f, 0.2f, 0.1f);
            }
            else if (itemHolding.gameObject.CompareTag("Bean") || itemHolding.gameObject.CompareTag("Corn"))
            {
                item.transform.localScale = new Vector3(0.25f, 0.13f, 0.3f);
            }
            else if ( itemHolding.gameObject.CompareTag("Cabbage"))
            {
                item.transform.localScale = new Vector3(0.3f, 0.15f, 0.35f);
            }
            else if(itemHolding.gameObject.CompareTag("Burrito"))
            {
                gm.burritos.Remove(item);
                item.transform.localScale = new Vector3(5f, 0.15f, 0.3f);
            }
            item.GetComponent<BoxCollider>().isTrigger = true;
            item.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
    public void DropItem()
    {
        itemHolding.transform.SetParent(null);
        itemHolding.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        itemHolding.gameObject.GetComponent<BoxCollider>().isTrigger = false;
        itemHolding = null;
    }

    public void InputManager()
    {
        //drop item
        if (Input.GetKeyDown(KeyCode.Q) && itemHolding != null)
        {
            DropItem();
        }
    }

    public void Takeitem(GameObject item)
    {
        if (itemHolding == null)
        {
            itemHolding = item;
            moveItemPos = handPoint;
            takeItemToHand = true;
            moveItem = true;
            item.GetComponent<Transform>().SetParent(transform);
        }
    }
    public void TakeitemFromPool(string tag)
    {
        if (itemHolding == null)
        {
            itemHolding = objectPooler.SpawnFromPool(tag, handPoint.position, handPoint.rotation);
            itemHolding.transform.SetParent(transform);
        }
    }
    //taking items from box
    public void TakeitemBox(ItemBoxes itemBox)
    {
        if (itemHolding == null && itemBox.itemCount>0)
        {
            itemBox.itemCount--;
            moveItemPos = handPoint;
            itemHolding = objectPooler.SpawnFromPool(itemBox.item.ToString(), itemBox.GetComponent<Transform>().position, itemBox.GetComponent<Transform>().rotation);
            takeItemToHand = true;
            moveItem = true;
            itemHolding.transform.SetParent(transform);
        }
    }
    //droping items back to box
    public void DropitemBox(ItemBoxes itemBox)
    {
        if (itemHolding != null && moveItem == false && itemBox.itemCount!= itemBox.maxItemCount && itemBox.item == WhichItemHolding())
        {
            moveItemPos = itemBox.GetComponent<Transform>();
            moveItem = true;
            itemBox.itemCount++;
        }
    }
    public void MoveItem(Transform pos)
    {
        if (moveItem)
        {
            // move item to pos
            itemHolding.GetComponent<Transform>().position = Vector3.Lerp(itemHolding.GetComponent<Transform>().position, pos.position, itemMoveSpeed);
            //take item to hand
            if (Vector3.Distance(itemHolding.GetComponent<Transform>().position, handPoint.position) < 0.1f && takeItemToHand)
            {
                takeItemToHand = false;
                moveItem = false;
                itemHolding.transform.position = handPoint.position;
                itemHolding.transform.rotation = handPoint.rotation;
            }
            //drop back to itembox
            else if (Vector3.Distance(itemHolding.GetComponent<Transform>().position, pos.position) < 0.1f && pos.position!=handPoint.position)
            {
                objectPooler.AddToPool(itemHolding);
                itemHolding = null;
                moveItem = false;
            }
        }
    }
    public  ItemOrderList WhichItemHolding()
    {
        if (itemHolding.gameObject.CompareTag("Meat"))
        {
            return ItemOrderList.Meat;
        }
        else if (itemHolding.gameObject.CompareTag("Chicken"))
        {
            return ItemOrderList.Chicken;
        }
        else if (itemHolding.gameObject.CompareTag("Bean"))
        {
            return ItemOrderList.Bean;
        }
        else if (itemHolding.gameObject.CompareTag("Corn"))
        {
            return ItemOrderList.Corn;
        }
        else if (itemHolding.gameObject.CompareTag("Cabbage"))
        {
            return ItemOrderList.Cabbage;
        }
        else
        {
            return ItemOrderList.Null;
        }
    }
}
