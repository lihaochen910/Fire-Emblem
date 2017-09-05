using UnityEngine;
using System.Collections.Generic;

namespace Map {

    public class SquareGrid : weightedGraph<Location>
    {
        public static float unit = 1f;//地图中一个格子单位的大小
        public static readonly Location[] DIRS = new[]
        {
            new Location(0,-unit),//下
            new Location(-unit,0),//左
            new Location(0,unit),//上
            new Location(unit,0)//右
        };

        public float width, height;
        public HashSet<Location> unPassable = new HashSet<Location>();//保存不可通过的地形
        public HashSet<Location> forests = new HashSet<Location>();//可通过，但是会被减速的地形
        public HashSet<Location> Enemy = new HashSet<Location>();//敌人
        public HashSet<Location> Ally = new HashSet<Location>();//友军
        public SquareGrid(float width , float height)//坐标实际值
        {
            this.width = width;
            this.height = height;
        }

        public bool InBounds(Location id)//判断位置点是否在地图范围内
        {
            //第四象限
            return 0 <= id.x && id.x < width && 0 >= id.y && id.y > height;
            //return 0 <= id.x && id.x < width && 0 <= id.y && id.y < height;
        }

        public bool Passable(Location id)//判断位置点是否可以穿过
        {
            return !unPassable.Contains(id)&&!Enemy.Contains(id);
        }
        public float Cost(Location id)
        {
            return forests.Contains(id) ? 2 : 1;
        }
        public IEnumerable<Location> Neighbors(Location id)//可迭代函数，返回邻近点位置信息
        {
            foreach(var offset in DIRS)
            {
                Location neighbor = new Location(id.x + offset.x , id.y + offset.y);
                if (InBounds(neighbor) && Passable(neighbor))
                    yield return neighbor;
            }
        }

    }//end SquareGrid def

    public interface weightedGraph<L>//加权图接口定义
    {
        float Cost(Location b);
        IEnumerable<Location> Neighbors(Location id);
    }
    public struct Location//表示位置点信息
    {
        public readonly float x, y;
        public Location(float x,float y)
        {
            this.x = x;
            this.y = y;
        }
        public Location(Vector2 v)
        {
            this.x = v.x;
            this.y = v.y;
        }
        public bool Equals(Location id)
        {
            return this.x == id.x && this.y == id.y;
        }
        public static bool operator ==(Location a, Location b)
        {
            return a.x == b.x && a.y == b.y;
        }
        public static bool operator !=(Location a, Location b)
        {
            return a.x != b.x || a.y != b.y;
        }

    }


}
