using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Salmon : FishBaseScript
{
    [SerializeField] float force;
    [SerializeField] float minPosY;
    [SerializeField] float speed;

    Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        FishMove();
        DestroyOutOfScreen();
    }
    public override void FishMove()
    {
        MoveLeft(speed);
        if (transform.position.y <= minPosY)
        {
            rb.AddForce(Vector3.up * force, ForceMode.Impulse);
        }
    }

}
