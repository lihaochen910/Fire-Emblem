using UnityEngine;
using System.Collections;

public class SkyKnight : Enemy
{

	// Use this for initialization
	void Start () {
        #region initValue
        HP = 7;
        Attack_Power = 4;
        Attack_Range = 1;
        Defense = 4;
        Agility = 7;
        Magic_Power = 2;
        Luck = 2;
        #endregion
    }


}
