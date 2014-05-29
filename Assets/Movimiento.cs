using UnityEngine;
using System.Collections;

public class Movimiento : MonoBehaviour {
    // Variables para movimiento.
    private float mouseinix;
    private float mouseiniy;
    private float mousefinx;
    private float mousefiny;
    private float mousediferencex;
    private float mousediferencey;
    private string direccion;
    public float velocity;
    
    //Variables para RayCasting.
    private RaycastHit hit;
    public float distancia;

    void FixedUpdate()
    {
        Raycasting();                                               //Genera el Raycast para la gema.
        if (gameObject.tag == "GemaQuieta")                         //En caso de que tenga el Tag de GemaQuieta se congela por completo sus movimientos.
            rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        if (direccion == "Arriba")                                  //Dependiendo de la dirección dada por OnMouseDown genera la velocidad en esa dirección.                      
            rigidbody.velocity = new Vector3(0, 0, velocity);
        if (direccion == "Abajo")
            rigidbody.velocity = new Vector3(0, 0, -velocity);
        if (direccion == "Izquierda")
            rigidbody.velocity = new Vector3(-velocity, 0, 0);
        if (direccion == "Derecha")
            rigidbody.velocity = new Vector3(velocity, 0, 0);

        if (Brain.ESTADO == "Un Movimiento")                        //En caso de activarse la poción de movimiento...
        {
            Collider[] circulo = Physics.OverlapSphere(this.gameObject.transform.position, 1.5f);   //Detecta la cuadricula a su alrededor que ya tiene el collider activado.
            
            //Les cambia el tag a los que están en el área.
            for (int i = 0; i < circulo.Length; i++)
                if (circulo[i].gameObject.tag == "Cuadricula")
                    circulo[i].gameObject.tag = "Punto";

            //Les dice que se activen los que están alrededor.
            for (int i = 0; i < circulo.Length; i++)
                if (circulo[i].gameObject.tag == "Punto")
                    circulo[i].BroadcastMessage("Activar", SendMessageOptions.DontRequireReceiver);
            
            //Descativa todos los que no sean necesarios.
            for (int i = 0; i < circulo.Length; i++)
                if (circulo[i].gameObject.tag == "Cuadricula")
                    circulo[i].BroadcastMessage("Desactivar", SendMessageOptions.RequireReceiver);
        }

    }

    /******************************************************************************************/
    //                                  MOUSE ACTIONS                                         //
    /******************************************************************************************/

    void OnMouseDown()
    {
        mouseinix = Input.mousePosition.x;
        mouseiniy = Input.mousePosition.y;
    }
    void OnMouseUp()
    {
        if (gameObject.tag == "GemaQuieta" && (Brain.ESTADO == "Nada" || Brain.ESTADO == "Tiempo"))
        {
            mousefinx = Input.mousePosition.x;
            mousefiny = Input.mousePosition.y;

            //Obtiene la diferencia del movimiento.
            mousediferencex = mousefinx - mouseinix;
            mousediferencey = mousefiny - mouseiniy;

            //Convierte la diferencia en positiva para comparar correctamente.
            if (mousediferencex < 0)
                mousediferencex = mousediferencex * -1;
            if (mousediferencey < 0)
                mousediferencey = mousediferencey * -1;

            //Si el desplazamiento fue mayor en x que en y.
            if (mousediferencex > mousediferencey)
            {
                //Y el movimiento fue positivo entonces muevete a la derecha, sino a la izquierda.
                if (mouseinix < mousefinx)
                    direccion = "Derecha";
                else
                    if (mouseinix > mousefinx)
                        direccion = "Izquierda";
                    else direccion = "Nada";
            }

            //Si el desplazamiento fue mayor en y que en x.
            else
            {
                //Y el movimiento fue positivo entonces muevete hacia arriba, sino hacia abajo.
                if (mouseiniy < mousefiny)
                    direccion = "Arriba";
                else
                    if (mouseiniy > mousefiny)
                        direccion = "Abajo";
                    else direccion = "Nada";
            }
            //if (validation() == 0 && direccion != "Nada")
            if (direccion != "Nada")
            {
                gameObject.tag = "GemaEnMovimiento";
                Brain.ESTADO = "GemaEnMovimiento";
                Time.timeScale = 3;    
                rigidbody.constraints = RigidbodyConstraints.None;
                rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            }
            else
            {
                direccion = "Nada";
            }
        }

        if (Brain.ESTADO == "A Punto")                      //Si se activa la poción y es la primera opción seleccionada.
        {
            Brain.ESTADO = "A Punto - Esperando Punto";     //Cambia el estado del juego a esperar a que se seleccione el punto final.
            this.gameObject.tag = "GemaAPunto";
        }

        if (Brain.ESTADO == "A Punto - Esperando Gema")     //Si se seleccionó primero el punto y al final la gema.
        {
            this.gameObject.transform.position = new Vector3(Brain.punto.x, this.gameObject.transform.position.y, Brain.punto.z);     //Mover la gema al punto ya seleccionado con la misma altura de la gema para no causar problemas.
            Brain.punto = new Vector3(0, 0, 0);        //Se anula la posición del vector auxiliar del cerebro.
            Brain.ESTADO = "Nada";                          //Se finaliza la poción.
        }

    }

