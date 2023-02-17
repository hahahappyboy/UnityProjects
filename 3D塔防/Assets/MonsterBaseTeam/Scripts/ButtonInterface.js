

//下面的数组在unity界面中初始化
var actionButtons : GUITexture[];//动画
var actionButtonsOffset : Vector2;//按钮与按钮直接的间隔

var monsterButtons : GUITexture[];//怪物
var monsterBtttonsOffset : Vector2;//按钮与按钮直接的间隔

var colorButtons : GUITexture[];//颜色
var colorButtonsOffset : Vector2;//按钮与按钮直接的间隔

var weaponButtons : GUITexture[];//武器
var weaponButtonsOffset : Vector2;//按钮与按钮直接的间隔

private var disableOtherClicks : boolean = false;
private var disableFade : float = 0.0;

var animationComponent : Animation[];//怪物模型组件
var startAnimation : String;

var monIndex : int = 0;//当前选择的怪物
var colIndex : String;//颜色，这里在color.js中默认选择
var wpIndex : String;//武器，这里在wpcontrol.js中默认选择
var camControl : CamControl;//摄像机

private var animationName : String;//怪物动画

function Start ()
{
    //初始化怪物模型组件，并默认播放动画
    animationComponent[monIndex].Play(startAnimation);
    //设置其他模型组件处于不活动状态
	animationComponent[1].gameObject.SetActiveRecursively(false);
	animationComponent[2].gameObject.SetActiveRecursively(false);
	animationComponent[3].gameObject.SetActiveRecursively(false);
}

