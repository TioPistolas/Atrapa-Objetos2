using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WiimoteApi;

public class Player : MonoBehaviour
{
    private Wiimote wiimote; // El control de Wii
    public float movementSpeed; //La velocidad del Player
    private bool calibrated; //checar si el wiimote está calibrado
    private Vector3 movement; //el movimiento del jugador
    [SerializeField] private float minTras;
    [SerializeField] private float maxTras;
    
    void Start()
    {
       wiimote = MenuManager.wiimote;
       movementSpeed *= MenuManager.speed;
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

            if (!calibrated)
            {
                wiimote.SendDataReportMode(InputDataType.REPORT_BUTTONS_ACCEL);
                wiimote.Accel.CalibrateAccel((AccelCalibrationStep)0);
                wiimote.SetupIRCamera(IRDataType.EXTENDED);
                calibrated = true;
            }
	    } while (ret > 0);
        

        if(calibrated) //Mover Barra dependiendo de a donde apunta el Wiimote, evitando salirse de la pantalla
        {
            if(GetPointingVector().x != -1f)
            {
                movement = new Vector3((GetPointingVector().x * 2f) - 1f,0,0);
            }
            transform.Translate(movement * movementSpeed);
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minTras, maxTras), transform.position.y, transform.position.z);
        }
    }



    // ============ WIIMOTE ============
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
