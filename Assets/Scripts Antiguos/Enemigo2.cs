using UnityEngine;
using System.Collections;

public class Enemigo2 : MonoBehaviour {
	private string direccion;
	public float espacios;
	private bool move;
	public bool izquierdaDerecha;
	public bool arribaAbajo;
	private string estado;
	private bool ok1;
	private bool ok2;
	private bool ok3;
	private bool ok4;
	private string pocion;

	private RaycastHit hit;
	public float distancia;


	void Start () {
		move = true;
		distancia = 1.29f;
		espacios = 1.236056f;
		if (izquierdaDerecha)
						direccion = "Izquierda";
				else if (arribaAbajo)
						direccion = "Arriba";
		ok1 = false;
		ok2 = false;
		ok3 = false;
		ok4 = false;
		pocion = "Nada";
	}					
	
	 void Update () {
/*		Debug.Log ("Derecha: " + ok1);
		Debug.Log ("Izquierda: " + ok2);
		Debug.Log ("Arriba: " + ok3);
		Debug.Log ("Abajo: " + ok4);
*/

		Raycasting ();
		GameObject[] validation = GameObject.FindGameObjectsWithTag ("GemaEnMovimiento");
		if (validation.Length == 1 && move == true) {
			
			if (direccion == "Izquierda" && ok1 == false) {
				this.gameObject.transform.position = new Vector3 (transform.position.x - espacios, transform.position.y, transform.position.z);
				move = false;
				
			}
			if (direccion == "Derecha" && ok2 == false) {
				this.gameObject.transform.position = new Vector3 (transform.position.x + espacios, transform.position.y, transform.position.z);
				move = false;
				
			}
			if (direccion == "Arriba" && ok3 == false) {
				this.gameObject.transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z + espacios);
				move = false;
				
			}
			if (direccion == "Abajo" && ok4 == false) {
				this.gameObject.transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z - espacios);
				move = false;										
			}
			
		} else if (validation.Length == 0) {
			if((ok1 == true && ok2 == true) || (ok1 == true && ok2 == true))
				move = false;
			else move = true;
		}

	}

	void Raycasting(){
		Debug.DrawRay(transform.position, -Vector3.back, Color.green);
		Debug.DrawRay(transform.position, Vector3.back, Color.green);
		Debug.DrawRay(transform.position, Vector3.right, Color.green);
		Debug.DrawRay(transform.position, Vector3.left, Color.green);

		
		if (Physics.Raycast (transform.position, -Vector3.back, out hit, distancia)) {
			if ((hit.collider.gameObject.tag == "Pared" || hit.collider.gameObject.tag == "GemaQuieta" || hit.collider.gameObject.tag == "Piedra") && direccion == "Arriba") {
				direccion = "Abajo";
				ok3 = true;
			}
			else ok3 = false;
		}
		
		if (Physics.Raycast (transform.position, Vector3.back, out hit, distancia)) {
			if ((hit.collider.gameObject.tag == "Pared" || hit.collider.gameObject.tag == "GemaQuieta" || hit.collider.gameObject.tag == "Piedra") && direccion == "Abajo"){ 
				direccion = "Arriba";
				ok4 = true;
			}
			else ok4 = false;
		}
		
		if (Physics.Raycast (transform.position, Vector3.left, out hit, distancia)) {
			if ((hit.collider.gameObject.tag == "Pared" || hit.collider.gameObject.tag == "GemaQuieta" || hit.collider.gameObject.tag == "Piedra") && direccion == "Izquierda"){ 
				direccion = "Derecha";
				ok2= true;
			}
			else ok2 = false;
		}
		
		if (Physics.Raycast (transform.position, Vector3.right, out hit, distancia)) {
			if ((hit.collider.gameObject.tag == "Pared" || hit.collider.gameObject.tag == "GemaQuieta" || hit.collider.gameObject.tag == "Piedra") && direccion == "Derecha"){ 
				direccion = "Izquierda";
				ok1 = true;
			}
			else ok1 = false;
		}
	}

	void Eliminar(string x){
		pocion = x;
		if (x == "Eliminar")
						this.gameObject.tag = "GemaEnMovimiento";
	}

	void OnMouseDown(){
		if (pocion == "Eliminar"){
			GameObject[] piedras = GameObject.FindGameObjectsWithTag ("GemaEnMovimiento");
			for (int i = 0; i < piedras.Length; i++){
				piedras[i].BroadcastMessage("Eliminar","Nada", SendMessageOptions.RequireReceiver);
				piedras[i].gameObject.tag = "Piedra";	
			}
			Destroy (this.gameObject);
		}
	}
	/*
	#if UNITY_EDITOR
	void OnMouseDown(){
		OnTouchDown ();
	}
	#endif*/
}
