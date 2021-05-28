using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControlModel : MonoBehaviour
{
    public Camera cam;//发射射线的摄像机
    private GameObject go;//射线碰撞的物体
    public static string btnName;//射线碰撞物体的名字
    private Vector3 screenSpace;
    private Vector3 offset;
    private bool isDrage = false;


    void Start()
    {
        
        //隐藏或者显示物体
        //transform.gameObject.SetActive(true);
    }

    void Update()
    {
        //鼠标右键按下，旋转物体
        if (Input.GetMouseButton(1))
        {
            float speed = 2.5f;//旋转跟随速度
            float OffsetX = Input.GetAxis("Mouse X");//获取鼠标x轴的偏移量
            float OffsetY = Input.GetAxis("Mouse Y");//获取鼠标y轴的偏移量
            transform.Rotate(new Vector3(OffsetY, -OffsetX, 0) * speed, Space.World);//旋转物体
        }

        //鼠标滚轮滚动，放大缩小
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            //范围值限定
            if (Camera.main.fieldOfView <= 100)
                Camera.main.fieldOfView += 2;
            if (Camera.main.orthographicSize <= 20)
                Camera.main.orthographicSize += 0.5F;
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            //范围值限定
            if (Camera.main.fieldOfView > 2)
                Camera.main.fieldOfView -= 2;
            if (Camera.main.orthographicSize >= 1)
                Camera.main.orthographicSize -= 0.5F;
        }

        //整体初始位置 
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        //从摄像机发出到点击坐标的射线
        RaycastHit hitInfo;
        if (isDrage == false)
        {
            if (Physics.Raycast(ray, out hitInfo))
            {
                //划出射线，只有在scene视图中才能看到
                Debug.DrawLine(ray.origin, hitInfo.point);
                go = hitInfo.collider.gameObject;
                //print(btnName);
                screenSpace = cam.WorldToScreenPoint(go.transform.position);
                offset = go.transform.position - cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
                //物体的名字  
                btnName = go.name;
                //组件的名字
            }
            else
            {
                btnName = null;
            }
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 currentScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);
            Vector3 currentPosition = cam.ScreenToWorldPoint(currentScreenSpace) + offset;
            if (btnName != null)
            {
                go.transform.position = currentPosition;
            }
            isDrage = true;
        }
        else
        {
            isDrage = false;
        }
    }
}