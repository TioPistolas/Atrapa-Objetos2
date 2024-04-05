using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WiimoteApi;

public class Player : MonoBehaviour
{
    private Wiimote wiimote;
    public float movementSpeed; //La velocidad del Player
    public float dirX;
    
    void Start()
    {
       InitWiimotes();
    }

   
    void Update()
    {
        dirX = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * dirX * movementSpeed);

        if (!WiimoteManager.HasWiimote()) { return; }
        
        wiimote = WiimoteManager.Wiimotes[0];

        int ret;
         do
	    {
            ret = wiimote.ReadWiimoteData();

            //AGREGAR EL TRANSLATE AQUI (Algo así?)
            print(GetAccelVector());
            //transform.Translate(GetAccelVector().x * dirX * movementSpeed);
            
	    } while (ret > 0);
    }



    //WIIMOTE
    void InitWiimotes() // Busca WiiMotes & Cambia LEDs
    {
	    WiimoteManager.FindWiimotes();

	    foreach(Wiimote remote in WiimoteManager.Wiimotes) {
            remote.Accel.CalibrateAccel(0);
            remote.SendPlayerLED(true, false, false, false);
            remote.SetupIRCamera(IRDataType.EXTENDED);
            //remote.SendDataReportMode(InputDataType.REPORT_BUTTONS_ACCEL);
	    }
    }

    void CleanWiimotes() //Desconecta Wiimotes
    {
        WiimoteManager.Cleanup(wiimote);
        wiimote = null;
    }

    private Vector3 GetAccelVector()
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
}
