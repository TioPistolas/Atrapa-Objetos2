using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class timmingLVL3 : MonoBehaviour
{
    public float tiempoMaximo3;

    public float timmerActual3;

    public bool tiempoActivado3;

    public TextMeshProUGUI timmerText3;

    public GameObject CanvaNextScene3;
    // Start is called before the first frame update
    void Start()
    {
        ActivarTemporizador3();
        CanvaNextScene3.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (tiempoActivado3)
        {
            timmerText3.text = Mathf.Floor(timmerActual3).ToString();
            cambiarContador3();
        }
       /* if (CanvaNextScene3.activeSelf && Player.wiimote.Button.a)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(2);
        }*/
    }

    public void cambiarContador3()
    {
        timmerActual3 -= 5 * Time.deltaTime;
        if (timmerActual3 < 0.99f)
        {
            CanvaNextScene3.SetActive(true);
            Time.timeScale = 0f;
            //Debug.log("derrota");
            cambiarTemporizador3(false);
        }
        else
        {

            Time.timeScale = 1f;

        }
    }

    public void cambiarTemporizador3(bool estado)
    {
        tiempoActivado3 = estado;
    }

    public void ActivarTemporizador3()
    {
        timmerActual3 = tiempoMaximo3;
        cambiarTemporizador3(true);
    }

    public void desactivarTemporizador3()
    {
        cambiarTemporizador3(false);
    }
}
