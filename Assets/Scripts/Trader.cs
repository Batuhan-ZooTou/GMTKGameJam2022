using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using UnityEngine.UI;

public class Trader : MonoBehaviour
{
    [Header("UI")]
    private Camera cam;
    public Canvas bouble;
    public Sprite[] icons;
    public Text itemGivenCount;
    public Text itemWantedCount;
    public Image itemWanted;
    public Image itemGiven;
    [Header("OnEnable")]
    public bool hasDeal;
    public GameManager gm;
    private NavMeshAgent navMeshAgent;
    public Transform exitPoint;
    public int inWhichLine;


    public float stayInLineTimer;
    [SerializeField]private float stayInLineCounter;

    [Header("PlayerInventory")]
    public int corn;
    public int cabbage;
    public int bean;
    public int meat;
    public int Chicken;
    public int[] highestItem;
    public int givenItem;
    public int takenItem;
    public int itemWantedCountNo;
    public int itemGivenCountNo;
    
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }
    private void OnEnable()
    {
        stayInLineCounter = stayInLineTimer;
        navMeshAgent = GetComponent<NavMeshAgent>();
        exitPoint = GameObject.Find("GameManager/Sp").GetComponent<Transform>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        corn = GameObject.Find("ItemBoxes/CornBox").GetComponent<ItemBoxes>().itemCount;
        meat = GameObject.Find("ItemBoxes/MeatBox").GetComponent<ItemBoxes>().itemCount;
        Chicken = GameObject.Find("ItemBoxes/ChickenBox").GetComponent<ItemBoxes>().itemCount;
        bean = GameObject.Find("ItemBoxes/BeanBox").GetComponent<ItemBoxes>().itemCount;
        cabbage = GameObject.Find("ItemBoxes/CabbageBox").GetComponent<ItemBoxes>().itemCount;
        hasDeal = true;
        transform.position = exitPoint.position;
            CheckForItemToDeal();
    }
    // Update is called once per frame
    void Update()
    {
        bouble.transform.rotation = Quaternion.LookRotation(bouble.transform.position - cam.transform.position);
        if (inWhichLine==1)
        {
            stayInLineCounter -= Time.deltaTime;
            if (stayInLineCounter<=0)
            {
                stayInLineCounter = stayInLineTimer;
                hasDeal = false;
            }
        }
        if (!hasDeal)
        {
            navMeshAgent.speed = 3.5f;
            navMeshAgent.destination = exitPoint.position;
        }
    }
    public void CompleteDeal()
    {
        if (takenItem == 0 && itemWantedCountNo <= GameObject.Find("ItemBoxes/MeatBox").GetComponent<ItemBoxes>().itemCount && hasDeal)
        {
            hasDeal = false;
            GameObject.Find("ItemBoxes/MeatBox").GetComponent<ItemBoxes>().itemCount -= itemWantedCountNo;
            AddToItemBox(givenItem,itemGivenCountNo);
            gm.costumerStatus++;
        }
        else if (takenItem == 1 && itemWantedCountNo <= GameObject.Find("ItemBoxes/ChickenBox").GetComponent<ItemBoxes>().itemCount && hasDeal)
        {
            hasDeal = false;
            GameObject.Find("ItemBoxes/ChickenBox").GetComponent<ItemBoxes>().itemCount -= itemWantedCountNo;
            AddToItemBox(givenItem,itemGivenCountNo);
            gm.costumerStatus++;
        }
        else if (takenItem == 2 && itemWantedCountNo <= GameObject.Find("ItemBoxes/CornBox").GetComponent<ItemBoxes>().itemCount && hasDeal)
        {
            hasDeal = false;
            GameObject.Find("ItemBoxes/CornBox").GetComponent<ItemBoxes>().itemCount -= itemWantedCountNo;
            AddToItemBox(givenItem,itemGivenCountNo);
            gm.costumerStatus++;
        }
        else if (takenItem == 3 && itemWantedCountNo <= GameObject.Find("ItemBoxes/CabbageBox").GetComponent<ItemBoxes>().itemCount && hasDeal)
        {
            hasDeal = false;
            GameObject.Find("ItemBoxes/CabbageBox").GetComponent<ItemBoxes>().itemCount -= itemWantedCountNo;
            AddToItemBox(givenItem,itemGivenCountNo);
            gm.costumerStatus++;
        }
        else if (takenItem == 4 && itemWantedCountNo <= GameObject.Find("ItemBoxes/BeanBox").GetComponent<ItemBoxes>().itemCount && hasDeal)
        {
            hasDeal = false;
            GameObject.Find("ItemBoxes/BeanBox").GetComponent<ItemBoxes>().itemCount -= itemWantedCountNo;
            AddToItemBox(givenItem,itemGivenCountNo);
            gm.costumerStatus++;
        }
    }
    void AddToItemBox(int item, int count)
    {
        if (item == 0)
        {
            GameObject.Find("ItemBoxes/MeatBox").GetComponent<ItemBoxes>().itemCount += count;
        }
        else if (item == 1)
        {
            
            GameObject.Find("ItemBoxes/ChickenBox").GetComponent<ItemBoxes>().itemCount += count;
        }
        else if (item == 2)
        {
            GameObject.Find("ItemBoxes/CornBox").GetComponent<ItemBoxes>().itemCount += count;
        }
        else if (item == 3)
        {
            GameObject.Find("ItemBoxes/CabbageBox").GetComponent<ItemBoxes>().itemCount += count;
        }
        else if (item == 4)
        {
            GameObject.Find("ItemBoxes/BeanBox").GetComponent<ItemBoxes>().itemCount += count;
        }
    }
    void RandomItem(int item)
    {
        int rnd = Mathf.FloorToInt(Random.Range(0, 5));
        if (rnd==0 && item != 0)
        {
            itemWanted.sprite = icons[0];
            takenItem = 0;
        }
        else if (rnd == 1 && item !=1)
        {
            itemWanted.sprite = icons[1];
            takenItem = 1;
        }
        else if(rnd == 2 && item != 2)
        {
            itemWanted.sprite = icons[2];
            takenItem = 2;
        }
        else if(rnd == 3 && item != 3)
        {
            itemWanted.sprite = icons[3];
            takenItem = 3;
        }
        else if(rnd == 4 && item != 4)
        {
            itemWanted.sprite = icons[4];
            takenItem = 4;
        }
        else
        {
            RandomItem(item);
        }
    }
    void CheckForItemToDeal()
    {
        UpdateItemBox();
        highestItem[2] = corn;
        highestItem[3] = cabbage;
        highestItem[4] = bean;
        highestItem[0] = meat;
        highestItem[1] = Chicken;
        int max = highestItem.Max();
        int rnd = Mathf.FloorToInt(Random.Range(1, 6));
        if (rnd == 1 && max!= meat)
        {
            itemWantedCountNo = Mathf.FloorToInt(Random.Range(1, 5));
            itemWantedCount.text = itemWantedCountNo.ToString();
            itemGivenCountNo = Mathf.FloorToInt(Random.Range(1, 5));
            itemGivenCount.text = itemGivenCountNo.ToString();
            givenItem = 0;
            itemGiven.sprite = icons[0];
            RandomItem(0);
        }
        else if (rnd == 2 && max != Chicken)
        {
            itemWantedCountNo = Mathf.FloorToInt(Random.Range(1, 5));
            itemWantedCount.text = itemWantedCountNo.ToString();
            itemGivenCountNo = Mathf.FloorToInt(Random.Range(1, 5));
            itemGivenCount.text = itemGivenCountNo.ToString();
            givenItem = 1;
            itemGiven.sprite = icons[1];
            RandomItem(1);
        }
        else if (rnd == 3 && max != corn)
        {
            itemWantedCountNo = Mathf.FloorToInt(Random.Range(1, 5));
            itemWantedCount.text = itemWantedCountNo.ToString();
            itemGivenCountNo = Mathf.FloorToInt(Random.Range(1, 5));
            itemGivenCount.text = itemGivenCountNo.ToString();
            givenItem = 2;
            itemGiven.sprite = icons[2];
            RandomItem(2);
        }
        else if (rnd == 4 && max != cabbage)
        {
            itemWantedCountNo = Mathf.FloorToInt(Random.Range(1, 5));
            itemWantedCount.text = itemWantedCountNo.ToString();
            itemGivenCountNo = Mathf.FloorToInt(Random.Range(1, 5));
            itemGivenCount.text = itemGivenCountNo.ToString();
            givenItem = 3;
            itemGiven.sprite = icons[3];
            RandomItem(3);
        }
        else if (rnd == 5 && max != bean)
        {
            itemWantedCountNo = Mathf.FloorToInt(Random.Range(1, 5));
            itemWantedCount.text = itemWantedCountNo.ToString();
            itemGivenCountNo = Mathf.FloorToInt(Random.Range(1, 5));
            itemGivenCount.text = itemGivenCountNo.ToString();
            givenItem = 4;
            itemGiven.sprite = icons[4];
            RandomItem(4);
        }
        else
        {
            CheckForItemToDeal();
        }

    }
    public void UpdateItemBox()
    {
        corn = GameObject.Find("ItemBoxes/CornBox").GetComponent<ItemBoxes>().itemCount;
        meat = GameObject.Find("ItemBoxes/MeatBox").GetComponent<ItemBoxes>().itemCount;
        Chicken = GameObject.Find("ItemBoxes/ChickenBox").GetComponent<ItemBoxes>().itemCount;
        bean = GameObject.Find("ItemBoxes/BeanBox").GetComponent<ItemBoxes>().itemCount;
        cabbage = GameObject.Find("ItemBoxes/CabbageBox").GetComponent<ItemBoxes>().itemCount;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ServePoint"))
        {
            gm.costumerInLine++;
            other.gameObject.GetComponent<Transform>().position = new Vector3(other.gameObject.GetComponent<Transform>().position.x - (2 * gm.costumerInLine), other.gameObject.GetComponent<Transform>().position.y, other.gameObject.GetComponent<Transform>().position.z);
            other.gameObject.GetComponent<Transform>().position = new Vector3(other.gameObject.GetComponent<Transform>().position.x + 2, other.gameObject.GetComponent<Transform>().position.y, other.gameObject.GetComponent<Transform>().position.z);
            navMeshAgent.speed = 0;
        }
        if (other.gameObject.CompareTag("SpawnPoint") && !hasDeal)
        {
            gm.RemoveTrader(this.gameObject);
        }
    }
}
