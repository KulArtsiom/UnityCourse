using System;
using System.Security.Cryptography;
using UnityEngine;


public class ScriptBoxAdapter : MonoBehaviour, IHitBox
{
    [SerializeField] private GameObject hitTarget;
    private IHitBox hitBox;

    private void Reset()
    {
        var hit = GetComponentInParent<IHitBox>();
        if (hit != null)
        {
            hitTarget = (hit as MonoBehaviour)?.gameObject;
        }
    }

    private void Start()
    {
        hitBox = hitTarget.GetComponent<IHitBox>();
        if (gameObject == hitTarget)
        {
            return;
        }
        
        Destroy(this);
        Debug.LogError($"Wrong place for hit box on EnemyGraphics{gameObject.name}");
    }

    public int Health => hitBox.Health;

    public void Hit(int damage)
    {
        hitBox.Hit(damage);
    }
    
    public void Die() { hitBox.Die();}
}