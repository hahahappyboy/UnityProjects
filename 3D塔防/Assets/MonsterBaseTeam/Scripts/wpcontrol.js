
var buttonInterface : ButtonInterface;
var weaponGroup : wpgroup;//�����飨ͨ��unity�������������Ϊ�����wpgroup.js�ű���

var weaponHand :  Transform; //ģ�������������
private var currentweapon :  Transform; //��ǰѡ�������

 function Start()
{
     //ʵ��������
     currentweapon = Instantiate(weaponGroup.weapons[0], weaponHand.position, weaponHand.rotation) as Transform;
     //Ϊ����ģ�Ͱ󶨸�ģ��Ϊ��������������֣�
 	currentweapon.parent = weaponHand;
 }
 
 function removeCurrentWeapon() 
 {
     //�Ƴ������ĸ�ģ�ͣ���ɾ������
	 currentweapon.parent = null;
	 Destroy(currentweapon.transform.gameObject);
 }
 
 function Update()
 { 
	 if (currentweapon != null)
	 {
	 	switch(buttonInterface.wpIndex)
	 	{
	 	    case "Axe01":
                //���Ƴ���ǰ��������ʵ�����µ����������������ĸ�ģ��
				removeCurrentWeapon();
				currentweapon = Instantiate(weaponGroup.weapons[0], weaponHand.position, weaponHand.rotation) as Transform;
				currentweapon.parent = weaponHand;
				break;
			case "Axe02":
				removeCurrentWeapon();
				currentweapon = Instantiate(weaponGroup.weapons[1], weaponHand.position, weaponHand.rotation) as Transform;
				currentweapon.parent = weaponHand;
				break;
			case "Sword01":
				removeCurrentWeapon();
				currentweapon = Instantiate(weaponGroup.weapons[2], weaponHand.position, weaponHand.rotation) as Transform;
				currentweapon.parent = weaponHand;
				break;
			case "Hammer01":
				removeCurrentWeapon();
				currentweapon = Instantiate(weaponGroup.weapons[3], weaponHand.position, weaponHand.rotation) as Transform;
				currentweapon.parent = weaponHand;
				break;
				
			case "Staff01":
				removeCurrentWeapon();
				currentweapon = Instantiate(weaponGroup.weapons[4], weaponHand.position, weaponHand.rotation) as Transform;
				currentweapon.parent = weaponHand;
				break;
			case "Staff02":
				removeCurrentWeapon();
				currentweapon = Instantiate(weaponGroup.weapons[5], weaponHand.position, weaponHand.rotation) as Transform;
				currentweapon.parent = weaponHand;
				break;
			case "Staff03":
				removeCurrentWeapon();
				currentweapon = Instantiate(weaponGroup.weapons[6], weaponHand.position, weaponHand.rotation) as Transform;
				currentweapon.parent = weaponHand;
				break;
			case "Staff04":
				removeCurrentWeapon();
				currentweapon = Instantiate(weaponGroup.weapons[7], weaponHand.position, weaponHand.rotation) as Transform;
				currentweapon.parent = weaponHand;
				break;
				
			case "Blunt01":
				removeCurrentWeapon();
				currentweapon = Instantiate(weaponGroup.weapons[8], weaponHand.position, weaponHand.rotation) as Transform;
				currentweapon.parent = weaponHand;
				break;
			case "Blunt02":
				removeCurrentWeapon();
				currentweapon = Instantiate(weaponGroup.weapons[9], weaponHand.position, weaponHand.rotation) as Transform;
				currentweapon.parent = weaponHand;
				break;
			case "Blunt03":
				removeCurrentWeapon();
				currentweapon = Instantiate(weaponGroup.weapons[10], weaponHand.position, weaponHand.rotation) as Transform;
				currentweapon.parent = weaponHand;
				break;
			case "Blunt04":
				removeCurrentWeapon();
				currentweapon = Instantiate(weaponGroup.weapons[11], weaponHand.position, weaponHand.rotation) as Transform;
				currentweapon.parent = weaponHand;
				break;

			case "Dagger01":
				removeCurrentWeapon();
				currentweapon = Instantiate(weaponGroup.weapons[12], weaponHand.position, weaponHand.rotation) as Transform;
				currentweapon.parent = weaponHand;
				break;
			case "Dagger02":
				removeCurrentWeapon();
				currentweapon = Instantiate(weaponGroup.weapons[13], weaponHand.position, weaponHand.rotation) as Transform;
				currentweapon.parent = weaponHand;
				break;
			case "Dagger03":
				removeCurrentWeapon();
				currentweapon = Instantiate(weaponGroup.weapons[14], weaponHand.position, weaponHand.rotation) as Transform;
				currentweapon.parent = weaponHand;
				break;
			case "Dagger04":
				removeCurrentWeapon();
				currentweapon = Instantiate(weaponGroup.weapons[15], weaponHand.position, weaponHand.rotation) as Transform;
				currentweapon.parent = weaponHand;
				break;
				
			default:
				removeCurrentWeapon();
				currentweapon = Instantiate(weaponGroup.weapons[0], weaponHand.position, weaponHand.rotation) as Transform;
				currentweapon.parent = weaponHand;
				break;
		}
	 }
	 else
	 {
         //�����ǰ������Ϊ�յģ���Ĭ�ϵ�һ������
     	currentweapon = Instantiate(weaponGroup.weapons[0], weaponHand.position, weaponHand.rotation) as Transform;
		currentweapon.parent = weaponHand;
	 }
 }
