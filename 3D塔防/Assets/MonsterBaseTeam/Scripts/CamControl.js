/*控制摄像机的转动（按左键移动可以移动摄像机）并且在按左键移动摄像机时使按钮不可选择*/
var buttonInterface : ButtonInterface;

var center : Vector3;
var radius : float = 3.0;

private var angle : float = 80;
private var angleSpeed : float;

var maxHeight : float = 3.0;
private var crvHeight : AnimationCurve;
var minHeight: float = 0.5;
private var crvLower : AnimationCurve;
private var heightRate : float;

private var posMove : Vector3;
private var disableOtherClicks : boolean=false;

function Start ()
{
    heightRate = (maxHeight + minHeight) * .5;
    //动画曲线（和摄像机转动有关）
	crvHeight = new AnimationCurve(Keyframe(minHeight,1), Keyframe(maxHeight - heightRate, 1), Keyframe(maxHeight, 0));
	crvLower = new AnimationCurve(Keyframe(minHeight,0), Keyframe(minHeight + heightRate, 1), Keyframe(maxHeight, 1));
}

function Update ()
{
    //动画曲线求值
	var evaHeight : float = crvHeight.Evaluate(transform.position.y);
	var evaLower : float = crvLower.Evaluate(transform.position.y);
    
    //点击鼠标左键，并且鼠标没有在按钮操作中使用
	if(Input.GetMouseButton(0) && buttonInterface.CanIClick())
	{
		disableOtherClicks = true;

		if(Input.GetAxis("Mouse Y") < 0)
		{
			posMove.y -= Input.GetAxis("Mouse Y") * evaHeight;	
		}
		else
		{
			posMove.y -= Input.GetAxis("Mouse Y") * evaLower;
		}
		
		angleSpeed -= Mathf.Abs(Mathf.Pow(Input.GetAxis("Mouse X"),1.0)) * Mathf.Sign(Input.GetAxis("Mouse X")) * 50.0;
	}
	//当释放鼠标左键时，表示鼠标没有被使用
	if(Input.GetMouseButtonUp(0))
	{
		disableOtherClicks = false;
	}
    //在Time.deltaTime*5.0的时间内，返回的数值是angleSpeed到0（可以做出摄像转动后不会立即静止，而是做减速到静止的效果）
	angleSpeed = Mathf.Lerp(angleSpeed, 0, Time.deltaTime * 5.0);
	angle += angleSpeed * Time.deltaTime;
	
	var calRadius : float = Mathf.Lerp(radius*.5, radius, evaHeight);
		
	var desiredHorizontalPosition : Vector2;
	desiredHorizontalPosition.x = Mathf.Cos(angle * Mathf.Deg2Rad) * calRadius;
	desiredHorizontalPosition.y = Mathf.Sin(angle * Mathf.Deg2Rad) * calRadius;
	
	transform.position.x = desiredHorizontalPosition.x;
	transform.position.z = desiredHorizontalPosition.y;
					
	posMove = Vector3.Lerp(posMove, Vector3.zero, Time.deltaTime * 5.0);
	
	if(posMove.y > 0 && transform.position.y > maxHeight - heightRate)
	{
		posMove.y = Mathf.Lerp(posMove.y, 0, 1 - evaHeight);
	}
	
	transform.Translate(posMove * Time.deltaTime, Space.World);
	transform.position.y = Mathf.Clamp(transform.position.y, minHeight, maxHeight);
	
	transform.LookAt(center);
}

function CanIClick() : boolean
{
	return !disableOtherClicks;
}