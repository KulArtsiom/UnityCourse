using System;
using UnityEngine;


public class Player : MonoBehaviour, IPlayer, IHitBox
{
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

    private void Awake()
    {
        RegisterPlayer();
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
        print("Player died");
    }
}