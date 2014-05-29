#pragma strict
	public  var distance:float;
	public  var velocity:float;
	var hp : int;
	
function Start()
{

	hp = 100;
}

function Update()
{
	rigidbody.velocity = Vector3(velocity,0,0);
	
	if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
		rigidbody.velocity = Vector3(velocity,0,distance);
	if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
		rigidbody.velocity = Vector3(velocity,0,-distance);	
		
		Debug.Log(hp);
}

function OnCollisionEnter(hit : Collision)
{
	if(hit.collider.tag == "Enemigo")
	{
		hp -= 5; 
	}

}

