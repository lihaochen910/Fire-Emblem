using UnityEngine;
using MapData;
using Map;

public class s_RangeManager : MonoBehaviour {
    public static SquareGrid currentMap;

    public static s_RangeManager _ins;
    public Pool<GameObject> pool;

    public static Sprite s_unPassable, s_Passable;

    [HideInInspector]
    public GameObject prefab_s_Range;//z value must be -2
    [HideInInspector]
    public GameObject RangeParent;
    public bool isShowing = false;
    void Awake () {
        _ins = this;
        pool = new Pool<GameObject>();
        prefab_s_Range = Resources.Load<GameObject>("prefabs/s_Range");
        s_unPassable = Resources.Load<Sprite>("Img/unPassable");
        s_Passable = Resources.Load<Sprite>("Img/Passable_2");
        RangeParent = new GameObject("Object Pool");
    }
    void Start()
    {
        //init map
        SquareGrid.unit = 1f;
        currentMap = new SquareGrid(30,-16);
        currentMap.unPassable = Mission_demo_DATA.unPassable;
        currentMap.forests = Mission_demo_DATA.forest;


    }

    int frame = 0;
    int need = 50;
    void LateUpdate () {

	    if(frame++ < need)//初始化时每帧生成一个
        {
            GameObject g = Instantiate(prefab_s_Range);
            pool.Store(g);
            g.name = "s" + frame;
            g.transform.SetParent(RangeParent.transform);
            g.SetActive(false);
        }

	}
}
