using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class timmingLVL3 : MonoBehaviour
{
    public float tiempoMaximo3;

    public float timmerActual3;

    public bool tiempoActivado3;

    public TextMeshProUGUI timmerText3;
    // Start is called before the first frame update
    void Start()
    {
        ActivarTemporizador3();
    }

    // Update is called once per frame
    void Update()
    {

        if (tiempoActivado3)
        {
            timmerText3.text = Mathf.Floor(timmerActual3).ToString();
            cambiarContador3();
        }
    }

    public void cambiarContador3()
    {
        timmerActual3 -= 5 * Time.deltaTime;
        if (timmerActual3 < 0.99f)
        {
            //Debug.log("derrota");
            cambiarTemporizador3(false);
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
