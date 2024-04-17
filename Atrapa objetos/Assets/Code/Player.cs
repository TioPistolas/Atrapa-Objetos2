using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WiimoteApi;

public class Player : MonoBehaviour
{
    private Wiimote wiimote;
    public float movementSpeed; //La velocidad del Player
    public float dirX;
    private bool calibrated;
    
    void Start()
    {
       //InitWiimotes();
       wiimote = MenuManager.wiimote;
       
    }

   
    void Update()
    {
        //dirX = Input.GetAxis("Horizontal");
        //transform.Translate(Vector3.right * dirX * movementSpeed);
        

        if (wiimote == null) { return; }

        int ret;
         do
	    {
            ret = wiimote.ReadWiimoteData();

            //if (Input.GetKeyDown(KeyCode.Space))
            if (!calibrated) // Calibrar presionando Barra Espaciadora
            {
                wiimote.SendDataReportMode(InputDataType.REPORT_BUTTONS_ACCEL);
                wiimote.Accel.CalibrateAccel((AccelCalibrationStep)0);
                wiimote.SetupIRCamera(IRDataType.EXTENDED);
                calibrated = true;
            }
	    } while (ret > 0);
        

        if(calibrated && GetPointingVector().x != -1f) //Mover Barra dependiendo de a donde apunta el Wiimote
        {
            /*--------------  MOVIMIENTO 1  --------------*/
            if(GetPointingVector().x <= 0.5f){ dirX = -1; }  else{ dirX = 1;}
            Vector3 movement = Vector3.right * dirX * movementSpeed;
            transform.Translate(movement);

            /*--------------  MOVIMIENTO 2  --------------*/
            //Vector3 movement = new Vector3((Mathf.Pow(GetPointingVector().x,2f) * 1f) - GetPointingVector().x,0,0);
            //transform.Translate(movement);

            print(movement);
        } 
    }



    //WIIMOTE
    void InitWiimotes() // Busca WiiMotes & Cambia LEDs
    {
	    WiimoteManager.FindWiimotes();

	    foreach(Wiimote remote in WiimoteManager.Wiimotes) {
            remote.SendPlayerLED(true, false, false, false);
	    }
    }

    void CleanWiimotes() //Desconecta Wiimotes
    {
        WiimoteManager.Cleanup(wiimote);
        wiimote = null;
    }

    private Vector3 GetAccelVector() // Rotación de Wiimote
    {
        float accel_x;
        float accel_y;
        float accel_z;

        float[] accel = wiimote.Accel.GetCalibratedAccelData();
        accel_x = accel[0];
        accel_y = -accel[2];
        accel_z = -accel[1];

        return new Vector3(accel_x, accel_y, accel_z);
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
