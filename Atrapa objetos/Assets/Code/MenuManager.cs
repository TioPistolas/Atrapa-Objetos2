using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using WiimoteApi;

public class MenuManager : MonoBehaviour
{
    public static Wiimote wiimote;
    public static float speed;

    public Text connectText, speedText;
    public Slider speedSlider;
    public Canvas[] canvases;


    private void Start(){
        _OpenCanvas(0);
        connectText.enabled = false;

        if(wiimote == null){
            speed = 1f;
        }
        speedSlider.value = speed * 10f;
        speedText.text = "VELOCIDAD: " + speed + "x";
    }




    public void _Play(){                    //Checar si hay wiimote conectado para poder Jugar
        if(wiimote != null){
            SceneManager.LoadScene(1);  
        } else {
            SetConnectionText(false);
        }
    }

    public void _OpenCanvas(int index){    //Abrir canvas
        foreach (Canvas canvas in canvases)
        {
            canvas.enabled = false;
        }
        canvases[index].enabled = true;
    }

    public void _ConnectWiimote()    //Conecta el wiimote
    {
	    WiimoteManager.FindWiimotes();
        
        if (!WiimoteManager.HasWiimote()) { return; }

        wiimote = WiimoteManager.Wiimotes[0];

	    foreach(Wiimote remote in WiimoteManager.Wiimotes) {
            remote.SendPlayerLED(true, false, false, false);
	    }
        
        SetConnectionText(true);
    }

    public void _ChangeSpeed(){
        speed = speedSlider.value/10f;
        speedText.text = "VELOCIDAD: " + speed + "x";
    }

    private void SetConnectionText(bool connected){    //Texto que muestra si mando está conectado
        connectText.enabled = true;
        if(connected){
            connectText.color = Color.green;
            connectText.text = "Mando de Wii conectado";
        } else {
            connectText.color = Color.red;
            connectText.text = "Conecta mando de Wii";
        }
    }
}
