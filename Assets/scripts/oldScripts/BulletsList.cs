
using System.Collections.Generic;
using UnityEngine;

public static class BulletsList  
{
    private static List<GameObject> weapons = new List<GameObject>();
    public static event  System.Action OnPeopleChange;

    public static void AddWeaponInList(GameObject weapon) => weapons.Add(weapon);
    public static int GetAmount() { return weapons.Count; }
    public static GameObject Getweapon(int index) { return weapons[index]; }
    


}
