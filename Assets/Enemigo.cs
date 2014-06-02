using UnityEngine;
using System.Collections;

public class Enemigo : MonoBehaviour {
    public float desplazamiento;
    private string direccion;
    private bool mover;

    //Variables para RayCasting.
    private RaycastHit hit;
    public float distancia;

	void Start () {
        direccion = "Derecha";
        mover = true;
        distancia = 0.7f;
	}
	
	void Update () {
        Raycasting();

        if (Brain.ESTADO == "Nada")
            mover = true;

        if (Brain.ESTADO == "GemaEnMovimiento" && mover) 
        {
            if(direccion == "Derecha")
                this.transform.position = new Vector3(transform.position.x + desplazamiento, transform.position.y, transform.position.z);
            if (direccion == "Izquierda")
                this.transform.position = new Vector3(transform.position.x - desplazamiento, transform.position.y, transform.position.z);
            if (direccion == "Arriba")
                this.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + desplazamiento);
            if (direccion == "Abajo")
                this.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - desplazamiento);

            mover = false;
        }

        Debug.Log(direccion);
	}

    void OnMouseDown() 
    {
        if (Brain.ESTADO == "Destruyendo")
        {
            Destroy(this.gameObject);
            Brain.ESTADO = "Nada";
        }
    }

    void Raycasting()
    {
        //Dibuja los rayos en la dirección del raycast dado.
        Debug.DrawRay(transform.position, -Vector3.back, Color.green);
        Debug.DrawRay(transform.position, Vector3.back, Color.green);
        Debug.DrawRay(transform.position, Vector3.right, Color.green);
        Debug.DrawRay(transform.position, Vector3.left, Color.green);

        //Detecta las colisiones en el sentido dado y si se activa cambia el tag de la gema y modifica su dirección.
        if (Physics.Raycast(transform.position, -Vector3.back, out hit, distancia))
            if (hit.collider.tag == "Pared" || hit.collider.tag == "Obstaculo" || hit.collider.tag == "GemaQuieta")
                direccion = "Abajo";
        
        if (Physics.Raycast(transform.position, Vector3.back, out hit, distancia))
            if (hit.collider.tag == "Pared" || hit.collider.tag == "Obstaculo" || hit.collider.tag == "GemaQuieta")
                direccion = "Arriba";
        
        if (Physics.Raycast(transform.position, Vector3.left, out hit, distancia))
            if (hit.collider.tag == "Pared" || hit.collider.tag == "Obstaculo" || hit.collider.tag == "GemaQuieta")
                direccion = "Derecha";

        if (Physics.Raycast(transform.position, Vector3.right, out hit, distancia))
            if (hit.collider.tag == "Pared" || hit.collider.tag == "Obstaculo" || hit.collider.tag == "GemaQuieta")
                direccion = "Izquierda";

    }
}
