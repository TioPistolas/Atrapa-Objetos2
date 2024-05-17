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

    public Text connectText;
    public Image battery;
    public RectTransform ir_pointer;
    public GameObject caja;
    public Canvas[] canvases;

    private bool wiimoteA, settingsMenu, wiimoteChangeSpeed, calibrated;
    private Vector3 movement;


    private void Start(){
        _OpenCanvas(0);
        connectText.enabled = false;

        if(wiimote == null){
            speed = 0.5f;
        } else {
            SetConnectionText(true);
        }

        GameObject.FindGameObjectWithTag("Music").GetComponent<MusicPlayer>().CheckMusic();
    }

    private void Update() {
        //In Settings Menu: Mostrar puntero y movimiento de caja
        if(wiimote != null && settingsMenu){
            if(!caja.activeSelf) caja.SetActive(true);
            if(!ir_pointer.gameObject.activeSelf) ir_pointer.gameObject.SetActive(true);
            
            ShowPointer();
            float posX = (((1f - wiimote.Ir.GetPointingPosition()[1]) * 2f) - 1f) * 8f;
            print(posX);
            if(posX != 24)  caja.transform.position = new Vector3(Mathf.Clamp(posX, -8f, 8f), caja.transform.position.y, caja.transform.position.z);
            
            int ret;
            do
	        {
                ret = wiimote.ReadWiimoteData();
                wiimoteA = wiimote.Button.a || wiimote.Button.home || wiimote.Button.b;
	        } while (ret > 0);
            
            //CHECAR BATERIA
            wiimote.SendStatusInfoRequest();
            battery.fillAmount = wiimote.Status.battery_level / 150f;

            //REGRESAR A MENÚ
            if(wiimoteA)  {_OpenCanvas(0);}
        }
        
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
        if(index == 1) settingsMenu = true; else {settingsMenu = false; caja.SetActive(false); ir_pointer.gameObject.SetActive(false);}
    }

    public void _ConnectWiimote()    //Conecta el wiimote
    {
        WiimoteManager.Wiimotes.Clear();
        wiimote = null;
	    WiimoteManager.FindWiimotes();
        
        if (!WiimoteManager.HasWiimote()) { return; }

        wiimote = WiimoteManager.Wiimotes[0];

	    foreach(Wiimote remote in WiimoteManager.Wiimotes) {
            remote.SendPlayerLED(true, false, false, false);
            remote.SendDataReportMode(InputDataType.REPORT_BUTTONS_ACCEL);
            remote.Accel.CalibrateAccel((AccelCalibrationStep)0);
            remote.SetupIRCamera(IRDataType.BASIC);
	    }
        SetConnectionText(true);
    }

    public void _ExitGame(){
        WiimoteManager.Wiimotes.Clear();
        wiimote = null;
        Application.Quit();
    }

    private void SetConnectionText(bool connected){    //Texto que muestra si mando está conectado
        connectText.enabled = true;
        if(connected){
            connectText.color = Color.green;
            connectText.text = "Castrol Conectado";
        } else {
            connectText.color = Color.red;
            connectText.text = "Coencte Castrol";
        }
    }

    private void ShowPointer(){
        float[] pointer = wiimote.Ir.GetPointingPosition();
        ir_pointer.anchorMin = new Vector2(1f - pointer[1],pointer[0]);
        ir_pointer.anchorMax = new Vector2(1f - pointer[1],pointer[0]);
    }
}
