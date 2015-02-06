/// <summary>
/// *** wangyel1***
/// 攻击导弹，从敌方区域发射，发射的弧度和着弹点随机,速度固定。
/// 如果碰到地面则减少玩家的总血量
/// </summary>
using UnityEngine;
using System.Collections;

public class AttackDaoDan : MonoBehaviour {
	
	public Vector3 rise = Vector3.zero; //起点位置，由Main_test脚本提供，可以像plan一样是一个区域，也可固定到发射器上//
	public Vector3 down = Vector3.zero; //目标位置，在plan区域中随机//
	public GameObject boomAttack; //爆炸粒子//
	float flyTime = 0f; //飞行时间缩放值，根据rise和down之间距离来确定，以保持飞行速度//
	Vector3 centerOff; //偏移中心//
	float t = 0f; //递增值，插值量//
	
	void Start () {
		float distance = Vector3.Distance(rise,down);
		flyTime = 1/distance * 20; 
		centerOff = new Vector3(0,Random.Range(50f,200f),0); //飞行弧线会因为这个偏移中心的不同而不同//
	}
	
	void Update () {
		t += Time.deltaTime;
		Vector3 center = (rise + down )*0.5f - centerOff;
		Vector3 riseReal = rise - center;
		Vector3 downReal = down - center;
		transform.position = Vector3.Slerp(riseReal,downReal,t * flyTime) + center;//球形插值，形成弧线//
		transform.LookAt(center,transform.up);//让导弹符合实际飞行姿势//
	}
	
	void OnTriggerEnter(Collider other) {
		if(other.CompareTag("Player")){
			Main_test.totalBlood -= 1;
			Destroy(gameObject);
			Instantiate(boomAttack,transform.position,Quaternion.identity);
		}
	}
	
	public void DestroySelf(){
		Destroy(gameObject);
		Instantiate(boomAttack,transform.position,Quaternion.identity);
	}
}
