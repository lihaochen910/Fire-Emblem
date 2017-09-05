using System;
using System.Collections.Generic;
using UnityEngine;
using Map;

using ebickle;//import PriorityQueue

public class AStarSearch {

    public Dictionary<Location, Location> From = new Dictionary<Location, Location>();//记录寻路的最佳路径
    public Dictionary<Location, float> costSoFar = new Dictionary<Location, float>();

    public List<Vector2> Path = new List<Vector2>();//寻路结果


    public AStarSearch(SquareGrid map,Location start,Location goal)
    {

        var frontier = new PriorityQueue<Location>();//open列表
        //init
        frontier.Enqueue(start,0);
        From[start] = start;
        costSoFar[start] = 0;

        while (frontier.Count > 0)
        {
            var currentLocation = frontier.Dequeue();

            if (currentLocation == goal)
                break;

            foreach (var neighbor in map.Neighbors(currentLocation))//遍历邻近点，选择最佳位置
            {
                //计算移动成本g = 走出当前点的成本 + 走出邻近点的成本
                float newCost = costSoFar[currentLocation] + map.Cost(neighbor);//g

                if (!costSoFar.ContainsKey(neighbor) //排除已走过的位置点，类似于close列表
                    || newCost < costSoFar[neighbor])
                {
                    costSoFar[neighbor] = newCost;
                    float priority = newCost = Heuristic(neighbor, goal);//f=g+h
                    frontier.Enqueue(neighbor,priority);
                    From[neighbor] = currentLocation;
                }

            }//end search Neighbor

        }//end Dequeue

        //将寻路路径按正常顺序提取
        Location current = goal;
        Path.Add(new Vector2(current.x, current.y));
        while (current != start)
        {
            current = From[current];
            Path.Add(new Vector2(current.x, current.y));
        }
        Path.Reverse();//From<下一结点，当前结点>

    }//end Search def


    public static float Heuristic(Location a,Location b)
    {
        return Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y);
    }
    public static float Heuristic(Vector3 a,Vector3 b)//ignore Z
    {
        return Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y);
    }

}
