using UnityEngine;
using System.Collections;

public class Punto : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown() 
    {
        if (Brain.ESTADO == "A Punto")          //Si se selecciona primero el punto que la gema.
        {
            Brain.ESTADO = "A Punto - Esperando Gema";      //Se cambia el estado del juego para esperar la gema que se va a mover.
            Brain.punto = this.gameObject.transform.position;       //vuelve global la posición del punto seleccionado.
        }

        if (Brain.ESTADO == "A Punto - Esperando Punto")        //Si se seleccionó primero la gema que el punto.
        {
            GameObject gemaAPunto = GameObject.FindGameObjectWithTag("GemaAPunto");     //Busca a la gema seleccionada que tiene un tag diferente
            gemaAPunto.BroadcastMessage("APunto", this.gameObject.transform.position, SendMessageOptions.RequireReceiver);  //Le manda moverse con la posición dada.
        }
    }
}
