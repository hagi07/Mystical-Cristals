#pragma strict
	public var prefab : GameObject;
	private var tiempo : int;
	private var okTiempo : boolean;
	
function Start () {
	okTiempo = true;
}

function Update () {
	
	tiempo = Time.timeSinceLevelLoad ;
	//Debug.Log(tiempo);
	
	if(tiempo % 2 != 0) okTiempo = true;
	
	if(tiempo % 2 == 0 && this.gameObject.name == "Generador1" && okTiempo)
	{
		Instantiate (prefab, this.transform.position,Quaternion.identity);
		okTiempo = false;
	}
}
