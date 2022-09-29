using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurritoMachine : MonoBehaviour
{
    public List<GameObject> items = new List<GameObject>();
    private GameObject burrito;
    public GameManager gm;
    ObjectPooler objectPooler;
    public Transform burritoPoint;
    public PlayerInventory playerInventory;
    public Transform DropPoint;
    public Transform burritoPoint2;
    public bool canCook = true;
    public float cookCounter;
    public bool itemTaken = false;
    public int maxItemCount=10;
    public Transform[] dropPoints;
    // Start is called before the first frame update
    void Start()
    {
        objectPooler = ObjectPooler.Instance;
        Invoke("RefleshBurrito",1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (itemTaken)
        {
            playerInventory.itemHolding.GetComponent<Transform>().position = Vector3.Lerp(playerInventory.itemHolding.GetComponent<Transform>().position, DropPoint.position, playerInventory.itemMoveSpeed);
            if (Vector3.Distance(playerInventory.itemHolding.GetComponent<Transform>().position, DropPoint.position)<0.1f)
            {
                itemTaken = false;
                playerInventory.DropItem();
            }
        }
    }
    public void RefleshBurrito()
    {
        burrito = objectPooler.SpawnFromPool("Burrito", burritoPoint.position, burritoPoint.rotation);
        burrito.GetComponent<Burrito>().meat = ItemOrderList.Null;
        burrito.GetComponent<Burrito>().notMeat1 = ItemOrderList.Null;
        burrito.GetComponent<Burrito>().notMeat2 = ItemOrderList.Null;
        burrito.GetComponent<Burrito>().notMeat3 = ItemOrderList.Null;
        burrito.GetComponent<Burrito>().meatCount = 0;
        burrito.GetComponent<Burrito>().notMeatCount1 = 0;
        burrito.GetComponent<Burrito>().notMeatCount2 = 0;
        burrito.GetComponent<Burrito>().notMeatCount3 = 0;
        burrito.GetComponent<Burrito>().itemCount = 0;
        burrito.GetComponent<Animator>().enabled = true;

    }
    public void TakeItem()
    {
        if (playerInventory.itemHolding!=null && canCook && items.Count<maxItemCount)
        {
            items.Add(playerInventory.itemHolding);
            itemTaken = true;
            playerInventory.itemHolding.GetComponent<Transform>().SetParent(null);
        }
    }
    public void CookBurrito()
    {
        if (canCook && burrito.GetComponent<Burrito>().itemCount>0)
        {
            items.RemoveRange(0,items.Count);
            canCook = false;
            StartCoroutine("CookCooldown");
        }
    }
    public void ButtonPress()
    {
        StartCoroutine("GiveBackItem");
        Debug.Log("pressed");
    }
    public IEnumerator GiveBackItem()
    {
        foreach (GameObject item in items)
        {
            if (item.gameObject.CompareTag("Meat"))
            {
                GameObject temp=objectPooler.SpawnFromPool("Meat", dropPoints[0].position, dropPoints[0].rotation);
                temp.gameObject.GetComponent<BoxCollider>().isTrigger = false;
                temp.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            }
            else if (item.gameObject.CompareTag("Chicken"))
            {
                GameObject temp = objectPooler.SpawnFromPool("Chicken", dropPoints[1].position, dropPoints[1].rotation);
                temp.gameObject.GetComponent<BoxCollider>().isTrigger = false;
                temp.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            }
            else if (item.gameObject.CompareTag("Corn"))
            {
                GameObject temp = objectPooler.SpawnFromPool("Corn", dropPoints[2].position, dropPoints[2].rotation);
                temp.gameObject.GetComponent<BoxCollider>().isTrigger = false;
                temp.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            }
            else if (item.gameObject.CompareTag("Cabbage"))
            {
                GameObject temp = objectPooler.SpawnFromPool("Cabbage", dropPoints[3].position, dropPoints[3].rotation);
                temp.gameObject.GetComponent<BoxCollider>().isTrigger = false;
                temp.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            }
            else if (item.gameObject.CompareTag("Bean"))
            {
                GameObject temp = objectPooler.SpawnFromPool("Bean", dropPoints[4].position, dropPoints[4].rotation);
                temp.gameObject.GetComponent<BoxCollider>().isTrigger = false;
                temp.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            }
            yield return new WaitForSeconds(0.5f);
        }
        items.RemoveRange(0, items.Count);
    }
    IEnumerator CookCooldown()
    {
        yield return new WaitForSeconds(cookCounter);
        burrito.transform.position = burritoPoint2.position;
        burrito.transform.rotation = burritoPoint2.rotation;
        burrito.GetComponent<Animator>().SetTrigger("move");
        gm.burritos.Add(burrito);
        RefleshBurrito();
        canCook = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Meat") && playerInventory.itemHolding==null)
        {
            burrito.GetComponent<Burrito>().meat = ItemOrderList.Meat;
            burrito.GetComponent<Burrito>().meatCount++;
            burrito.GetComponent<Burrito>().itemCount ++;
            other.gameObject.GetComponent<BoxCollider>().isTrigger = true;
            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            objectPooler.AddToPool(other.gameObject);

        }
        if (other.gameObject.CompareTag("Chicken") && playerInventory.itemHolding == null)
        {
            burrito.GetComponent<Burrito>().meat = ItemOrderList.Chicken;
            burrito.GetComponent<Burrito>().meatCount++;
            burrito.GetComponent<Burrito>().itemCount ++;
            other.gameObject.GetComponent<BoxCollider>().isTrigger = true;
            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            objectPooler.AddToPool(other.gameObject);
        }
        if (other.gameObject.CompareTag("Corn") && playerInventory.itemHolding == null)
        {
            if (burrito.GetComponent<Burrito>().notMeat1==ItemOrderList.Null || burrito.GetComponent<Burrito>().notMeat1 == ItemOrderList.Corn)
            {
                burrito.GetComponent<Burrito>().notMeat1 = ItemOrderList.Corn;
                burrito.GetComponent<Burrito>().notMeatCount1++;
            }
            else if (burrito.GetComponent<Burrito>().notMeat2 == ItemOrderList.Null || burrito.GetComponent<Burrito>().notMeat2 == ItemOrderList.Corn)
            {
                burrito.GetComponent<Burrito>().notMeat2 = ItemOrderList.Corn;
                burrito.GetComponent<Burrito>().notMeatCount2++;
            }
            else
            {
                burrito.GetComponent<Burrito>().notMeat3 = ItemOrderList.Corn;
                burrito.GetComponent<Burrito>().notMeatCount3++;
            }
            burrito.GetComponent<Burrito>().itemCount ++;
            other.gameObject.GetComponent<BoxCollider>().isTrigger = true;
            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            objectPooler.AddToPool(other.gameObject);
        }
        if (other.gameObject.CompareTag("Bean") && playerInventory.itemHolding == null)
        {
            if (burrito.GetComponent<Burrito>().notMeat1 == ItemOrderList.Null || burrito.GetComponent<Burrito>().notMeat1 == ItemOrderList.Bean)
            {
                burrito.GetComponent<Burrito>().notMeat1 = ItemOrderList.Bean;
                burrito.GetComponent<Burrito>().notMeatCount1++;
            }
            else if (burrito.GetComponent<Burrito>().notMeat2 == ItemOrderList.Null || burrito.GetComponent<Burrito>().notMeat2 == ItemOrderList.Bean)
            {
                burrito.GetComponent<Burrito>().notMeat2 = ItemOrderList.Bean;
                burrito.GetComponent<Burrito>().notMeatCount2++;
            }
            else
            {
                burrito.GetComponent<Burrito>().notMeat3 = ItemOrderList.Bean;
                burrito.GetComponent<Burrito>().notMeatCount3++;
            }
            burrito.GetComponent<Burrito>().itemCount ++;
            other.gameObject.GetComponent<BoxCollider>().isTrigger = true;
            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            objectPooler.AddToPool(other.gameObject);
        }
        if (other.gameObject.CompareTag("Cabbage") && playerInventory.itemHolding == null)
        {
            if (burrito.GetComponent<Burrito>().notMeat1 == ItemOrderList.Null || burrito.GetComponent<Burrito>().notMeat1 == ItemOrderList.Cabbage)
            {
                burrito.GetComponent<Burrito>().notMeat1 = ItemOrderList.Cabbage;
                burrito.GetComponent<Burrito>().notMeatCount1++;
            }
            else if (burrito.GetComponent<Burrito>().notMeat2 == ItemOrderList.Null || burrito.GetComponent<Burrito>().notMeat2 == ItemOrderList.Cabbage)
            {
                burrito.GetComponent<Burrito>().notMeat2 = ItemOrderList.Cabbage;
                burrito.GetComponent<Burrito>().notMeatCount2++;
            }
            else
            {
                burrito.GetComponent<Burrito>().notMeat3 = ItemOrderList.Cabbage;
                burrito.GetComponent<Burrito>().notMeatCount3++;
            }
            burrito.GetComponent<Burrito>().itemCount ++;
            other.gameObject.GetComponent<BoxCollider>().isTrigger = true;
            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            objectPooler.AddToPool(other.gameObject);
        }
        if (other.gameObject.CompareTag("Burrito") && playerInventory.itemHolding == null)
        {
            other.gameObject.GetComponent<Animator>().enabled = false;
            other.gameObject.GetComponent<Transform>().position = gm.BurritoStackPoint[gm.burritos.Count - 1].position;
            

        }
    }
}
