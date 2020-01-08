using System;
using UnityEngine;


public class StaticObject : MonoBehaviour, IHitBox
{
    [SerializeField] private LevelObjectData objectData;
    private Rigidbody2D rig;
    private int health;

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
        print("Object died");
    }

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        Health = objectData.Health;
        if (rig)
        {
            rig.bodyType = objectData.Static ? RigidbodyType2D.Static : RigidbodyType2D.Dynamic;
        }
    }

    [ContextMenu("Rename")]
    private void Rename()
    {
        if (objectData != null)
        {
            gameObject.name = objectData.Name;
        }
    }

    [ContextMenu("Copyup")]
    private void CopyUp()
    {
        var copy = Instantiate(gameObject);
        var pos = copy.transform.position;
        pos.y += 4;
        copy.transform.position = pos;
    }
}