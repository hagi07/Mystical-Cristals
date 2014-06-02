using UnityEngine;
using System.Collections;

public class ClickButton : MonoBehaviour {
    public bool teletransportacion;
    public bool rotacion;
    public bool tiempo;
    public bool unMovimiento;
    public bool aPunto;
    public bool destruye;

    //Variable auxiliar de tiempo.
    private bool okContinua;

    void Start() 
    {
        okContinua = true;
    }

    void OnMouseDown()
    {
        if (teletransportacion && Brain.teletransportacion >= 1 && Brain.ESTADO == "Nada" && Brain.portalesActivos == 0)        //Si existen pociones en el baúl, no hay poción activa y no hay portales activos, ejecuta la poción.
        { 
            Brain.teletransportacion--;
            Brain.ESTADO = "Teletransportacion";
        }
        if (rotacion && Brain.rotacion >= 1 && Brain.ESTADO == "Nada")  //Si hay pociones en el baúl y no hay poción activa, acciona la poción.
        { 
            Brain.rotacion--;
            Brain.ESTADO = "Rotacion";
        }

        if (tiempo && Brain.tiempo >= 1 && okContinua && Brain.ESTADO == "Nada")  //Permite que se accione la poción sólo sino no está ya activada (okcontinua).
        {
            Brain.tiempo--;
            Brain.ESTADO = "Tiempo";
            StartCoroutine(Yeild());
        }

        if (unMovimiento && Brain.unMovimiento >= 1 && Brain.ESTADO == "Nada") 
        {
            Brain.unMovimiento--;
            Brain.ESTADO = "Un Movimiento";
            GameObject[] cuadricula = GameObject.FindGameObjectsWithTag("Cuadricula");
            for (int i = 0; i < cuadricula.Length; i++)
                cuadricula[i].BroadcastMessage("ActivarCollider", SendMessageOptions.RequireReceiver);
        }
        if (aPunto && Brain.aPunto >= 1 && Brain.ESTADO == "Nada")
        {
            Brain.aPunto--;
            Brain.ESTADO = "A Punto";
        }
        if (destruye && Brain.destruye >= 1 && Brain.ESTADO == "Nada")
        {
            Brain.destruye--;
            Brain.ESTADO = "Destruyendo";
        }
    }

    private IEnumerator Yeild()
    {
        okContinua = false;     //Bloquea una segunda activación de la poción.
        Brain.pocionActivaTiempo = false;

        yield return new WaitForSeconds(10);    //Detiene el reloj por 10 segundos.
        
        if (Brain.continuarTiempo && Brain.ESTADO == "Tiempo")  //Si es la primera vez que se usa la poción intercambia el valor para que el conteo de segundos se mantenga.
        {
            Brain.continuarTiempo = false;
            Brain.pocion1Tiempo = true;
            Brain.ESTADO = "Nada";
        }
        if (Brain.pocion1Tiempo && Brain.ESTADO == "Tiempo")    //Si es la segunda vez que se usa la poción intercambia el valor para que el conteo de segundos se mantenga.
        {
            Brain.pocion1Tiempo = false;
            Brain.pocion2Tiempo = true;
            Brain.ESTADO = "Nada";
        }
        if (Brain.pocion2Tiempo && Brain.ESTADO == "Tiempo")    //Si es la tercera vez que se usa la poción intercambia el valor para que el conteo de segundos se mantenga.
        {
            Brain.pocion2Tiempo = false;
            Brain.pocion3Tiempo = true;
            Brain.ESTADO = "Nada";
        }
        okContinua = true;                  //Habilita el uso de la poción de nuevo.
        Brain.pocionActivaTiempo = true;   //Habilita el uso de la poción de nuevo.
    }
}
