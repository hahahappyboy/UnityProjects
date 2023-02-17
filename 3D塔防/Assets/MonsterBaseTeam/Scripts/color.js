#pragma strict

var buttonInterface : ButtonInterface;

var textureColor : Texture[] ;

function Start ()
{
}

function Update ()
{
	switch(buttonInterface.colIndex)
	{
		case("Color01"):
		renderer.material.mainTexture = textureColor[0];
		break;
		
		case("Color02"):
		renderer.material.mainTexture = textureColor[1];
		break;
		
		case("Color03"):
		renderer.material.mainTexture = textureColor[2];
		break;
		
		case("Color04"):
		renderer.material.mainTexture = textureColor[3];
		break;
		
		default:
		renderer.material.mainTexture = textureColor[0];
		break;
	}
}