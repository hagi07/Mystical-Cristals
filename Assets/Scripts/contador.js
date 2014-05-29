#pragma strict
var numerotext:Texture2D[];
var soyDigito:boolean;
var soyDecena:boolean;
var soyCentena:boolean;
var soyMillar:boolean;
var soyDmillar:boolean;

var numero:int;
var digito:int;
var decena:int;
var centena:int;
var millar:int;
var decenamillar:int;

function Start () {

}


function contador (n:int)
{
	numero=n;
	decenamillar=numero/10000;
	millar=(numero/1000)-(decenamillar*10);
	centena=(numero/100)-(decenamillar*100)-(millar*10);
	decena=(numero/10)-(decenamillar*1000)-(millar*100)-(centena*10);
	digito=(numero)-(decenamillar*10000)-(millar*1000)-(centena*100)-(decena*10);
	if (soyDigito==true){
	this.gameObject.renderer.material.SetTexture("_MainTex",numerotext[digito]);
	
	}
	else if (soyDecena==true){
	this.gameObject.renderer.material.SetTexture("_MainTex",numerotext[decena]);
	}
	else if (soyCentena==true){
	this.gameObject.renderer.material.SetTexture("_MainTex",numerotext[centena]);
	}
	else if(soyMillar==true){
	this.gameObject.renderer.material.SetTexture("_MainTex",numerotext[millar]);
	}
	else if(soyDmillar==true){
	this.gameObject.renderer.material.SetTexture("_MainTex",numerotext[decenamillar]);
	}
	else{}
}
function Update () {
}