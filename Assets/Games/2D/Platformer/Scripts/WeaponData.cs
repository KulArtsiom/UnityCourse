using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Objects/WeaponObject", order = 1)]
public class WeaponData : ScriptableObject
{
    public string WeaponName = "Weapon Name";
    public int WeaponDamage = 1;
    public float WeaponRange = 0.3f;
    public float FireRate = 1f;
    public GameObject Projectile;
    public float ProjectileSpeed = 1f;
}
