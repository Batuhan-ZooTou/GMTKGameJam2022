using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class Costumer : MonoBehaviour
{
    public ItemOrderList meat;
    public ItemOrderList notMeat1;
    public ItemOrderList notMeat2;
    public ItemOrderList notMeat3;
    public int notMeatCount;
    public int notMeatCount1;
    public int notMeatCount2;
    public int notMeatCount3;

    [Header("UI")]
    private Camera cam;
    public Sprite[] icons;
    public Canvas bouble;

    public Canvas lvl1;
    public Canvas notmeat1;
    public Canvas notmeat2;
    public Canvas notmeat3;
    public Image meat1;
    public Image meat2;
    public Image meat3;
    public Image meat4;
    public Image item1;
    public Image item2;
    public Image item3;
    public Text Item1;
    public Text Item2;
    public Text Item3;
    public Text Item4;
    public Text Item5;
    public Text Item6;
    public int coin;
    public Text coinCount;
    [Header("OnEnable")]
    public Transform handPoint;
    public GameManager gm;
    private NavMeshAgent navMeshAgent;
    public Transform exitPoint;
    public bool hasBurrito = false;
    public GameObject itemHolding;
    public int inWhichLine;
    public float stayInLineTimer;
    [SerializeField] private float stayInLineCounter;

    private void OnEnable()
    {
        itemHolding = null;
        stayInLineCounter = stayInLineTimer;
        navMeshAgent = GetComponent<NavMeshAgent>();
        lvl1.gameObject.SetActive(false);
        notmeat1.gameObject.SetActive(false);
        notmeat2.gameObject.SetActive(false);
        notmeat3.gameObject.SetActive(false);
        notMeat1 = ItemOrderList.Null;
        notMeat2 = ItemOrderList.Null;
        notMeat3 = ItemOrderList.Null;
        notMeatCount=0;
        notMeatCount1=0;
        notMeatCount2=0;
        notMeatCount3=0;
        hasBurrito = false;
        exitPoint = GameObject.Find("GameManager/Sp").GetComponent<Transform>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        int rnd = Mathf.FloorToInt(Random.Range(1, 4));
        if(rnd == 1)
            {
            RandomBurrito(1);
        }
            else if (rnd==2)
        {
            RandomBurrito(2);
        }
        else
        {
            RandomBurrito(3);
        }
        UpdateItemImage();
    }
    private void Start()
    {
        cam = Camera.main;
    }
    private void Update()
    {
        bouble.transform.rotation = Quaternion.LookRotation(bouble.transform.position - cam.transform.position);
        lvl1.transform.rotation = Quaternion.LookRotation(lvl1.transform.position - cam.transform.position);
        notmeat1.transform.rotation = Quaternion.LookRotation(notmeat1.transform.position - cam.transform.position);
        notmeat2.transform.rotation = Quaternion.LookRotation(notmeat2.transform.position - cam.transform.position);
        notmeat3.transform.rotation = Quaternion.LookRotation(notmeat3.transform.position - cam.transform.position);
        if (inWhichLine==1)
        {
            stayInLineCounter -= Time.deltaTime;
            if (stayInLineCounter <= 0 &&!hasBurrito)
            {
                stayInLineCounter = stayInLineTimer;
                hasBurrito = true;
                gm.costumerStatus-=2;
            }
        }
        if (hasBurrito)
        {
            navMeshAgent.speed = 3.5f;
            navMeshAgent.destination = exitPoint.position;
        }
    }
    
    public void GetItem(Burrito Burrito)
    {
        int status = 0;
        if (Burrito.meat==meat && Burrito.meatCount == 1)
        {
            Debug.Log("Dogru et");
            for (int i = 0; i < 4; i++)
            {
                if ( i==0)
                {
                    if (Burrito.notMeat1 == notMeat1)
                    {
                        if (Burrito.notMeatCount1 == notMeatCount1)
                        {
                            Debug.Log("Dogru sayýda");
                            status++;
                        }
                    }
                    else if (Burrito.notMeat1 == notMeat2)
                    {
                        if (Burrito.notMeatCount1 == notMeatCount2)
                        {
                            Debug.Log("Dogru sayýda");
                            status++;
                        }
                    }
                    else if (Burrito.notMeat1 == notMeat3)
                    {
                        if (Burrito.notMeatCount1 == notMeatCount3)
                        {
                            Debug.Log("Dogru sayýda");
                            status++;
                        }
                    }
                    else if (Burrito.notMeat1 == ItemOrderList.Null)
                    {
                        status++;
                    }
                }
                if (i == 1)
                {
                    if (Burrito.notMeat2 == notMeat1)
                    {
                        if (Burrito.notMeatCount2 == notMeatCount1)
                        {
                            Debug.Log("Dogru sayýda");
                            status++;
                        }
                    }
                    else if (Burrito.notMeat2 == notMeat2)
                    {
                        if (Burrito.notMeatCount2 == notMeatCount2)
                        {
                            Debug.Log("Dogru sayýda");
                            status++;
                        }
                    }
                    else if (Burrito.notMeat2 == notMeat3)
                    {
                        if (Burrito.notMeatCount2 == notMeatCount3)
                        {
                            Debug.Log("Dogru sayýda");
                            status++;
                        }
                    }
                    else if (Burrito.notMeat2 == ItemOrderList.Null)
                    {
                        status++;
                    }
                }
                if ( i == 2)
                {
                    if (Burrito.notMeat3 == notMeat1)
                    {
                        if (Burrito.notMeatCount3 == notMeatCount1)
                        {
                            Debug.Log("Dogru sayýda");
                            status++;
                        }
                    }
                    else if (Burrito.notMeat3 == notMeat2)
                    {
                        if (Burrito.notMeatCount3 == notMeatCount2)
                        {
                            Debug.Log("Dogru sayýda");
                            status++;
                        }
                    }
                    else if (Burrito.notMeat3 == notMeat3)
                    {
                        if (Burrito.notMeatCount3 == notMeatCount3)
                        {
                            Debug.Log("Dogru sayýda");
                            status++;
                        }
                    }
                    else if (Burrito.notMeat3 == ItemOrderList.Null)
                    {
                        status++;
                    }
                }
                if (i==3)
                {
                    if (status==3)
                    {
                        gm.costumerStatus++;
                        gm.coin += coin;
                    }
                    else
                    {
                        gm.costumerStatus -= 2;
                    }
                }
            }
        }
        else
        {
            gm.costumerStatus -= 2;
        }
        hasBurrito = true;
    }
    public void TakeBurrito(GameObject item)
    {
        if (itemHolding==null)
        {
            item.transform.SetParent(transform);
            item.transform.position = transform.position;
            itemHolding = item;
        }
    }
    public ItemOrderList RandomItem(bool meat)
    {
        if (meat)
        {
            float rnd = Mathf.FloorToInt(Random.Range(1, 3));
            if (rnd == 1)
            {
                return ItemOrderList.Meat;
            }
            else if (rnd == 2)
            {
                return ItemOrderList.Chicken;
            }
            else
            {
                return ItemOrderList.Null;
            }
        }
        else if (!meat)
        {
            float rnd = Mathf.FloorToInt(Random.Range(1, 4));
            if (rnd == 1 && notMeat1!=ItemOrderList.Corn && notMeat2 != ItemOrderList.Corn&& notMeat3 != ItemOrderList.Corn)
            {
                return ItemOrderList.Corn;
            }
            else if (rnd == 2 && notMeat1 != ItemOrderList.Cabbage && notMeat2 != ItemOrderList.Cabbage && notMeat3 != ItemOrderList.Cabbage)
            {
                return ItemOrderList.Cabbage;
            }
            else if (rnd == 3 && notMeat1 != ItemOrderList.Bean && notMeat2 != ItemOrderList.Bean && notMeat3 != ItemOrderList.Bean)
            {
                return ItemOrderList.Bean;
            }
            else
            {
                return ItemOrderList.Null;
            }
        }
        else
        {
            return ItemOrderList.Null;
        }
    }
    public void UpdateItemImage()
    {
        if (meat==ItemOrderList.Meat)
        {
            meat1.sprite = icons[0];
            meat2.sprite = icons[0];
            meat3.sprite = icons[0];
            meat4.sprite = icons[0];
        }
        if (meat == ItemOrderList.Chicken)
        {
            meat1.sprite = icons[1];
            meat2.sprite = icons[1];
            meat3.sprite = icons[1];
            meat4.sprite = icons[1];
        }
        if (notMeat1 == ItemOrderList.Corn)
        {
            item1.sprite = icons[2];
            item2.sprite = icons[2];
        }
        else if (notMeat1 == ItemOrderList.Bean)
        {
            item1.sprite = icons[4];
            item2.sprite = icons[4];
        }
        else if (notMeat1 == ItemOrderList.Cabbage)
        {
            item1.sprite = icons[3];
            item2.sprite = icons[3];
        }
        if (notMeat2 == ItemOrderList.Corn)
        {
            item3.sprite = icons[2];
        }
        else if (notMeat2 == ItemOrderList.Bean)
        {
            item3.sprite = icons[4];
        }
        else if (notMeat2 == ItemOrderList.Cabbage)
        {
            item3.sprite = icons[3];
        }

    }
    public void  RandomBurrito(int lvl)
    {
        for (int i = 0; i < 2; i++)
        {
            if (i == 0)
            {
                if (lvl==1)
                {
                    coinCount.text = "3";
                    coin =3;
                }
                else
                {
                }
                meat = RandomItem(true);
            }
            else if (i == 1)
            {
                if (lvl==1)
                {
                    notMeat1 = ItemOrderList.Null;
                    notMeat2 = ItemOrderList.Null;
                    notMeat3 = ItemOrderList.Null;
                    notMeatCount = 0;
                    lvl1.gameObject.SetActive(true);
                }
                else if(lvl == 2)
                {
                    float rnd = Mathf.FloorToInt(Random.Range(1, 3));
                    coinCount.text = "4";
                    coin = 4;
                    if (rnd==1)
                    {
                        notMeat1 = RandomItem(false);
                        notMeatCount1 = Mathf.FloorToInt(Random.Range(1, 4));
                        Item1.text = notMeatCount1.ToString();
                        notmeat1.gameObject.SetActive(true);
                    }
                    else if (rnd == 2)
                    {
                        notMeat1 = RandomItem(false);
                        notMeatCount1 = Mathf.FloorToInt(Random.Range(1, 3));
                        Item2.text = notMeatCount1.ToString();
                        notMeat2 = RandomItem(false);
                        while (notMeat2 == ItemOrderList.Null)
                        {
                            notMeat2 = RandomItem(false);
                        }
                        notMeatCount2 = Mathf.FloorToInt(Random.Range(1, 3));
                        Item3.text = notMeatCount2.ToString();
                        notmeat2.gameObject.SetActive(true);
                    }
                    else if (rnd == 3)
                    {
                        notMeat1 = ItemOrderList.Bean;
                        notMeatCount1 = 1;
                        Item4.text = "1";
                        notMeat2 = ItemOrderList.Cabbage;
                        notMeatCount2 = 1;
                        Item5.text = "1";
                        notMeat3 = ItemOrderList.Corn;
                        notMeatCount3 = 1;
                        Item6.text = "1";
                        notmeat3.gameObject.SetActive(true);
                    }
                }
                else if (lvl == 3)
                {
                    float rnd = Mathf.FloorToInt(Random.Range(1, 4));
                    coinCount.text = "5";
                    coin = 5;
                    if (rnd == 1)
                    {
                        notMeat1 = RandomItem(false);
                        notMeatCount1 = Mathf.FloorToInt(Random.Range(1, 6));
                        Item1.text = notMeatCount1.ToString();
                        notmeat1.gameObject.SetActive(true);
                    }
                    else if (rnd == 2)
                    {
                        notMeat1 = RandomItem(false);
                        notMeatCount1 = Mathf.FloorToInt(Random.Range(1, 4));
                        Item2.text = notMeatCount1.ToString();
                        notMeat2 = RandomItem(false);
                        while (notMeat2 == ItemOrderList.Null)
                        {
                            notMeat2 = RandomItem(false);
                        }
                        notMeatCount2 = Mathf.FloorToInt(Random.Range(1, 4));
                        Item3.text = notMeatCount2.ToString();
                        notmeat2.gameObject.SetActive(true);
                    }
                    else if (rnd == 3)
                    {
                        notMeat1 = ItemOrderList.Bean;
                        notMeatCount1 = Mathf.FloorToInt(Random.Range(1, 3));
                        Item4.text = notMeatCount1.ToString();
                        notMeat2 = ItemOrderList.Cabbage;
                        notMeatCount2 = Mathf.FloorToInt(Random.Range(1, 3));
                        Item5.text = notMeatCount2.ToString();
                        notMeat3 = ItemOrderList.Corn;
                        notMeatCount3 = Mathf.FloorToInt(Random.Range(1, 3));
                        Item6.text = notMeatCount3.ToString();
                        notmeat3.gameObject.SetActive(true);
                    }

                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ServePoint"))
        {
            gm.costumerInLine++;
            other.gameObject.GetComponent<Transform>().position = new Vector3(other.gameObject.GetComponent<Transform>().position.x, other.gameObject.GetComponent<Transform>().position.y, other.gameObject.GetComponent<Transform>().position.z - (2 * gm.costumerInLine));
            other.gameObject.GetComponent<Transform>().position = new Vector3(other.gameObject.GetComponent<Transform>().position.x, other.gameObject.GetComponent<Transform>().position.y, other.gameObject.GetComponent<Transform>().position.z +2);
            navMeshAgent.speed = 0;

        }
        if (other.gameObject.CompareTag("SpawnPoint") && hasBurrito)
        {
            gm.RemoveCostumer(this.gameObject);
        }
    }
}
