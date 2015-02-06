/// <summary>
/// 防御导弹，由导弹发射架发射。发射后追击来袭导弹，如果追上则同毁
/// </summary>
using UnityEngine;
using System.Collections;

public class FangYu_DaoDan : MonoBehaviour {
	
	public GameObject target; //来袭导弹//
	public Transform boomPosition;
	public float lerpTime = 0.1f; //导弹速度//
	public GameObject boomDaoDan; //爆炸后火焰//

	void Update () {
		if(target != null){
			transform.LookAt(target.transform); //跟踪导弹//
			transform.position = Vector3.Lerp (transform.position,target.transform.position,lerpTime);
		}
		else{
			Destroy(gameObject);
			Instantiate(boomDaoDan,transform.position,Quaternion.identity);
		}
	}
	void OnTriggerEnter(Collider other) {
		Destroy(other.gameObject); //摧毁来袭导弹，并自毁//
		Destroy(gameObject);
		Instantiate(boomDaoDan,boomPosition.position,Quaternion.identity);
	}
}
