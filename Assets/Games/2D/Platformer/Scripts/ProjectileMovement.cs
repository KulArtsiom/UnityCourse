using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
   private float lifeTime = 3f;
    private int damageBullet;
    private float forceBullet = 5;
    private float speedTimeScale = 0.05f;
    private float speedTimeBackgroundScale = 0.02f;

    public void LaunchBullet(float force, int damage, Vector2 direction)
    {
        var rig = GetComponent<Rigidbody2D>();
        damageBullet = damage;

        rig.AddForce(new Vector2(force * forceBullet * direction.x, force * forceBullet * direction.y), ForceMode2D.Impulse);
        StartCoroutine(DestroyTime());
    }
    public void LaunchBulletBackground(float force, int damage, Vector2 direction)
    {
        StartCoroutine(BackgroundProjectile());
        LaunchBullet(force, damage, direction);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        IHitBox target = null;
        if (collision != null)
        {
            target = collision.transform.gameObject.GetComponent<IHitBox>();
        }
        target?.Hit(damageBullet);
    }

    private IEnumerator DestroyTime()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    private IEnumerator BackgroundProjectile()
    {
        var scale = transform.localScale;
        scale.x = 0.4f;
        scale.y = scale.x;
        scale.z = scale.x;
        transform.localScale = scale;
        var collider = GetComponent<PolygonCollider2D>();
        collider.enabled = false;
        while (transform.localScale.x < 1)
        {
            yield return new WaitForSeconds(speedTimeBackgroundScale);
            scale.x += 0.025f;
            scale.y = scale.x;
            scale.z = scale.x;
            transform.localScale = scale;
        }
        collider.enabled = true;
    }
}
