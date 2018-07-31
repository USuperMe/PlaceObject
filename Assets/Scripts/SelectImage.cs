using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class SelectImage : MonoBehaviour,IPointerDownHandler{
	//需要被实例化的预制
    public GameObject inistatePrefab;
	//实例化后的对象
	private GameObject inistateObj;

    // Use this for initialization
    void Start () {
		if (inistatePrefab==null)return;
		//实例化预制
		inistateObj=Instantiate(inistatePrefab) as GameObject;
		inistateObj.SetActive(false);
	}
	//实现鼠标按下的接口
	public void OnPointerDown(PointerEventData eventData)
    {
        inistateObj.SetActive(true);

		//将当前需要被实例化的对象传递到管理器中
		SelectObjManager.Instance.AttachNewObject(inistateObj);
    }
}
