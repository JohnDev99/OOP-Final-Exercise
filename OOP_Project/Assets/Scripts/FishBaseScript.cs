using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FishBaseScript : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float posX = 9f;
    //VFX
    [SerializeField] GameObject fishBlood;
    public int pointsToGive = 5;

    public virtual void Move()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
        DestroyOutOfScreen();
    }

    private void DestroyOutOfScreen()
    {
        if (transform.position.x < -posX)
        {
            Destroy(gameObject);
        }
    }

    //Givesomething To PLayer

}
