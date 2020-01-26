using System;
using UnityEngine;

public class Player : MonoBehaviour, IPlayer, IHitBox
{
    private PlayerWeapon[] weapons;
    
    public void RegisterPlayer()
    {
        GameManager manager = FindObjectOfType<GameManager>();

        if (manager.Player == null)
        {
            manager.Player = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [SerializeField] private int health = 1;
    public int Health
    {
        get => health;
        private set
        {
            health = value;
            if (health <= 0)
            {
                DieEnemy();
            }
        }
    }
    
    public void Hit(int damage)
    {
        Health -= damage;
    }

    public void DieEnemy()
    {
        print("Player died");
    }

    private void Awake()
    {
        RegisterPlayer();
        weapons = GetComponents<PlayerWeapon>();
        InputController.FireAction += Attack;
    }

    private void OnDestroy()
    {
        InputController.FireAction -= Attack;
    }

    private void Attack(string button)
    {
        foreach (var weapon in weapons)
        {
            if (weapon.ButtonName == button)
            {
                weapon.SetDamage();
            }
        }
    }
}
