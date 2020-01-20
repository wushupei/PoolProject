using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnemy : MonoBehaviour
{
    public Transform enemyPrefab;
    void Start()
    {
        InvokeRepeating("Create", 1, 1);
    }
    //在一定空间范围内生成敌人
    void Create()
    {
        //计算随机生成位置
        Vector3 randPos = new Vector3(Random.Range(-10, 10f), Random.Range(0, 10f), transform.position.z);
        //如果对象池有,就从对象池拿
        if (PoolManager.Instance.enemyPool.Count > 0)
            PoolManager.Instance.enemyPool[0].GetFromPool(randPos);
        else
            Instantiate(enemyPrefab, randPos, Quaternion.identity);
    }
}
