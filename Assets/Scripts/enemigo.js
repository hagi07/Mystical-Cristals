#pragma strict

var target : Transform; //el Objetivo 
public var moveSpeed = 15; //velocidad de movimiento 
public var rotationSpeed = 15; //Velocidad de rotación 

var myTransform : Transform;  
 

function Awake() 
{ 
    myTransform = transform;  
} 

function Start() 
{ 
    //Si deseas que siga a otro objeto que no sea el solo comenta esta linea de abajo... 
     target = GameObject.FindWithTag("personaje").transform; //target the player 

} 

function Update () { 
    //Rotacion para mirar hacia el target(objetivo a seguir) 
    myTransform.rotation = Quaternion.Slerp(myTransform.rotation,Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed*Time.deltaTime); 

    //Movimiento en dirección del target 
    myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime; 
} 

function OnMouseDown()
{
	Destroy (this.gameObject);
}

