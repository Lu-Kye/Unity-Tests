/// <summary>
/// 以一敌百，不在话下O(∩_∩)O~
/// </summary>
using UnityEngine;
using System.Collections;

public class Hero: MonoBehaviour {
	
	public GameObject fangYu;//防御导弹或激光//
	public Transform instanPos; //实例导弹的位置//
	public Transform headLook; //发射器瞄准目标//
	GameObject targetGo; //来袭导弹//
	GameObject daoDan; //实例导弹或激光//
	
	void Update () {
		if(targetGo != null)
			headLook.LookAt(targetGo.transform.position);//发射筒朝向目标//
	}
	
	void OnTriggerEnter(Collider other) {
		targetGo = other.gameObject; //获取来袭导弹//
		daoDan = Instantiate(fangYu) as GameObject; //实例化防御导弹//
		daoDan.GetComponent<FangYu_JiGuang>().target = targetGo; //激光的目标//
		daoDan.GetComponent<FangYu_JiGuang>().startPostion = instanPos.gameObject;//起点也要动态跟踪//

	}
}