using UnityEngine;
using System.Collections;

public class Wizard : Enemy
{

	// Use this for initialization
	void Start () {
        #region initValue
        HP = 6;
        Attack_Power = 1;
        Attack_Range = 2;
        Defense = 4;
        Agility = 3;
        Magic_Power = 7;
        Luck = 2;
        #endregion
    }

}
