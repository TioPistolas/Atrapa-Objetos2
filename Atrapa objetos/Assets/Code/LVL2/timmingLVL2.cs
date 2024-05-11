using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class timmingLVL2 : MonoBehaviour
{
    public float tiempoMaximo2;

    public float timmerActual2;

    public bool tiempoActivado2;

    public TextMeshProUGUI timmerText2;

    public GameObject CanvaNextScene2;
    // Start is called before the first frame update
    void Start()
    {
        ActivarTemporizador2();
        CanvaNextScene2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (tiempoActivado2)
        {
            timmerText2.text = Mathf.Floor(timmerActual2).ToString();
            cambiarContador2();
        }
        if (CanvaNextScene2.activeSelf && Player.wiimote.Button.a)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(3);
        }
    }

    public void cambiarContador2()
    {
        timmerActual2 -= 3*Time.deltaTime;
        if (timmerActual2 < 0.99f)
        {
            CanvaNextScene2.SetActive(true);
            Time.timeScale = 0f;
            //SceneManager.LoadScene(3);
            cambiarTemporizador2(false);
        }else
        {
            
            Time.timeScale = 1f;
         
        }
    }

    public void cambiarTemporizador2(bool estado)
    {
        tiempoActivado2 = estado;
    }

    public void ActivarTemporizador2()
    {
        timmerActual2 = tiempoMaximo2;
        cambiarTemporizador2(true);
    }

    public void desactivarTemporizador2()
    {
        cambiarTemporizador2(false);
    }
}
