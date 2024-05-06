using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SistemaRecoleccion : MonoBehaviour
{
    public int cantidadCollecionable;
    public TextMeshProUGUI score;
    public TextMeshProUGUI score2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        score.text = cantidadCollecionable.ToString();
        score2.text = cantidadCollecionable.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collector1"))
        {
            Destroy(collision.gameObject);
            cantidadCollecionable++;
        }

        if (collision.gameObject.CompareTag("CollectorBAD"))
        {
            Destroy(collision.gameObject);
            cantidadCollecionable--;
        }
    }
}
