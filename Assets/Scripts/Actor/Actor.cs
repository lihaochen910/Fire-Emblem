using UnityEngine;
using System.Collections.Generic;

using FrameAnimeState;
using Map;


[RequireComponent(typeof(AnimeState))]
public class Actor : MonoBehaviour {

    //HP
    [ReadOnly]
    public int HP;

    //属性值
    public uint Attack_Power;
    public uint Attack_Range;//攻击范围
    public uint Defense;
    public uint Agility;//影响每次移动范围和闪避率
    public uint Magic_Power;
    public uint Luck;

    //其他值
    [ReadOnly,Range(0,100)]
    public uint Exp;

    List<Vector3> LRange = new List<Vector3>();

    AnimeState AnimationStateMachine;
    public ActorState currentState;

    void Awake()
    {
        AnimationStateMachine= GetComponent<AnimeState>();
        currentState = ActorState.Idle;
    }
    public void ShowMoveRange()//显示移动范围,斜线绘制方式
    {
        if (s_RangeManager._ins.isShowing || currentState == ActorState.AwaitOrders)
            return;
        
        Vector3 pBasePoint = new Vector3(transform.position.x,transform.position.y + Agility, transform.position.z+1);
        Vector3 TargetPoint = pBasePoint;

        for (int i = 0; i < 2*Agility+1 ; i++)
        {
            uint Cycles;//循环次数
            if (i % 2 == 0)
                Cycles = Agility + 1;
            else
                Cycles = Agility;

            for (int j = 1; j <= Cycles; j++)
            {
                if(TargetPoint!=transform.position)
                {
                    GameObject widget = getRangeObject();
                    if (s_RangeManager.currentMap.Passable(new Location(TargetPoint.x, TargetPoint.y)))
                        widget.GetComponentInChildren<SpriteRenderer>().sprite = s_RangeManager.s_Passable;
                    else widget.GetComponentInChildren<SpriteRenderer>().sprite = s_RangeManager.s_unPassable;
                    widget.SetActive(true);
                    widget.transform.position = TargetPoint;
                }
                    
                TargetPoint = new Vector3(TargetPoint.x + 1, TargetPoint.y - 1, TargetPoint.z);             
            }

            //循环结束前改变BasePoint
            if ((i + 1) % 2 == 1)//奇数，向下移动
                pBasePoint = new Vector3(pBasePoint.x, pBasePoint.y-1, pBasePoint.z);
            else pBasePoint = new Vector3(pBasePoint.x-1, pBasePoint.y, pBasePoint.z);//偶数则向左移动
            TargetPoint = pBasePoint;
        }
        s_RangeManager._ins.isShowing = true;
    }
    public void HideMoveRange()//隐藏移动范围
    {
        if (!s_RangeManager._ins.isShowing)
            return;
        GameObject[] g = GameObject.FindGameObjectsWithTag("InRange");
        for(int i = 0;i < g.Length; i++)
        {
            s_RangeManager._ins.pool.Store(g[i]);
            g[i].SetActive(false);
        }
        s_RangeManager._ins.isShowing = false;

    }

    [HideInInspector]
    public bool isMoving = false;

    List<Vector2> Lpath;
    int pathIndex = 0;
    public void MoveTo(Vector3 destnation)
    {
        isMoving = true;
        lastPosition = transform.position;
        AStarSearch SearchResult = new AStarSearch(s_RangeManager.currentMap, new Location(transform.position.x, transform.position.y), new Location(destnation.x, destnation.y));

        Lpath = new List<Vector2>();
        foreach (var waypoint in SearchResult.Path)
            Lpath.Add(new Vector2(waypoint.x,waypoint.y));

        MoveByPathIndex();
    }
    private void MoveByPathIndex()
    {
        if (pathIndex == Lpath.Count)//到达终点时结束移动
        {
            MoveEnd();
            return;
        }

        iTween.MoveTo(gameObject, iTween.Hash("x", Lpath[pathIndex].x,"y", Lpath[pathIndex].y, "easetype", iTween.EaseType.linear,"time",0.2f, "oncomplete", "MoveByPathIndex"));
        pathIndex++;

        //移动状态机处理
        if (pathIndex+1 > Lpath.Count-1)
            return;
        if (Lpath[pathIndex].x == Lpath[pathIndex + 1].x
           && Lpath[pathIndex].y < Lpath[pathIndex + 1].y)
            AnimationStateMachine.ChangeState(ActorState.Up_Move);
        else if (Lpath[pathIndex].x == Lpath[pathIndex + 1].x
           && Lpath[pathIndex].y > Lpath[pathIndex + 1].y)
            AnimationStateMachine.ChangeState(ActorState.Down_Move);
        else if (Lpath[pathIndex].y == Lpath[pathIndex + 1].y
           && Lpath[pathIndex].x < Lpath[pathIndex + 1].x)
            AnimationStateMachine.ChangeState(ActorState.Right_Move);
        else AnimationStateMachine.ChangeState(ActorState.Left_Move);
    }
    protected virtual void MoveEnd()//移动结束时
    {
        AnimationStateMachine.ChangeState(ActorState.Idle);
        HideMoveRange();
        isMoving = false;
        pathIndex = 0;
        
    }

    Vector3 lastPosition;
    public void CancelTheMove()//取消移动操作
    {
        transform.position = lastPosition;
        GameObject.FindGameObjectWithTag("Cursor").transform.position = transform.position;
    }
    public void AwaitOrders()//进入待命状态
    {
        currentState = ActorState.AwaitOrders;
        AnimationStateMachine.ChangeState(ActorState.AwaitOrders);
    }
    private GameObject getRangeObject()//从对象池拿取Range控件
    {
        #region get object from pool
        GameObject i_sRange = s_RangeManager._ins.pool.Get();
        if (!i_sRange)
        {
            GameObject g = Instantiate(s_RangeManager._ins.prefab_s_Range);
            g.transform.SetParent(s_RangeManager._ins.RangeParent.transform);
            return g;
        }
        else
        {
            //i_sRange.SetActive(true);
            s_RangeManager._ins.pool.RemoveFirstElement();
            return i_sRange;
        }
        #endregion
    }
}
