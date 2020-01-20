using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager
{
    private static PoolManager _Instance;
    public static PoolManager Instance
    {
        get
        {
            if (_Instance == null)
                _Instance = new PoolManager();
            return _Instance;
        }
    }
    public List<Shell> shellPool = new List<Shell>();//炮弹对象池
    private Transform _ShellPoolTF;//炮弹对象池父物体
    public Transform ShellPoolTF
    {
        get
        {
            if (_ShellPoolTF == null)
                _ShellPoolTF = new GameObject("ShellPool").transform;
            return _ShellPoolTF;
        }
    }

    public List<Enemy> enemyPool = new List<Enemy>();//敌人对象池
    private Transform _EnemyPoolTF;//敌人对象池父物体
    public Transform EnemyPoolTF
    {
        get
        {
            if (_EnemyPoolTF == null)
                _EnemyPoolTF = new GameObject("EnemyPool").transform;
            return _EnemyPoolTF;
        }
    }
}
