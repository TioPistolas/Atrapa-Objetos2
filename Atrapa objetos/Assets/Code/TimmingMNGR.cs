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

    public GameObject CanvaNextScene;
    // Start is called before the first frame update
    void Start()
    {
        ActivarTemporizador();
        CanvaNextScene.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (tiempoActivado)
        {
            timmerText.text = Mathf.Floor(timmerActual).ToString();
            cambiarContador();
        }

        /*if(CanvaNextScene.activeSelf && Player.wiimote.Button.a){
            Time.timeScale = 1f;
            SceneManager.LoadScene(2);
        }*/
        //Apague esto porque no tengo la antena de wii :c, por cierto, puse que cuando pase al siguiente nivel el timescale se pusiera en 1, esta en el timmingLVL2
    }

    public void cambiarContador()
    {
        timmerActual -= Time.deltaTime;
        if(timmerActual < 0.99f)
        {
            CanvaNextScene.SetActive(true);
            Time.timeScale = 0f;
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
