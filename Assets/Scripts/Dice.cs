using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    Rigidbody rb;
    public DiceMachine diceMachine;

    bool hasLanded;
    bool thrown;

    Vector3 initposition;

    public int diceValue;

    public diceside[] diceSides;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initposition = transform.position;
        rb.useGravity = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RollDice();
        }

        if (rb.IsSleeping() && !hasLanded && thrown)
        {
            hasLanded = true;
            rb.useGravity = false;
            SideValueCheck();

        }
        
    }

    void RollDice()
    {
        if (!thrown && !hasLanded)
        {
            thrown = true;
            rb.useGravity = true;
            rb.AddTorque(Random.Range(0, 500), Random.Range(0, 500), Random.Range(0, 500));
        }
        else if (thrown && hasLanded)
        {
            Reset();
        }
    }

    void Reset()
    {
        transform.position = initposition;
        thrown = false;
        hasLanded = false;
        rb.useGravity = false;


    }

    void RollAgain()
    {
        Reset();
        thrown = true;
        rb.useGravity = true;
        rb.AddTorque(Random.Range(0, 1000), Random.Range(500, 1000), Random.Range(0, 1000));
    }

    void SideValueCheck()
    {
        diceValue = 0;
        foreach (diceside side in diceSides)
        {
            if (side.OnGround())
            {
                diceValue = side.sideValue;
                diceMachine.diceRoll = diceValue;
                Debug.Log(diceValue);
            }
        }

    }
}
