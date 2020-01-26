using System;
using UnityEngine;

namespace Games._2D.Platformer.Scripts
{
    public enum EEnemyState
    {
        Sleep,
        Wait,
        StartWalk,
        Walk,
        StartPatrol,
        Patrol
    }

    public class BaseEnemy : MonoBehaviour, IEnemy, IHitBox
    {
    
        [SerializeField] private Animator animator;
        [SerializeField] private Transform checkGroundPoint;
        [SerializeField] private Transform graphics;
        [SerializeField] private int health = 10;

        private GameManager gameManager;

        private EEnemyState currentState = EEnemyState.Sleep;

        private float wakeUpTimer;
        private float waitTimer;
        private float patrolTimer;
        private float patroWaitTimer;
        private bool isWaitPotrol;
        private EEnemyState nextState;
        private float currentDirection = 1f;
        private float iteration;
        private float distanse;
        private static readonly int Walking = Animator.StringToHash("Walking");
        private static readonly int Die = Animator.StringToHash("Die");
        private static readonly int IsGround = Animator.StringToHash("IsGround");

        public void GetEnemy()
        {
            gameManager = FindObjectOfType<GameManager>();
            gameManager.Enemies.Add(this);
        }

        private void Awake()
        {
            animator.SetBool(IsGround, true);
            GetEnemy();
            wakeUpTimer = Time.time + 1f;
        }

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
            animator.SetTrigger(Die);
            Destroy(this);
            Destroy(gameObject, 1f);
        }

        private void Update()
        {
            switch (currentState)
            {
                case EEnemyState.Sleep:
                    Sleep();
                    break;
                case EEnemyState.Wait:
                    Wait();
                    break;
                case EEnemyState.StartPatrol:
                    animator.SetInteger(Walking, 0);
                    currentState = EEnemyState.Patrol;
                    break;
                case EEnemyState.Patrol:
                    Patroling();
                    break;
                case EEnemyState.StartWalk:
                    animator.SetInteger(Walking, 1);
                    currentState = EEnemyState.Walk;
                    break;
                case EEnemyState.Walk:
                    Walk();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void StartSleeping(float sleepTime = 1f)
        {
            wakeUpTimer = Time.time + sleepTime;
            currentState = EEnemyState.Sleep;
        }

        private void Sleep()
        {
            if (Time.time >= wakeUpTimer)
            {
                WakeUp();
            }
        }

        private void WakeUp()
        {
            var playerPosition = ((MonoBehaviour) gameManager.Player).transform.position;

            if (Vector3.Distance(transform.position, playerPosition) > 20f)
            {
                StartSleeping();
                return;
            }

            if (Mathf.Abs(playerPosition.y - transform.position.y) < 0.3)
            {
                currentState = EEnemyState.Wait;
                nextState = EEnemyState.StartWalk;
                waitTimer = Time.time + 0.1f;
                return;
            }

            currentState = EEnemyState.Wait;
            nextState = EEnemyState.StartPatrol;
            isWaitPotrol = true;
            waitTimer = Time.time + 0.3f;
            patroWaitTimer = Time.time + 1.5f;
            animator.SetInteger(Walking, 0);
        }

        private void Wait()
        {
            if (Time.time >= waitTimer)
            {
                currentState = nextState;
            }
        }

        private void Patroling()
        {
            var playerPosition = ((MonoBehaviour) gameManager.Player).transform.position;
            if (Mathf.Abs(playerPosition.y - transform.position.y) < 0.3)
            {
                currentState = EEnemyState.Wait;
                nextState = EEnemyState.StartWalk;
                waitTimer = Time.time + 0.1f;
                return;
            }

            if (isWaitPotrol)
            {
                if (Time.time < patroWaitTimer)
                {
                    return;
                }
                isWaitPotrol = false;
                patrolTimer = Time.time + 3f;
                animator.SetInteger(Walking, 1);
            }
            else
            {
                RaycastHit2D hit = Physics2D.Raycast(checkGroundPoint.position, Vector2.down, 0.3f);
                if (hit.collider == null)
                {
                    currentDirection *= -1;
                    float angle = currentDirection > 0 ? 0f : 180f;
                    graphics.localEulerAngles = new Vector3(0, angle, 0f);
                    return;
                }

                transform.Translate(transform.right * (Time.deltaTime * currentDirection));
            }

            if (Time.time > patrolTimer)
            {
                isWaitPotrol = true;
                patroWaitTimer = Time.time + 1.5f;
                animator.SetInteger(Walking, 0);
                return;
            }
        }

        private void Walk()
        {
            var playerPosition = ((MonoBehaviour) gameManager.Player).transform.position;
            if (Mathf.Abs(playerPosition.y - transform.position.y) > 0.3)
            {
                currentState = EEnemyState.Wait;
                nextState = EEnemyState.StartPatrol;
                isWaitPotrol = true;
                waitTimer = Time.time + 0.3f;
                patroWaitTimer = Time.time + 1.5f;
                animator.SetInteger(Walking, 0);
                return;
            }

            transform.Translate(transform.right * (Time.deltaTime * currentDirection));
        }
    
    }
}