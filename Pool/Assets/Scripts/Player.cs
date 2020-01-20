using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum FireState//左右开火状态机
{
    L,
    R
}
public class Player : MonoBehaviour
{
    FireState fs = FireState.L;
    Animator anima;
    public RectTransform aim;//准心图片
    public Transform shellPrefab;//炮弹预制体
    public Transform muzzle_L, muzzle_R;//左右枪口
    LineRenderer line;//显示瞄准方向
    void Start()
    {
        anima = GetComponent<Animator>();
        Cursor.visible = false;//隐藏鼠标光标
        line = GetComponent<LineRenderer>();
    }
    void Update()
    {
        RotateCannon();
        SetFireDir();
        if (Input.GetMouseButtonDown(0))
            ChangeFireState();
    }
    void RotateCannon()//旋转大炮
    {
        Vector3 mp = Input.mousePosition;
        //鼠标必须停留在屏幕范围内才能旋转
        if (mp.x > 0 && mp.x < Screen.width && mp.y > 0 && mp.y < Screen.height)
        {
            //获取鼠标世界坐标,并朝向该方向
            Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(mp.x, mp.y, 10));
            transform.LookAt(pos);//身体跟随鼠标世界坐标旋转
        }
    }
    void ChangeFireState()//切换左右手开炮开炮
    {
        if (fs == FireState.L)
        {
            Fire(muzzle_L);
            anima.Play("Fire_L");
            fs = FireState.R;
        }
        else if (fs == FireState.R)
        {
            Fire(muzzle_R);
            anima.Play("Fire_R");
            fs = FireState.L;
        }
    }
    void Fire(Transform muzzle)//开炮
    {
        //如果对象池有就从对象池拿
        if (PoolManager.Instance.shellPool.Count > 0)
            PoolManager.Instance.shellPool[0].GetFromPool(muzzle);
        else
            Instantiate(shellPrefab, muzzle.position, muzzle.rotation);
    }
    void SetFireDir()//显示发射方向
    {
        Vector3 ori = transform.position + transform.forward;//起点
        line.SetPosition(0, ori);//瞄准方向起点位置
        RaycastHit hit;
        if (Physics.Raycast(ori, transform.forward, out hit, 101))
        {
            //如果射线打到物体,设置瞄准方向的终点位置,显示准心
            line.SetPosition(1, hit.point);
            aim.gameObject.SetActive(true);
            aim.position = Camera.main.WorldToScreenPoint(hit.point);
        }
        else
        {
            line.SetPosition(1, ori + transform.forward * 101);
            aim.gameObject.SetActive(false);
        }
    }
}
