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
    public GameObject pointer;
    public Canvas[] canvases;

    private bool settingsMenu, wiimotePlus, wiimoteMinus, wiimoteChangeSpeed;
    private Vector3 movement;


    private void Start(){
        _OpenCanvas(0);
        connectText.enabled = false;

        if(wiimote == null){
            speed = 1f;
        }
        speedSlider.value = speed * 10f;
        speedText.text = "VELOCIDAD: " + speed + "x";
    }

    private void Update() {
        //In Settings Menu: Show pointer for reference & Change speed with +/-
        if(settingsMenu && wiimote != null){
            if(!pointer.activeSelf) pointer.SetActive(true);
            /*float[] pointerPos = wiimote.Ir.GetPointingPosition();
            pointer.anchorMin = new Vector2(pointerPos[0], pointerPos[1]);
            pointer.anchorMax = new Vector2(pointerPos[0], pointerPos[1]);*/

            if(GetPointingVector().x != -1f)
            {
                movement = new Vector3((GetPointingVector().x * 2f) - 1f,0,0);
            }
            pointer.transform.Translate(movement * speed);
            pointer.transform.position = new Vector3(Mathf.Clamp(pointer.transform.position.x, -8f, 8f), pointer.transform.position.y, pointer.transform.position.z);
        

            int ret;
            do
	        {
                ret = wiimote.ReadWiimoteData();

                wiimote.SendDataReportMode(InputDataType.REPORT_BUTTONS_ACCEL);
                wiimote.Accel.CalibrateAccel((AccelCalibrationStep)0);
                wiimote.SetupIRCamera(IRDataType.EXTENDED);
                wiimotePlus = wiimote.Button.plus;
                wiimoteMinus = wiimote.Button.minus;
	        } while (ret > 0);
            
            if(wiimoteChangeSpeed){
                if(wiimotePlus && !wiimoteMinus){
                    speedSlider.value += 1;
                    _ChangeSpeed();
                    wiimoteChangeSpeed = false;
                }
                else if (!wiimotePlus && wiimoteMinus){
                    speedSlider.value -= 1;
                    _ChangeSpeed();
                    wiimoteChangeSpeed = false;
                }
            }
            if (!wiimotePlus && !wiimoteMinus && !wiimoteChangeSpeed) wiimoteChangeSpeed = true;
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
        if(index == 1) settingsMenu = true; else  {settingsMenu = false; pointer.SetActive(false);};
    }

    public void _ConnectWiimote()    //Conecta el wiimote
    {
        /*foreach(Wiimote remote in WiimoteManager.Wiimotes) {
		    WiimoteManager.Cleanup(remote);
	    }*/

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

    private Vector2 GetPointingVector() //Puntero de Wiimote
    {
        float point_x;
        float point_y;

        float[] point = wiimote.Ir.GetPointingPosition();
        point_x = point[0];
        point_y = point[1];

        return new Vector2(point_x,point_y);
    }
}
