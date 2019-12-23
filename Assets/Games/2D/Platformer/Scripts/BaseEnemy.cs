using System;
using UnityEngine;


public class BaseEnemy : MonoBehaviour, IEnemy, IHitBox
{
    public void RegisterEnemy()
    {
        GameManager manager = FindObjectOfType<GameManager>();
        manager.Enemies.Add(this);
    }

    private void Awake()
    {
        RegisterEnemy();
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
                Die();
            }
        }
    }
    
    public void Hit(int damage)
    {
        Health -= damage;
    }

    public void Die()
    {
        print("Enemy died");
    }
}