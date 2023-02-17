

//�����������unity�����г�ʼ��
var actionButtons : GUITexture[];//����
var actionButtonsOffset : Vector2;//��ť�밴ťֱ�ӵļ��

var monsterButtons : GUITexture[];//����
var monsterBtttonsOffset : Vector2;//��ť�밴ťֱ�ӵļ��

var colorButtons : GUITexture[];//��ɫ
var colorButtonsOffset : Vector2;//��ť�밴ťֱ�ӵļ��

var weaponButtons : GUITexture[];//����
var weaponButtonsOffset : Vector2;//��ť�밴ťֱ�ӵļ��

private var disableOtherClicks : boolean = false;
private var disableFade : float = 0.0;

var animationComponent : Animation[];//����ģ�����
var startAnimation : String;

var monIndex : int = 0;//��ǰѡ��Ĺ���
var colIndex : String;//��ɫ��������color.js��Ĭ��ѡ��
var wpIndex : String;//������������wpcontrol.js��Ĭ��ѡ��
var camControl : CamControl;//�����

private var animationName : String;//���ﶯ��

function Start ()
{
    //��ʼ������ģ���������Ĭ�ϲ��Ŷ���
    animationComponent[monIndex].Play(startAnimation);
    //��������ģ��������ڲ��״̬
	animationComponent[1].gameObject.SetActiveRecursively(false);
	animationComponent[2].gameObject.SetActiveRecursively(false);
	animationComponent[3].gameObject.SetActiveRecursively(false);
}

function Update ()
{
    //�������
	var clickLock : boolean = false;
    //��ʼ������ֵ�������ж�����Ƿ�ʹ�õ����������������������ת��
	disableOtherClicks = false;
	
    //�����������ͷ�״̬ʱ����ʾ���û�б�ʹ��
	if(Input.GetMouseButtonUp(0))
	{
		disableOtherClicks = false;
	}
	
	// Action Buttons
	for (var i = 0; i < actionButtons.Length; i++)
	{
		actionButtons[i].pixelInset.x = actionButtons[0].pixelInset.x + (i % 2) * (actionButtonsOffset.x + actionButtons[i].pixelInset.width);
		actionButtons[i].pixelInset.y = actionButtons[0].pixelInset.y - (i / 2) * (actionButtonsOffset.y + actionButtons[i].pixelInset.height);
	    
        //������ڽ����������δʹ�������������λ����GUITexture��
		if(!clickLock && camControl.CanIClick() && actionButtons[i].HitTest(Input.mousePosition))
		{
			actionButtons[i].transform.localScale = Vector3(0.01,0.01,0);//GUITexture���ҷŴ�
			clickLock = true;//�������
			disableOtherClicks =true;//�����ʹ�ã����������GUITexture�е�����ƶ�����Ӱ�쵽�����������

			if(Input.GetMouseButton(0))
			{
			    actionButtons[i].transform.localScale = Vector3(-0.01,-0.01,0);//GUITexture������С
                //���GUITexture��Ӧ�Ľű���StringHold�Ǹ���GUITexure�Ľű������������ű���Ҫ��GUITexture��ʼ����ʶ������������
			    animationName = actionButtons[i].GetComponent(StringHold).setString;
                //���е�ǰ����ģ�͵Ķ���
				animationComponent[monIndex].CrossFade(animationName);
				
			}
		}
		else{
            //GUITexture�ָ�ԭʼ��С
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
				//animationComponent[monIndex].CrossFade(animationName);//�����bug���������ģ���Ҳ����ö���
				
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