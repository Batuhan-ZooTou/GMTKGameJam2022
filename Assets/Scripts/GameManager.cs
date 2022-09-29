using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> costumers= new List<GameObject>();
    public List<GameObject> burritos = new List<GameObject>();
    ObjectPooler objectPooler;
    public Transform costumerSp;
    public int costumerInLine;
    public int costumerStatus = 4;
    public float spawnTimer;
    public Transform[] LinePos;
    public Transform[] BurritoStackPoint;
    public Text Status;
    public Text Money;
    public int coin=10;
    [SerializeField]private float spawnCounter;


    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
        spawnCounter = spawnTimer;

        costumers.Add(objectPooler.SpawnFromPool("Costumer", costumerSp.position, costumerSp.rotation));
        //costumers.Add(objectPooler.SpawnFromPool("Trader", costumerSp.position, costumerSp.rotation));

    }
    public void CreateNewCostumer()
    {
        int rnd = Mathf.FloorToInt(Random.Range(1, 3));
        if (rnd ==1)
        {
            GameObject tmep = objectPooler.SpawnFromPool("Costumer", costumerSp.position, costumerSp.rotation);
            costumers.Add(tmep);
        }
        else
        {
            GameObject tmep = objectPooler.SpawnFromPool("Trader", costumerSp.position, costumerSp.rotation);
            costumers.Add(tmep);
        }
    }
    private void Update()
    {
        Money.text = "Coin " + coin;
        Status.text = "Status " + costumerStatus;
        if (costumerStatus>10)
        {
            costumerStatus = 10;
        }
        UpdateCostumerLine();
        if (costumers.Count <= 3)
        {
            spawnCounter -= Time.deltaTime;
            if (spawnCounter<=0)
            {
                CreateNewCostumer();
                spawnCounter = spawnTimer;
            }
        }
    }
    public void UpdateTraderData()
    {
        foreach (GameObject trader in costumers)
        {
            if (trader.GetComponent<Trader>()!=null)
            {
                trader.GetComponent<Trader>().UpdateItemBox();
            }
        }
    }
    public void RemoveCostumer(GameObject costumer)
    {
        costumers.Remove(costumer.GetComponent<GameObject>());
        objectPooler.AddToPool(costumer);
        if (costumer.GetComponent<Costumer>().itemHolding!=null)
        {
            objectPooler.AddToPool(costumer.GetComponent<Costumer>().itemHolding);
        }
    }
    public void RemoveTrader(GameObject costumer)
    {
        objectPooler.AddToPool(costumer);
    }
    public void UpdateCostumerLine()
    {
        if (costumers.Count>0)
        {
            if (costumers[0].GetComponent<Costumer>() != false)
            {
                if (costumers[0].GetComponent<Costumer>().hasBurrito == false)
                {
                    costumers[0].GetComponent<Costumer>().inWhichLine = 1;
                    costumers[0].GetComponent<NavMeshAgent>().destination=LinePos[0].position;
                }
                else
                {
                    costumers.Remove(costumers[0]);
                }
            }
            else if (costumers[0].GetComponent<Trader>() != false)
            {
                if (costumers[0].GetComponent<Trader>().hasDeal == true)
                {
                    costumers[0].GetComponent<Trader>().inWhichLine = 1;
                    costumers[0].GetComponent<NavMeshAgent>().destination=LinePos[0].position;
                }
                else
                {
                    costumers.Remove(costumers[0]);
                }
            }
        }
        if (costumers.Count > 1)
        {
            if (costumers[1].GetComponent<Costumer>() != false)
            {
                costumers[1].GetComponent<Costumer>().inWhichLine = 2;
                    costumers[1].GetComponent<NavMeshAgent>().destination=LinePos[1].position;
            }
            else if (costumers[1].GetComponent<Trader>() != false)
            {
                costumers[1].GetComponent<Trader>().inWhichLine = 2;
                    costumers[1].GetComponent<NavMeshAgent>().destination=LinePos[1].position;
            }
        }
        if (costumers.Count > 2)
        {
            if (costumers[2].GetComponent<Costumer>() != false)
            {
                costumers[2].GetComponent<Costumer>().inWhichLine = 3;
                    costumers[2].GetComponent<NavMeshAgent>().destination=LinePos[2].position;
            }
            else if (costumers[2].GetComponent<Trader>() != false)
            {
                costumers[2].GetComponent<Trader>().inWhichLine = 3;
                    costumers[2].GetComponent<NavMeshAgent>().destination=LinePos[2].position;
            }
        }
        if (costumers.Count > 3)
        {
            if (costumers[3].GetComponent<Costumer>() != false)
            {
                costumers[3].GetComponent<Costumer>().inWhichLine = 4;
                    costumers[3].GetComponent<NavMeshAgent>().destination=LinePos[3].position;
            }
            else if (costumers[3].GetComponent<Trader>() != false)
            {
                costumers[3].GetComponent<Trader>().inWhichLine = 4;
                    costumers[3].GetComponent<NavMeshAgent>().destination=LinePos[3].position;
            }
        }
    }

}
