/// <summary>
/// *** wangyel1 ***
/// 敌方导弹的主发射脚本，通过获取玩家区域来进行随机发射，发射间隔随时间和难度增加而增加
/// 同时控制玩家血量，当总血量低于100则游戏结束（也可单独放到玩家主脚本中）
/// </summary>
using UnityEngine;
using System.Collections;

public class Main_test : MonoBehaviour {
	
	public GameObject attackDaoDan; //进攻导弹，按一定间隔发射//
	public Transform plan; //目标区域，可根据难度不同设置不同的大小//
	public float nextAttack = 0f;
	public float delayTime = 0.5f;
	public GameObject boom; //游戏结束时防御架的爆炸//
	public int bloodTest = 10; //用于测试总血量//
	public static int totalBlood = 10; //玩家总血量//
	
	Vector3 down; //目标位置，在plan区域中随机//
	Mesh meshPlan;
	float xReal,zReal,yPosition,zOffset;
	void Start(){
		zOffset = Vector3.Distance(transform.position,plan.position);//根据落点确定距离//
		yPosition = plan.position.y;
		totalBlood = bloodTest;
		
		meshPlan = plan.GetComponent<MeshFilter>().mesh;//获取目标位置的mesh//
		float xSize = meshPlan.bounds.extents.x;
		float xScale = plan.lossyScale.x;
		xReal = xSize * xScale;//获得plan的实际大小的一半//
		
		float zSize = meshPlan.bounds.extents.z;
		float zScale = plan.lossyScale.z;
		zReal = zSize * zScale;
	}
	
	Vector3 FindArea () {
		float xPosition = plan.position.x + Random.Range(-xReal,xReal);//在x方向上的随机值//
		float zPostion = plan.position.z + Random.Range(-zReal,zReal);
		
		down = new Vector3(xPosition,yPosition,zPostion); 
		return down;
	}
	
	void Update () {
		if(Time.time > nextAttack){
			nextAttack = Time.time + delayTime;
			GameObject attack = Instantiate(attackDaoDan) as GameObject;
			attack.transform.position = transform.position;
			attack.GetComponent<AttackDaoDan>().down = FindArea();
			attack.GetComponent<AttackDaoDan>().rise = FindArea() + new Vector3(0f,0f,zOffset);
			if(totalBlood <= 0){
				GameOver();
				nextAttack = Mathf.Infinity;
			}
		}
	}
	
	void GameOver(){
		Debug.Log ("GameOver"); //游戏结束测试//
		GameObject[] fangYuJia = GameObject.FindGameObjectsWithTag("FangYuTotal");
		foreach(GameObject fangYu in fangYuJia){
			Destroy(fangYu);
			Instantiate(boom,fangYu.transform.position,Quaternion.identity);
		}
	}
}
