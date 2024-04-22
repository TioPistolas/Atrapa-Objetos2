using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimmingMNGR : MonoBehaviour
{
    public float tiempoMaximo;

    public float timmerActual;

    public bool tiempoActivado;

    public TextMeshProUGUI timmerText;
    // Start is called before the first frame update
    void Start()
    {
        ActivarTemporizador();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (tiempoActivado)
        {
            cambiarContador();
        }
    }

    public void cambiarContador()
    {
        timmerActual -= Time.deltaTime;
        if(timmerActual <= 0)
        {
            Debug.Log("derrota");
            cambiarTemporizador(false);
        }
    }

    public void cambiarTemporizador(bool estado)
    {
        tiempoActivado = estado;
    }
    
    public void ActivarTemporizador()
    {
        timmerActual = tiempoMaximo;
        cambiarTemporizador(true);
        timmerText.text = timmerActual.ToString();
    }

    public void desactivarTemporizador()
    {
        cambiarTemporizador(false);
    }
}
