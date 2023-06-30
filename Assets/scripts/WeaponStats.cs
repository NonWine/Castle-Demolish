
using UnityEngine;
[CreateAssetMenu(fileName = "WeaponStats", menuName = "ScriptableObjects/Weapon", order = 1)]
public class WeaponStats : ScriptableObject
{
    [SerializeField] private float Radius;
    [SerializeField] private float _Force;
    [SerializeField] private int _Damage;
    [SerializeField] private float _CoolDown;
    [SerializeField] private int _Count;
    [SerializeField] private bool CanExplode;
    public float GetRadius() { return Radius; }
    public float GetForce() { return _Force; }
    public int GetDamage() { return _Damage; }
    public float GetCD() { return _CoolDown; }
    public int GetCount() { return _Count; }
    public bool IsExplode() { return CanExplode; }
}
