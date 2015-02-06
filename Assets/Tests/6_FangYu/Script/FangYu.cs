/// <summary>
/// 防御导弹发射架，用一个SphereCollider触发抵近的来袭导弹
/// 导弹每发射一枚，在1秒钟后才能下次发射，所以不能无限制拦截，同理激光也需要照射时间
/// 修改已拦截的导弹的标签，以让别的发射架不必再次拦截
/// </summary>
using UnityEngine;
using System.Collections;

public class FangYu : MonoBehaviour {
	
	public GameObject fangYu;//防御导弹或激光//
	public float delayTime = 1.0f; //每发射一枚防御导弹后1秒钟后才能下次发射//
	public Transform instanPos; //实例导弹的位置//
	public Transform headLook; //发射器瞄准目标//
	bool isGo; //是否可以开火//
	GameObject targetGo; //来袭导弹//
	GameObject daoDan; //实例导弹或激光//
	float t = 1f;
	public enum DDOrJG{//不同的防御方式，导弹、激光、声波、密集阵等等，这里只做了两种//
		DaoDan,
		JiGuang
	}
	public DDOrJG change = DDOrJG.DaoDan;
	
	void Update () {
		if(!isGo){
			t -= Time.deltaTime;
			if(t <= 0f){
				t = delayTime;
				isGo = true;
			}		
		}
		if(targetGo != null)
			headLook.LookAt(targetGo.transform.position);//发射筒朝向目标//
	}
	
	void OnTriggerEnter(Collider other) {
		if(isGo && other.CompareTag("noCollider")){
			other.tag = "haveCollider"; //被拦截的导弹做一个记录，别的防御体系不再拦截//
			targetGo = other.gameObject; //获取来袭导弹//
			daoDan = Instantiate(fangYu) as GameObject; //实例化防御导弹//
			
			switch(change){
			case DDOrJG.DaoDan:
				Daodan();
				break;
			case DDOrJG.JiGuang:
				JiGuang();
				break;
			}
			
			isGo = false;
		}
	}
	
	void Daodan(){
		daoDan.transform.position = instanPos.position;
		daoDan.GetComponent<FangYu_DaoDan>().target = targetGo;//导弹的目标//

	}
	
	void JiGuang(){
		daoDan.GetComponent<FangYu_JiGuang>().target = targetGo; //激光的目标//
		daoDan.GetComponent<FangYu_JiGuang>().startPostion = instanPos.gameObject;//起点也要动态跟踪//
	}
}