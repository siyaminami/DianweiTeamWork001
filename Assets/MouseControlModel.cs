using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControlModel : MonoBehaviour
{
    Vector3 cubeScreenPos;
    Vector3 offset;
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

        cubeScreenPos = Camera.main.WorldToScreenPoint(transform.position);

        //2. 计算偏移量
        //鼠标的三维坐标
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, cubeScreenPos.z);
        //鼠标三维坐标转为世界坐标
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        offset = transform.position - mousePos;

        //3. 物体随着鼠标移动
        while (Input.GetMouseButton(0))
        {
            //目前的鼠标二维坐标转为三维坐标
            Vector3 curMousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, cubeScreenPos.z);
            //目前的鼠标三维坐标转为世界坐标
            curMousePos = Camera.main.ScreenToWorldPoint(curMousePos);

            //物体世界位置
            transform.position = curMousePos + offset;
        }
    }
}