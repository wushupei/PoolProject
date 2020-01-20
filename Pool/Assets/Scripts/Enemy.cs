using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp = 2;
    Rigidbody rigid;
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        //方便管理,生成的敌人都放在一个物体的子节点下
        transform.SetParent(PoolManager.Instance.EnemyPoolTF);
    }
    private void Update()
    {
        rigid.velocity = new Vector3(0, 0, -10);//向主角方向前进
        //跑到主角后方则消失
        if (transform.position.z < -5)
            GoBackPool();
    }
    private void OnCollisionExit(Collision collision)//碰撞检测
    {
        //被打扣血,血空消失
        if (--hp == 0)
            GoBackPool();
    }
    void GoBackPool()//返回对象池
    {
        PoolManager.Instance.enemyPool.Add(this);
        gameObject.SetActive(false);//禁用       
        rigid.isKinematic = true;
    }
    public void GetFromPool(Vector3 pos)//从对象池获取
    {
        PoolManager.Instance.enemyPool.Remove(this);
        transform.SetPositionAndRotation(pos, Quaternion.identity);
        gameObject.SetActive(true);//启用   
        rigid.isKinematic = false;
        hp = 2;
    }
}
