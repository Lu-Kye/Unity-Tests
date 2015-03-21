using UnityEngine;
using System.Collections;
using System;

public class DrawCircle : MonoBehaviour {
	public Transform m_Transform;
	public float m_Radius = 1; // 圆环的半径
	public float m_Theta = 0.1f; // 值越低圆环越平滑
	public Color m_Color = Color.green; // 线框颜色
	
	void Start()
	{
//		m_Transform = transform;
//		if (m_Transform == null)
//		{
//			throw new Exception("Transform is NULL.");
//		}

		float theta_scale = 0.1f;             //Set lower to add more points
		float size = (2.0f * Mathf.PI) / theta_scale; //Total number of points in circle.
		
		LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
		lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
		lineRenderer.SetColors(Color.green, Color.green);
		lineRenderer.SetWidth(0.2F, 0.2F);
		lineRenderer.SetVertexCount((int)size);
		
		int i = 0;
		for(float theta = 0f; theta < 2f * Mathf.PI; theta += 0.1f) {
			var x = 10* Mathf.Cos(theta);
			var y = 10*Mathf.Sin(theta);
			
			Vector3 pos = new Vector3(x, y, 0);
			lineRenderer.SetPosition(i, pos);
			i+=1;
		}
	}

	void UpDate() 
	{
		Draw(
			transform.position,
			new Quaternion(),
			100,
			100,
			Color.green
			);
	}

	public void Draw(Vector3 origin, Quaternion rotation, float radius, int pieceCount, Color color)
	{
		if (3 > pieceCount)
		{
			return;
		}
		if (0 >= radius)
		{
			return;
		}
		
		float pieceAngle = 360.0f / pieceCount;
		
		Vector3 p0 = origin + rotation * Vector3.forward * radius;
		Vector3 p1 = p0;
		for (int i = 0; i < pieceCount-1; ++i)
		{
			var r = Quaternion.Euler(0, pieceAngle*(i+1), 0);
			Vector3 p2 = origin + rotation * (r * Vector3.forward * radius);
			Debug.DrawLine(p1, p2, color);
			
			p1 = p2;
		}
		Debug.DrawLine(p0, p1, color);
	}
	
	void OnDrawGizmos()
	{
		if (m_Transform == null) return;
		if (m_Theta < 0.0001f) m_Theta = 0.0001f;
		
		// 设置矩阵
		Matrix4x4 defaultMatrix = Gizmos.matrix;
		Gizmos.matrix = m_Transform.localToWorldMatrix;
		
		// 设置颜色
		Color defaultColor = Gizmos.color;
		Gizmos.color = m_Color;
		
		// 绘制圆环
		Vector3 beginPoint = Vector3.zero;
		Vector3 firstPoint = Vector3.zero;
		for (float theta = 0; theta < 2 * Mathf.PI; theta += m_Theta)
		{
			float x = m_Radius * Mathf.Cos(theta);
			float z = m_Radius * Mathf.Sin(theta);
			Vector3 endPoint = new Vector3(x, 0, z);
			if (theta == 0)
			{
				firstPoint = endPoint;
			}
			else
			{
				Gizmos.DrawLine(beginPoint, endPoint);
			}
			beginPoint = endPoint;
		}
		
		// 绘制最后一条线段
		Gizmos.DrawLine(firstPoint, beginPoint);
		
		// 恢复默认颜色
		Gizmos.color = defaultColor;
		
		// 恢复默认矩阵
		Gizmos.matrix = defaultMatrix;
	}
}
