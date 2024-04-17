using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using WiimoteApi;

public class MenuManager : MonoBehaviour
{
    public Text connectText;
    public static Wiimote wiimote;
    private void Start(){
        connectText.enabled = false;
    }




    void _ChangeScene(int newScene){ //Cambia Escena
        SceneManager.LoadScene(newScene);             
    }

    public void _CheckWiimote(){    //Checar si hay wiimote conectado para poder Jugar
        if(wiimote != null){
            _ChangeScene(1);
        } else {
            SetConnectionText(false);
        }
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
