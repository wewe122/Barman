using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarManMovement : MonoBehaviour
{
    public float speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal"); // +1 if right arrow is pushed, -1 if left arrow is pushed, 0 otherwise
        
        GetComponent<SpriteRenderer>().flipX =  horizontal == 1 ? 
                                                    false : 
                                                    horizontal == -1 ? 
                                                        true : 
                                                        GetComponent<SpriteRenderer>().flipX;
        Vector3 movementVector = new Vector3(horizontal, 0, 0) * speed * Time.deltaTime;
        transform.position += movementVector;
            

    }
}