function Update ()
{
    //解锁点击
	var clickLock : boolean = false;
    //初始化，该值用来：判断鼠标是否使用到其他操作（比如摄像机旋转）
	disableOtherClicks = false;
	
    //鼠标左键处于释放状态时，表示鼠标没有被使用
	if(Input.GetMouseButtonUp(0))
	{
		disableOtherClicks = false;
	}
	
	// Action Buttons
	for (var i = 0; i < actionButtons.Length; i++)
	{
		actionButtons[i].pixelInset.x = actionButtons[0].pixelInset.x + (i % 2) * (actionButtonsOffset.x + actionButtons[i].pixelInset.width);
		actionButtons[i].pixelInset.y = actionButtons[0].pixelInset.y - (i / 2) * (actionButtonsOffset.y + actionButtons[i].pixelInset.height);
	    
        //点击处于解锁，摄像机未使用鼠标操作，鼠标位置在GUITexture中
		if(!clickLock && camControl.CanIClick() && actionButtons[i].HitTest(Input.mousePosition))
		{
			actionButtons[i].transform.localScale = Vector3(0.01,0.01,0);//GUITexture自我放大
			clickLock = true;//锁定点击
			disableOtherClicks =true;//鼠标已使用（即，鼠标在GUITexture中点击后移动不会影响到摄像机操作）

			if(Input.GetMouseButton(0))
			{
			    actionButtons[i].transform.localScale = Vector3(-0.01,-0.01,0);//GUITexture自我缩小
                //获得GUITexture对应的脚本（StringHold是各个GUITexure的脚本）的命名（脚本主要对GUITexture初始化标识符（命名））
			    animationName = actionButtons[i].GetComponent(StringHold).setString;
                //运行当前怪物模型的动作
				animationComponent[monIndex].CrossFade(animationName);
				
			}
		}
		else{
            //GUITexture恢复原始大小
			actionButtons[i].transform.localScale.x = 0;
			actionButtons[i].transform.localScale.y = 0;
			}
	}
	
	// Monster Buttons
	for (var j = 0; j < monsterButtons.Length; j++)
	{
		monsterButtons[j].pixelInset.x = monsterButtons[0].pixelInset.x + (j % 1) * (monsterBtttonsOffset.x + monsterButtons[j].pixelInset.width);
		monsterButtons[j].pixelInset.y = monsterButtons[0].pixelInset.y - (j / 1) * (monsterBtttonsOffset.y + monsterButtons[j].pixelInset.height);
		
		if(!clickLock && camControl.CanIClick() && monsterButtons[j].HitTest(Input.mousePosition))
		{
			monsterButtons[j].transform.localScale = Vector3(0.02,0.02,0);
			clickLock = true;
			disableOtherClicks =true;
			
			if(Input.GetMouseButton(0))
			{
				monsterButtons[j].transform.localScale = Vector3(-0.01,-0.01,0);
				animationName = monsterButtons[j].GetComponent(StringHold).setString;
				//animationComponent[monIndex].CrossFade(animationName);//这句有bug，添加这句后，模型找不到该动画
				
				switch(animationName){
					case "OrcWrrior":
						monIndex = 0;
						colIndex = "Color1";
						wpIndex = "Axe01";
						animationComponent[0].gameObject.SetActiveRecursively(true);
						animationComponent[1].gameObject.SetActiveRecursively(false);
						animationComponent[2].gameObject.SetActiveRecursively(false);
						animationComponent[3].gameObject.SetActiveRecursively(false);
						break;
						
					case "GoblinWizard":
						monIndex = 1;
						colIndex = "Color1";
						wpIndex = "Staff01";						
						animationComponent[0].gameObject.SetActiveRecursively(false);
						animationComponent[1].gameObject.SetActiveRecursively(true);
						animationComponent[2].gameObject.SetActiveRecursively(false);
						animationComponent[3].gameObject.SetActiveRecursively(false);
						break;
	
					case "OrgeHitter":
						monIndex = 2;
						colIndex = "Color1";
						wpIndex = "Blunt01";
						animationComponent[0].gameObject.SetActiveRecursively(false);
						animationComponent[1].gameObject.SetActiveRecursively(false);
						animationComponent[2].gameObject.SetActiveRecursively(true);
						animationComponent[3].gameObject.SetActiveRecursively(false);
						break;	
															
					case "TrolCurer":
						monIndex = 3;
						colIndex = "Color1";
						wpIndex = "Dagger01";
						animationComponent[0].gameObject.SetActiveRecursively(false);
						animationComponent[1].gameObject.SetActiveRecursively(false);
						animationComponent[2].gameObject.SetActiveRecursively(false);
						animationComponent[3].gameObject.SetActiveRecursively(true);
						break;
						
					default:
						monIndex = 0;
						colIndex = "Color1";
						wpIndex = "Axe01";
						animationComponent[0].gameObject.SetActiveRecursively(true);
						animationComponent[1].gameObject.SetActiveRecursively(false);
						animationComponent[2].gameObject.SetActiveRecursively(false);
						animationComponent[3].gameObject.SetActiveRecursively(false);					
						break;
				}
			}
		}
		else
		{
			monsterButtons[j].transform.localScale.x = 0;
			monsterButtons[j].transform.localScale.y = 0;
		}
	}
	// Color Buttons
	for (var k = 0; k < colorButtons.Length; k++)
	{
		colorButtons[k].pixelInset.x = colorButtons[0].pixelInset.x + (k % 1) * (colorButtonsOffset.x + colorButtons[k].pixelInset.width);
		colorButtons[k].pixelInset.y = colorButtons[0].pixelInset.y - (k / 1) * (colorButtonsOffset.y + colorButtons[k].pixelInset.height);
		
		if(!clickLock && camControl.CanIClick() && colorButtons[k].HitTest(Input.mousePosition))
		{
			colorButtons[k].transform.localScale = Vector3(0.01,0.01,0);
			clickLock = true;
			disableOtherClicks =true;

			if(Input.GetMouseButton(0))
			{
				colorButtons[k].transform.localScale = Vector3(-0.01,-0.01,0);
				colIndex = colorButtons[k].GetComponent(StringHold).setString;

			}
		}
		else
		{
			colorButtons[k].transform.localScale.x = 0;
			colorButtons[k].transform.localScale.y = 0;
		}
	}
	
	// Weapon Buttons
	for (var l = 0; l < weaponButtons.Length; l++)
	{
		weaponButtons[l].pixelInset.x = weaponButtons[0].pixelInset.x + (l % 2) * (weaponButtonsOffset.x + weaponButtons[l].pixelInset.width);
		weaponButtons[l].pixelInset.y = weaponButtons[0].pixelInset.y - (l / 2) * (weaponButtonsOffset.y + weaponButtons[l].pixelInset.height);
		
		if(!clickLock && camControl.CanIClick() && weaponButtons[l].HitTest(Input.mousePosition))
		{
			weaponButtons[l].transform.localScale = Vector3(0.01,0.01,0);
			clickLock = true;
			disableOtherClicks =true;

			if(Input.GetMouseButton(0))
			{
				weaponButtons[l].transform.localScale = Vector3(-0.01,-0.01,0);
				wpIndex = weaponButtons[l].GetComponent(StringHold).setString;
			}
		}
		else
		{
			weaponButtons[l].transform.localScale.x = 0;
			weaponButtons[l].transform.localScale.y = 0;
		}
	}	
	
}
		
function CanIClick() : boolean
{
	return !disableOtherClicks;
}