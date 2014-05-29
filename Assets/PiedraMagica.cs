using UnityEngine;
using System.Collections;

public class PiedraMagica : MonoBehaviour {
    //Variables de control privadas.
    private bool activo;
    private bool hayGemas;

	void Start () {
        activo = true;      //De entrada la piedra está activa.
        hayGemas = false;   //Hasta no reconocer gemas al rededor permanece en false.
	}
	
	
	void Update () {
        Debug.Log(activo);
	}

    void OnMouseDown() 
    {
        if (Brain.ESTADO == "Nada")     //Si no hay otra poción activa.
        {
            Collider[] rededor = Physics.OverlapSphere(this.gameObject.transform.position, 1);  //Busca alrededor de la Piedra Mágica todos los elementos en alcance de cruz.

            for (int i = 0; i < rededor.Length; i++)    //Busca en todos los elementos si puede girar la Piedra Mágica y si hay gemas al rededor.
            {
                if (rededor[i].gameObject.tag == "Pared")   
                    activo = false;

                if (rededor[i].gameObject.tag == "GemaQuieta")
                    hayGemas = true;
            }
            
            if (activo && hayGemas)             //En caso de que sean posibles ambas cuestiones emparenta las gemas e inicia la corutina.
            {
                for (int i = 0; i < rededor.Length; i++)
                    if (rededor[i].gameObject.tag == "GemaQuieta")
                        rededor[i].transform.parent = this.gameObject.transform;

                StartCoroutine(Yeild(rededor));
            }
        }
    }

    /******************************************************************************************/
    //                                      Yeild                                             //
    /******************************************************************************************/
    private IEnumerator Yeild(Collider[] rededor)
    {
        Brain.ESTADO = "Rotando";       //Cambia el estado para evitar sobrecarga de llamadas a corutinas.

        Collider[] estorbos = Physics.OverlapSphere(this.gameObject.transform.position, 1.5f);      //Busca los estorbos en un alcance de 1 espacio a cada lado.

        for (int i = 0; i < estorbos.Length; i++)                                                   //En caso de ser un estorbo para el giro, lo desaparece.
            if (estorbos[i].gameObject.tag == "Obstaculo" || estorbos[i].gameObject.tag == "Enemigo")
                estorbos[i].gameObject.renderer.enabled = false;
        
        
        for (int i = 0; i < 90; i++)            //Realiza el giro de 1 grado y con un retardo de la mitad de tiempo al establecido.
        {
            if (i % 2 == 0) yield return new WaitForSeconds(.01f);
            transform.Rotate(Vector3.back, 1f);
        }

        for (int i = 0; i < rededor.Length; i++)        //Desemparenta todas las gemas.
        {
            if (rededor[i].gameObject.tag == "GemaQuieta")
                rededor[i].transform.parent = null;
        }

        for (int i = 0; i < estorbos.Length; i++)       //Reaparece los estorbos.
            if (estorbos[i].gameObject.tag == "Obstaculo" || estorbos[i].gameObject.tag == "Enemigo")
                estorbos[i].gameObject.renderer.enabled = true;

        hayGemas = false;       //Quita la validación de gemas para nueva evaluación.
        Brain.ESTADO = "Nada";  //Se da por terminada la secuencia de la poción.
    }
}
