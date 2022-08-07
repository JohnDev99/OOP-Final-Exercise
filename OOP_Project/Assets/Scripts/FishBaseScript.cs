using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FishBaseScript : MonoBehaviour
{
    [SerializeField] float speed;

    //Metodo Virtual

    public virtual void Move()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
}
