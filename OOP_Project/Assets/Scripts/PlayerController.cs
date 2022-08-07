using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 2f;
    [SerializeField] float minY;
    [SerializeField] float maxY;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerBoundries();

        if(transform.position.y > maxY)
        {
            Debug.Log("Jump");
        }
    }

    private void PlayerBoundries()
    {
        if (transform.position.y < -minY)
        {
            transform.position = new Vector3(transform.position.x, -minY, transform.position.z);
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float verticalInput = Input.GetAxisRaw("Vertical");
        transform.Translate(Vector3.up * verticalInput * speed * Time.deltaTime);
    }
}
