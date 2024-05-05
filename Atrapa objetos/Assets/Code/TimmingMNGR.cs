using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TimmingMNGR : MonoBehaviour
{
    public float tiempoMaximo;

    public float timmerActual;

    public bool tiempoActivado;

    public TextMeshProUGUI timmerText;

    private int scene;
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
            timmerText.text = Mathf.Floor(timmerActual).ToString();
            cambiarContador();
        }
    }

    public void cambiarContador()
    {
        timmerActual -= Time.deltaTime;
        if(timmerActual < 0.99f)
        {
            SceneManager.LoadScene(2); 
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
    }

    public void desactivarTemporizador()
    {
        cambiarTemporizador(false);
    }
}
