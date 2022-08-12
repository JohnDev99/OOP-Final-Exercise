using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FishBaseScript : MonoBehaviour
{
    protected float posX = 9f;
    public int pointsToGive = 5;


    public abstract void FishMove();

    protected void DestroyOutOfScreen()
    {
        if (transform.position.x < -posX)
        {
            Destroy(gameObject);
        }
    }

    //Givesomething To PLayer
    protected void MoveLeft(float fishSpeed)
    {
        transform.Translate(Vector3.left * fishSpeed * Time.deltaTime);
    }

    

}
