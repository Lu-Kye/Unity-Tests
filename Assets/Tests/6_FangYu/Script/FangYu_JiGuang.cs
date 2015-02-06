using UnityEngine;
using System.Collections;

public class FangYu_JiGuang : MonoBehaviour {

	public GameObject target; //来袭导弹//
	public GameObject startPostion; //激光起点//
	public GameObject boom;
	public float mul = 2f; //插值速度，与击毁导弹的destroyTime时间成正比//
	float t = 0f;
	LineRenderer line;
	void Start(){
		line = GetComponent<LineRenderer>();//用线渲染器制作激光//
	}
	
	void Update () {
		if(t >= 1f){//激光需要照射的时间，之后销毁来袭导弹和激光//
			Destroy(gameObject);
			if(target != null)
				target.GetComponent<AttackDaoDan>().DestroySelf();
		}
		
		if(target != null && startPostion != null){
			line.SetPosition(0,startPostion.transform.position);
			line.SetPosition(1,target.transform.position);
		}
		
		t += Time.deltaTime * mul;
		Color colorStart = Color.Lerp(Color.yellow,Color.red,t);
		Color colorEnd = Color.Lerp(Color.green,Color.red,t);//这里的red换成blue等可形成彩虹激光//
		line.SetColors(colorStart,colorEnd);
	}
}
