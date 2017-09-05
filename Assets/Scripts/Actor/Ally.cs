using UnityEngine;
using System.Collections.Generic;

public class Ally : Actor {


    List<Enemy> enemyList = new List<Enemy>();
    protected override void MoveEnd()
    {
        base.MoveEnd();
        //弹出命令选择框
        enemyList.Clear();
        foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (AStarSearch.Heuristic(enemy.transform.position, transform.position) <= Attack_Range)
                enemyList.Add(enemy.GetComponent<Enemy>());
        }
        if(enemyList.Count>0)
            CommandBox._ins.Show(this, Command.Attack, Command.AwaitOrders);
        else CommandBox._ins.Show(this, Command.AwaitOrders);
    }
}
