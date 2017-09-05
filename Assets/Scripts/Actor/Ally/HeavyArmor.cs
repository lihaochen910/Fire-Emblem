using UnityEngine;
using System.Collections;

public class HeavyArmor : Ally
{

	// Use this for initialization
	void Start () {
        #region initValue
        HP = 12;
        Attack_Power = 10;
        Attack_Range = 1;
        Defense = 10;
        Agility = 3;
        Magic_Power = 2;
        Luck = 2;
        #endregion
    }


}
