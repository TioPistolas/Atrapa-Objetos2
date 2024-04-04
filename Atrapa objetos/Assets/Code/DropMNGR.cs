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

    [SerializeField] private float minTras;
    [SerializeField] private float maxTras;

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
        while (true)
        {
            var wanted = Random.Range(minTras, maxTras);
            var position = new Vector3(wanted, transform.position.y);
            GameObject gameObject = Instantiate(aceiteCollect[Random.Range(0, aceiteCollect.Length)], position, Quaternion.identity);
            yield return new WaitForSeconds(timerDrop);
            Destroy(gameObject, 5f);
        }
        
    }
    
 
}
