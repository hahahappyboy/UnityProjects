
var shadowHeight : float = 0.01f ;
var shadowOpacity : float = 0.6f;
var shadowSize : float = 1.0f ;
var shadowPosition : Transform ;

private var initHeight : float ;
private var currentHeight : float ;

function Start () 
{
	initHeight = shadowPosition.transform.position.y;
}

function Update () 
{
	transform.position.x = shadowPosition.transform.position.x;
	transform.position.z = shadowPosition.transform.position.z;
	transform.position.y = shadowHeight;
	
	currentHeight = shadowPosition.transform.position.y;
	var shadowRate : float = (initHeight/currentHeight);
	
	transform.localScale = Vector3(1.0f,1.0f,1.0f) * shadowSize + Vector3(1.0f,1.0f,1.0f) * (shadowRate * 0.2f);
	renderer.material.color.a = shadowOpacity + (shadowRate * 0.1f) ; 
}