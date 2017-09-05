using UnityEngine;
using System.Collections;
using FrameAnimeState;

public class CursorController : MonoBehaviour {


    public float unit = float.NaN;
    public float CursorMoveTimeInterval = 0.1f;

    float h, v;
    bool isMoving = false;

    s_RangeManager rManager;
    void Start()
    {
        rManager = s_RangeManager._ins;
    }

    RaycastHit2D hit;
    Actor currentActor;
    void FixedUpdate()
    {
        if (CommandBox._ins.active)
            return;
        #region Move
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        if (h == 1 && !isMoving)
        {
            isMoving = true;
            iTween.MoveTo(gameObject, iTween.Hash("x", transform.position.x + unit,
                "time", CursorMoveTimeInterval, "easytype", "linear", "oncomplete", "MovingEnd"));
        }
        if (h == -1 && !isMoving)
        {
            isMoving = true;
            iTween.MoveTo(gameObject, iTween.Hash("x", transform.position.x - unit, "time", CursorMoveTimeInterval, "easytype", "linear", "oncomplete", "MovingEnd"));
        }
        if (v == 1 && !isMoving)
        {
            isMoving = true;
            iTween.MoveTo(gameObject, iTween.Hash("y", transform.position.y + unit, "time", CursorMoveTimeInterval, "easytype", "linear", "oncomplete", "MovingEnd"));
        }

        if (v == -1 && !isMoving)
        {
            isMoving = true;
            iTween.MoveTo(gameObject, iTween.Hash("y", transform.position.y - unit, "time", CursorMoveTimeInterval, "easytype", "linear", "oncomplete", "MovingEnd"));
        }
        #endregion
    }//处理移动
    void Update ()//处理当前range控件显示时的按键逻辑
    {
        if (!rManager.isShowing || CommandBox._ins.active)
            return;
        
        if (Input.GetKeyDown(KeyCode.J))//Select
        {
            Ray ray = new Ray(transform.position, new Vector3(0, 0, 1));
            hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit)
            {
                //print(hit.transform.name);
                if(hit.transform.tag == "InRange" && currentActor.tag != "Enemy")
                {
                    if (currentActor.isMoving)
                        return;

                    currentActor.MoveTo(hit.transform.position);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.K))//Hide
        {
            print("取消移动，光标恢复到"+currentActor.name +currentActor.transform.position);

            isMoving = true;
            iTween.MoveTo(gameObject, iTween.Hash("x", currentActor.transform.position.x,"y", currentActor.transform.position.y, "time", CursorMoveTimeInterval, "easetype", "linear", "oncomplete", "MovingEnd"));

            currentActor.HideMoveRange();
            rManager.isShowing = false;
        }


    }
    void LateUpdate()
    {
        
        if (rManager.isShowing || CommandBox._ins.active)
            return;
        //通常状态下按键反应
        #region Control
        if (Input.GetKeyDown(KeyCode.J))//Select
        {
            Ray ray = new Ray(transform.position, new Vector3(0, 0, 1));
            hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit)
            {
                print(hit.transform.name);
                currentActor = hit.transform.gameObject.GetComponent<Actor>();
                if ((currentActor.transform.tag == "Ally" || currentActor.transform.tag == "Leader" || currentActor.transform.tag == "Enemy") && currentActor.currentState != ActorState.AwaitOrders)
                    currentActor.ShowMoveRange();
            }
            else CommandBox._ins.Show(null, Command.RoundOver);
        }
        if (Input.GetKeyDown(KeyCode.K))//Cancel
        {
            if (currentActor.tag != "Leader" || currentActor.tag != "Ally")
                return;
            Vector3 Leader = GameObject.FindGameObjectWithTag("Leader").transform.localPosition;
            //transform.position = new Vector3(Leader.x, Leader.y, transform.position.z);
            isMoving = true;
            iTween.MoveTo(gameObject, iTween.Hash("x", Leader.x, "y", Leader.y, "time", CursorMoveTimeInterval, "easetype", "spring", "oncomplete", "MovingEnd"));
        }
        #endregion
    }

    void MovingEnd()
    {
        isMoving = false;
    }
    
}
