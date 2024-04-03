using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float movementSpeed; //La velocidad del Player
    public float dirX;
    
    void Start()
    {
       
    }

   
    void Update()
    {
        dirX = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * dirX * movementSpeed);

    }
}
