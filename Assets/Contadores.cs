using UnityEngine;
using System.Collections;

public class Contadores : MonoBehaviour {
    //Variables de ContadorMovimientos
    public bool decenaMovimientoTag;
    public bool unidadMovimientoTag;
    private int decenaMovimiento;
    private int unidadMovimiento;
    private int cantidadMovimiento;
    private bool continuaContando;


    //Variables de ContadorTiempo
    public bool centenaTiempoTag;
    public bool decenaTiempoTag;
    public bool unidadTiempoTag;
    private int centenaTiempo;
    private int decenaTiempo;
    private int unidadTiempo;
    private int cantidadTiempo;
    public static int incremento;

    //Texturas
    public Texture2D[] textura = new Texture2D[10];

    //Pocion de Tiempo
   

    void Start()
    {
        cantidadMovimiento = 0;             //Establece en 0 los movimientos al cargar la escena.
        continuaContando = true;            //Permite un aumento en 1 del contador y no lo que dura un cuadro.
        Brain.continuarTiempo = true;
        incremento = 0;
    }

    //Muestra los contadores que están activos.
    void Update()
    {
        ContadorMovimientos();
        ContadorTiempo();
    }

    void ContadorMovimientos()
    {
        //Establece los parámetros de la decena y la unidad.
        decenaMovimiento = (cantidadMovimiento / 10);       
        unidadMovimiento = cantidadMovimiento - (decenaMovimiento * 10);

        //Hace el cambio de valor global dependiendo si es unidad o decena.
        if (decenaMovimientoTag)
            Brain.numeroMovimiento = decenaMovimiento;

        if (unidadMovimientoTag)
            Brain.numeroMovimiento = unidadMovimiento;

        //Hace el cambio de textura.
        if (decenaMovimientoTag || unidadMovimientoTag)
        {
            gameObject.renderer.material.shader = Shader.Find("Unlit/Texture");
            gameObject.renderer.material.mainTexture = textura[Brain.numeroMovimiento];
        }
        
        //Si detecta alguna gema moviendose...
        GameObject[] GemaEnMovimiento = GameObject.FindGameObjectsWithTag("GemaEnMovimiento");

        //Aumenta la variable 1 sola vez y o durante todo un cuadro.
        if (GemaEnMovimiento.Length == 1 && continuaContando == true)
        {
            cantidadMovimiento++;
            continuaContando = false;
        }
        else if (GemaEnMovimiento.Length == 0)          //En caso contrario no hace nada y continua analizando.
            continuaContando = true;
    }


    void ContadorTiempo()
    {
        
        if (Time.timeScale == 1)
        {
            if (Brain.continuarTiempo && Brain.ESTADO != "Tiempo" && Brain.pocionActivaTiempo)
                cantidadTiempo = 360 - (int)Time.timeSinceLevelLoad + incremento;    //Mantiene el conteo del tiempo pero a la inversa para hacer una cuenta regresiva.

            if (Brain.pocion1Tiempo && Brain.ESTADO == "Nada" && Brain.pocionActivaTiempo)
                cantidadTiempo = 360 - (int)Time.timeSinceLevelLoad + 10 + incremento;       //Cuenta el tiempo pero agregando el tiempo "perdido" de la primera posción de tiempo.

            if (Brain.pocion2Tiempo && Brain.ESTADO == "Nada" && Brain.pocionActivaTiempo)
                cantidadTiempo = 360 - (int)Time.timeSinceLevelLoad + 20 + incremento;       //Cuenta el tiempo pero agregando el tiempo "perdido" de la segunda posción de tiempo.

            if (Brain.pocion3Tiempo && Brain.ESTADO == "Nada" && Brain.pocionActivaTiempo)
                cantidadTiempo = 360 - (int)Time.timeSinceLevelLoad + 30 + incremento;       //Cuenta el tiempo pero agregando el tiempo "perdido" de la tercera posción de tiempo.

            //Establece los parámetros de la centena, decena y la unidad.
            centenaTiempo = cantidadTiempo / 100;
            decenaTiempo = (cantidadTiempo / 10) - (centenaTiempo * 10);
            unidadTiempo = cantidadTiempo - (decenaTiempo * 10) - (centenaTiempo * 100);

            //Hace el cambio de valor global dependiendo si es centena, decena o unidad.
            if (centenaTiempoTag)
                Brain.numeroTiempo = centenaTiempo;

            if (decenaTiempoTag)
                Brain.numeroTiempo = decenaTiempo;

            if (unidadTiempoTag)
                Brain.numeroTiempo = unidadTiempo;

            //Hace el cambio de textura.
            if (decenaTiempoTag || unidadTiempoTag || centenaTiempoTag)
            {
                gameObject.renderer.material.shader = Shader.Find("Unlit/Texture");
                if (cantidadTiempo >= 0) gameObject.renderer.material.mainTexture = textura[Brain.numeroTiempo];
                if (cantidadTiempo == 0) Brain.ESTADO = "Game Over";                //Si el tiempo termina cambia el estado del juego.
            }
        }
    }

    
}
