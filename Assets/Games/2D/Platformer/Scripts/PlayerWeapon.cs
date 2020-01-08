using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour, IDamager
{
    [SerializeField] private WeaponData weaponData;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private string buttonName = "Fire1";
    [SerializeField] private Animator animator;


    private float lastAttackTime;
    private static readonly int Attack = Animator.StringToHash("Attack");

    public int Damage => weaponData.WeaponDamage;

    public void SetDamage()
    {
        if (Time.time - lastAttackTime < weaponData.FireRate)
        {
            return;
        }

        lastAttackTime = Time.time;
        animator.SetTrigger(Attack);

        var target = GetTarget();
        target?.Hit(Damage);
    }

    public string ButtonName => buttonName;

    private IHitBox GetTarget()
    {
        IHitBox target = null;
        RaycastHit2D hit = Physics2D.Raycast(attackPoint.position, attackPoint.right, weaponData.WeaponRange);

        if (hit.collider != null)
        {
            target = hit.transform.gameObject.GetComponent<IHitBox>();
        }

        return target;
    }
}