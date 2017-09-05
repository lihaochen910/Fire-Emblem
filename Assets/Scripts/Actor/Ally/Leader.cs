using UnityEngine;
using System.Collections;

public class Leader : Ally
{

	
	void Start () {
        #region initValue
        HP = 15;
        Attack_Power = 10;
        Attack_Range = 1;
        Defense = 8;
        Agility = 5;
        Magic_Power = 2;
        Luck = 2;
        #endregion
    }

}
