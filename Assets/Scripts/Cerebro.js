#pragma strict
var choques:int;
var contadores:GameObject[];
private var i:int;
function Start () {
choques=0;
}

function Update () {

}

function choque ()
{
	choques+=5;
	for(i=0;i<5;i++)
	{
		contadores[i].BroadcastMessage("contador",choques,SendMessageOptions.RequireReceiver);
	}
}