using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropMNGR : MonoBehaviour
{
    public float timerDrop = 0.5f; //El tiempo que va a dropear cada objeto
    public GameObject[] aceiteCollect; //El objeto coleccionable
    public GameObject powerUp; //El powerUp
    public GameObject spawner; //El spawn de los objetos
    public int amountSpawners; //La cantidad de spawners que tendremos en la escena

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(aceiteSpawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator aceiteSpawn()
    {
        yield return new WaitForSeconds(timerDrop);
    }
    
 
}