    /******************************************************************************************/
    //                                  RAYCASTING                                            //
    /******************************************************************************************/
    void Raycasting()
    {
        //Dibuja los rayos en la dirección del raycast dado.
        Debug.DrawRay(transform.position, -Vector3.back, Color.green);
        Debug.DrawRay(transform.position, Vector3.back, Color.green);
        Debug.DrawRay(transform.position, Vector3.right, Color.green);
        Debug.DrawRay(transform.position, Vector3.left, Color.green);

        //Detecta las colisiones en el sentido dado y si se activa cambia el tag de la gema y modifica su dirección.
        if (Physics.Raycast(transform.position, -Vector3.back, out hit, distancia))
            if ((hit.collider.gameObject.name == "Enemigo" || hit.collider.gameObject.tag == "Pared" || hit.collider.gameObject.tag == "GemaQuieta" || hit.collider.gameObject.tag == "Piedra" || hit.collider.gameObject.tag == "PiedraMagica" || hit.collider.gameObject.tag == "Obstaculo") && direccion == "Arriba")
            {
                gameObject.tag = "GemaQuieta";
                Brain.ESTADO = "Nada";
                direccion = "Nada";
                Time.timeScale = 1;
                Contadores.incremento++;
            }
        if (Physics.Raycast(transform.position, Vector3.back, out hit, distancia))
            if ((hit.collider.gameObject.name == "Enemigo" || hit.collider.gameObject.tag == "Pared" || hit.collider.gameObject.tag == "GemaQuieta" || hit.collider.gameObject.tag == "Piedra" || hit.collider.gameObject.tag == "PiedraMagica" || hit.collider.gameObject.tag == "Obstaculo") && direccion == "Abajo")
            {
                gameObject.tag = "GemaQuieta";
                Brain.ESTADO = "Nada";
                direccion = "Nada";
                Time.timeScale = 1;
                Contadores.incremento++;
            }
        if (Physics.Raycast(transform.position, Vector3.left, out hit, distancia))
            if ((hit.collider.gameObject.name == "Enemigo" || hit.collider.gameObject.tag == "Pared" || hit.collider.gameObject.tag == "GemaQuieta" || hit.collider.gameObject.tag == "Piedra" || hit.collider.gameObject.tag == "PiedraMagica" || hit.collider.gameObject.tag == "Obstaculo") && direccion == "Izquierda")
            {
                gameObject.tag = "GemaQuieta";
                Brain.ESTADO = "Nada";
                direccion = "Nada";
                Time.timeScale = 1;
                Contadores.incremento++;
            }
        if (Physics.Raycast(transform.position, Vector3.right, out hit, distancia))
            if ((hit.collider.gameObject.name == "Enemigo" || hit.collider.gameObject.tag == "Pared" || hit.collider.gameObject.tag == "GemaQuieta" || hit.collider.gameObject.tag == "Piedra" || hit.collider.gameObject.tag == "PiedraMagica" || hit.collider.gameObject.tag == "Obstaculo") && direccion == "Derecha")
            {
                gameObject.tag = "GemaQuieta";
                Brain.ESTADO = "Nada";
                direccion = "Nada";
                Time.timeScale = 1;
                Contadores.incremento++;
            }
    }
    

    /****************************************/
    //              COLISIONES              //
    /****************************************/

    void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.tag == "PortalActivo")                 //Si choca con algún portal activo.
        {
            other.gameObject.tag = "Cuadricula";                    //Cambia su tag a Cuadricula para que se desaparezca solo.
            GameObject ultimoPortal = GameObject.FindGameObjectWithTag("PortalActivo");     //Encuentra el portal restante.
            this.transform.position = ultimoPortal.transform.position;      //Intercambien posiciones.
            Brain.portalesActivos = 0;                              //Reestablece valores para la siguiente vez de la posción.
            //NOTA EL ERROR PRODUCIDO EN EJECUCIÓN NO AFECTA EL JUEGO.
        }
    }


    /****************************************/
    //            UN MOVIMIENTO             //
    /****************************************/

    void UnMovimiento(Vector3 posicion)
    {
        this.gameObject.transform.position = posicion;      //Intercambia la posición con el punto dado.

        GameObject[] puntos = GameObject.FindGameObjectsWithTag("Punto");   //Busca todos los que se llamen punto para desactivarlos.
        for (int i = 0; i < puntos.Length; i++)
            if(puntos[i].tag == "Punto")
                puntos[i].BroadcastMessage("Desactivar", SendMessageOptions.DontRequireReceiver);
    }

    /****************************************/
    //                A PUNTO               //
    /****************************************/

    void APunto(Vector3 posicion)
    {             
        this.gameObject.tag = "GemaQuieta";         //Restaura el tag de la gema.
        this.gameObject.transform.position = new Vector3(posicion.x, this.gameObject.transform.position.y, posicion.z); //Hace el movimiento de la gema al punto deseado con la altura de la gema original para evitar problemas.
        Brain.ESTADO = "Nada";      //Termina la poción.
    }
}
