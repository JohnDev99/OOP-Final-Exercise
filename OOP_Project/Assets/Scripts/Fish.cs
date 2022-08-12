using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : FishBaseScript
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        FishMove();
    }


    public override void FishMove()
    {

        MoveLeft(speed);
        DestroyOutOfScreen();
    }

}