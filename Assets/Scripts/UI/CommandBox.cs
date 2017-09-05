using UnityEngine;
using System.Collections;

public enum Command
{
    Attack = 0x01,
    RoundOver,
    AwaitOrders,
}
public class CommandBox : MonoBehaviour {

    public static CommandBox _ins;

    Transform CommandList;
    UISprite sprite_BG;
    GameObject point;

    GameObject prefab_commandLabel;
	
	void Start () {
        _ins = this;
        CommandList = GameObject.Find("UI Root/CommandBox/CommandList").transform;
        sprite_BG = GameObject.Find("UI Root/CommandBox/sprite_BG").GetComponent<UISprite>();
        point = GameObject.Find("UI Root/CommandBox/point");
        prefab_commandLabel = Resources.Load<GameObject>("Prefabs/commandLabel");
        sprite_BG.enabled = false;
        point.SetActive(false);
    }

    bool CursorIsMoving = false;
	void FixedUpdate () {
        if (!active || CursorIsMoving)
            return;
        float v = Input.GetAxisRaw("Vertical");
        if (v == -1)
        {
            if (selectIndex + 1 < CommandList.childCount)
            {
                CursorIsMoving = true;
                iTween.MoveTo(point, iTween.Hash("y", point.transform.localPosition.y - 60, "time", 0.1f, "islocal", true, "oncomplete", "MoveEnd", "oncompletetarget", gameObject));
                selectIndex++;
            }
                
        }
        else if(v == 1)
        {
            if (selectIndex - 1 >= 0)
            {
                CursorIsMoving = true;
                iTween.MoveTo(point, iTween.Hash("y", point.transform.localPosition.y + 60, "time", 0.1f, "islocal", true, "oncomplete", "MoveEnd", "oncompletetarget", gameObject)); selectIndex--;
            }
                
        }
	}
    void Update()
    {
        if (!active)
            return;
        if (Input.GetKeyDown(KeyCode.J))
        {
            ExecuteCommand();
            Hide();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            if(commander)
                commander.CancelTheMove();
            Hide();
        }

    }
    int selectIndex = 0;//命令组下标
    Ally commander;
    public void ExecuteCommand()//执行命令
    {
        print(CommandList.GetChild(selectIndex).name);
        if (CommandList.GetChild(selectIndex).GetComponent<UILabel>().text.Equals("待命"))
            commander.AwaitOrders();
        if (CommandList.GetChild(selectIndex).GetComponent<UILabel>().text.Equals("攻击"))
        {
            //Attack Command here
        }
        if (CommandList.GetChild(selectIndex).GetComponent<UILabel>().text.Equals("结束"))
        {
            //End Command here 
        }
        selectIndex = 0;
    }
    public bool active = false;
    public void Show(Ally commander,params Command[] Commands)//根据命令显示UI项
    {
        sprite_BG.enabled = true;
        point.SetActive(true);
        point.transform.localPosition = new Vector3(400,270,0);

        sprite_BG.height = 80 + Commands.Length * (Commands.Length==1?0: Commands.Length == 2?30:40);
        sprite_BG.gameObject.transform.localPosition = new Vector3(590,310,0);

        for (var i = 0;i<Commands.Length;i++)
        {
            GameObject com=Instantiate<GameObject>(prefab_commandLabel);
            com.transform.SetParent(CommandList);
            com.transform.localScale = Vector3.one;
            com.transform.localPosition = new Vector3(450,270 - (i *(50+10)),0);
            com.GetComponent<UILabel>().text = 
                Commands[i] == Command.Attack ? "攻击" : Commands[i] == Command.RoundOver ? "结束" : "待命";

        }
        active = true;
        this.commander = commander;
    }
    public void Hide()
    {
        CommandList.DestroyChildren();
        sprite_BG.enabled = false;
        point.SetActive(false);
        active = false;
    }
    void MoveEnd()
    {
        CursorIsMoving = false;
    }
}
