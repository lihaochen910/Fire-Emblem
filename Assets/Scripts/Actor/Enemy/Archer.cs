using UnityEngine;
using System.Collections;

public class Archer : Enemy {

	
	void Start () {
        #region initValue
        HP = 8;
        Attack_Power = 6;
        Attack_Range = 2;
        Defense = 4;
        Agility = 4;
        Magic_Power = 1;
        Luck = 2;
        #endregion
    }

}
