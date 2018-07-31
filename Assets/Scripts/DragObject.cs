using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour {
	//只针对指定的层级进行拖动
	public LayerMask _dragLayerMask;
	//指定当前要拖动的对象
	public Transform currentTransform;
	//是否可以拖动当前对象
	public bool isDrag = false;
	//用于存储当前需要拖动的对象在屏幕空间中的坐标
	Vector3 screenPos = Vector3.zero;
	//当前需要拖动对象的坐标相对于鼠标在世界空间坐标中的偏移量
	Vector3 offset = Vector3.zero;
	void Update () {

		if (Input.GetMouseButtonDown (0)) {
			//将鼠标输入点转化为一条射线
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hitinfo;
			//如果当前对象与指定的层级发生碰撞，表示当前对象可以被拖动
			if (Physics.Raycast (ray, out hitinfo, 1000f, _dragLayerMask)) {
				isDrag = true;
				//将当前需要拖动的对象赋值为射线碰撞到的对象
				currentTransform = hitinfo.transform;
				//将当前对象的世界坐标转化为屏幕坐标
				screenPos = Camera.main.WorldToScreenPoint (currentTransform.position);
				//将鼠标的屏幕坐标转换为世界空间坐标，再与当前要拖动的对象计算两者的偏移量
				offset = currentTransform.position - Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPos.z));
			} else {
				isDrag = false;
			}
		}
		if (Input.GetMouseButton (0)) {
			if (isDrag == true) {

				var currentScreenPos = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPos.z);
				//鼠标的屏幕空间坐标转化为世界坐标，并加上偏移量
				var currentPos = Camera.main.ScreenToWorldPoint (currentScreenPos) + offset;
				currentTransform.position = currentPos;
			}
		}
		if (Input.GetMouseButtonUp (0)) {
			isDrag = false;
			currentTransform = null;
		}
	}
}