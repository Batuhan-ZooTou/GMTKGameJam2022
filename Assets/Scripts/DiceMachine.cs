using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceMachine : MonoBehaviour
{
    public GameManager gm;
    public int diceRoll;
    public bool canRoll;
    public bool canPickReward;
    public Transform itemDropPoint;
    ObjectPooler objectPooler;
    public GameObject RewardBox;
    public Sprite[] rolls;
    public Image screen;
    public Animator animator;
    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
    }
    public void TakeGold()
    {

        if (gm.coin>=3 && !canRoll)
        {
            animator.enabled = true;
            canRoll = true;
            canPickReward = true;
            gm.coin -= 3;
            diceRoll= Mathf.FloorToInt(Random.Range(1, 7));
            StartCoroutine(ChangeScreen());
        }
    }
    public void TakeReward(string tag)
    {
        if (canPickReward)
        {
            canPickReward = false;
            canRoll = false;
            RewardBox.GetComponent<ItemBoxes>().itemCount= diceRoll;
            if (tag=="Meat")
            {
                RewardBox.GetComponent<ItemBoxes>().item = ItemOrderList.Meat;
            }
            else if (tag == "Chicken")
            {
                RewardBox.GetComponent<ItemBoxes>().item = ItemOrderList.Chicken;
            }
            else if (tag == "Bean")
            {
                RewardBox.GetComponent<ItemBoxes>().item = ItemOrderList.Bean;
            }
            else if (tag == "Corn")
            {
                RewardBox.GetComponent<ItemBoxes>().item = ItemOrderList.Corn;
            }
            else if (tag == "Cabbage")
            {
                RewardBox.GetComponent<ItemBoxes>().item = ItemOrderList.Cabbage;
            }
            diceRoll = 0;
        }
    }
    public IEnumerator ChangeScreen()
    {
        yield return new WaitForSeconds(0.6f);
        Debug.Log("changed");
        animator.enabled = false;
        if (diceRoll==6)
        {
            screen.sprite = rolls[5];
        }
        else if (diceRoll == 5)
        {
            screen.sprite = rolls[4];
        }
        else if(diceRoll == 4)
        {
            screen.sprite = rolls[3];
        }
        else if(diceRoll == 3)
        {
            screen.sprite = rolls[2];
        }
        else if(diceRoll == 2)
        {
            screen.sprite = rolls[1];
        }
        else if(diceRoll == 1)
        {
            screen.sprite = rolls[0];
        }
    }
}
