using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemOrderList
{
    Null,
    Cabbage,
    Chicken,
    Meat,
    Corn,
    Bean
}
public class ItemList 
{
    public ItemOrderList Order1;
    public ItemOrderList Order2;
    public int Order2ItemCount;
    public ItemList(ItemOrderList order1, ItemOrderList order2,int itemCount)
    {
        Order1 = order1;
        Order2 = order2;
        Order2ItemCount = itemCount;
    }
}
