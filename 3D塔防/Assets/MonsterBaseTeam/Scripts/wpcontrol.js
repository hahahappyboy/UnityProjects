
var buttonInterface : ButtonInterface;
var weaponGroup : wpgroup;//武器组（通过unity界面配置组件，为组件绑定wpgroup.js脚本）

var weaponHand :  Transform; //模型拿武器的组件
private var currentweapon :  Transform; //当前选择的武器

 function Start()
{
     //实例化武器
     currentweapon = Instantiate(weaponGroup.weapons[0], weaponHand.position, weaponHand.rotation) as Transform;
     //为武器模型绑定父模型为拿武器的组件（手）
 	currentweapon.parent = weaponHand;
 }
 
 function removeCurrentWeapon() 
 {
     //移除武器的父模型，并删除武器
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
                //先移除当前武器，再实例化新的武器，并绑定武器的父模型
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
         //如果当前武器是为空的，则默认第一个武器
     	currentweapon = Instantiate(weaponGroup.weapons[0], weaponHand.position, weaponHand.rotation) as Transform;
		currentweapon.parent = weaponHand;
	 }
 }
