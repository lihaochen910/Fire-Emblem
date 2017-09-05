using UnityEngine;

public class AxSoldier : Enemy
{

    void Start()
    {
        #region initValue
        HP = 9;
        Attack_Power = 3;
        Attack_Range = 1;
        Defense = 4;
        Agility = 3;
        Magic_Power = 2;
        Luck = 2;
        #endregion
    }
}
