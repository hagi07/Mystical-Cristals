using UnityEngine;
using System.Collections;

public class Brain : MonoBehaviour {
    //Variable de estado general del juego.
    public static string ESTADO = "Nada";

    //Variables para control de pociones.
    public static int teletransportacion;
    public static int rotacion;
    public static int tiempo;
    public static int unMovimiento;
    public static int aPunto;
    public static int destruye;

    //Variables de contadores.
    public static int numeroMovimiento = 0;
    public static int numeroTiempo = 0;
    
    //Variables de Pociones.
        
        //Teletransportacion.
    public static int portalesActivos;

        //Tiempo
    public static bool continuarTiempo;
    public static bool pocion1Tiempo;
    public static bool pocion2Tiempo;
    public static bool pocion3Tiempo;
    public static bool pocionActivaTiempo;

        //A Punto
    public static Vector3 punto;

    public static int portales = 0;
    public static int unidadTeletransportador = 3;
    public static int unidadRotacion = 3;
    public static bool moverGemas = true;

    //Se inicializan los valores para cada llamada de escena.
	void Start () {
        ESTADO = "Nada";
        teletransportacion = 3;
        rotacion = 3;
        tiempo = 3;
        unMovimiento = 3;
        aPunto = 3;
        destruye = 3;
        portalesActivos = 0;
        continuarTiempo = true;
        pocion1Tiempo = false;
        pocion2Tiempo = false;
        pocion3Tiempo = false;
        pocionActivaTiempo = true;
	}
	
	void Update () {
        Debug.Log("Estado del Juego : " + ESTADO);
        if (ESTADO == "Teletransportacion" || ESTADO == "Rotacion")     //En caso de llamar a estas pociones se activa la cuadricula.
            llamarCuadricula("Activar");
	}


    public void llamarCuadricula(string funcion) 
    {
        GameObject[] cuadricula = GameObject.FindGameObjectsWithTag("Cuadricula");      //Busca los objetos con tag Cuadricula.
        for (int i = 0; i < cuadricula.Length; i++)
            cuadricula[i].BroadcastMessage(funcion,SendMessageOptions.RequireReceiver);   //Los manda actiar a todos.
    }
   
}

