public interface IPlayer
{
    void RegisterPlayer();
}

public interface IEnemy
{
    void RegisterEmemy();
}

public interface IDamager
{
    int Damage { get; }
    void SetDamage();
}

public interface IHitBox
{
    int Health { get; }
    void Hit(int damage);
    void Die();
}