using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WiimoteApi;

public class Player : MonoBehaviour
{
    public static Wiimote wiimote; // El control de Wii
    
    void Start()
    {
       wiimote = MenuManager.wiimote;
       GameObject.FindGameObjectWithTag("Music").GetComponent<MusicPlayer>().CheckMusic();
    }

   
    void Update()
    {
        if (wiimote == null) { return; }

        int ret;
        do
	    {
            ret = wiimote.ReadWiimoteData();

	    } while (ret > 0);
        

        //Mover Barra dependiendo de a donde apunta el Wiimote, evitando salirse de la pantalla
        float posX = (((1f - wiimote.Ir.GetPointingPosition()[1]) * 2f) - 1f) * 8f;
        if(posX != 24)  transform.position = new Vector3(Mathf.Clamp(posX, -8f, 8f), transform.position.y, transform.position.z);
    }
}
