using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    private void OnEnable()
    {
        //朝Y轴方向前进,2秒后消失       
        GetComponent<Rigidbody>().velocity = transform.up * 200;
        Invoke("GoBackPool", 2);
    }
    private void Start()
    {
        //方便管理,生成的炮弹都放在一个物体的子节点下
        transform.SetParent(PoolManager.Instance.ShellPoolTF);
    }
    void GoBackPool()//返回对象池
    {
        PoolManager.Instance.shellPool.Add(this);
        gameObject.SetActive(false);//禁用      
    }
    public void GetFromPool(Transform muzzle)//从对象池获取
    {
        PoolManager.Instance.shellPool.Remove(this);
        transform.SetPositionAndRotation(muzzle.position, muzzle.rotation);
        gameObject.SetActive(true);//启用              
    }
}
