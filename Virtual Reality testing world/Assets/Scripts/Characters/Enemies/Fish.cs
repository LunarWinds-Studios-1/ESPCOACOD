using System;
using UnityEngine;
using UnityEngine.AI;

public class Fish : MonoBehaviour, IDamageable
{
    [SerializeField] float maxHealth = 15;
    [SerializeField] float currentHealth;
    [SerializeField] public float moveSpeed = 3;
    [SerializeField] GameObject blood;
    [SerializeField] GameObject bloodLight;
    [SerializeField] HealthBar healthBar;
    public float healthPerBite = 10;
    [HideInInspector] public NavMeshAgent agent;

    public Vector3 damagePoint;

    [SerializeField] public Transform target;

    float randomHeightOffset;


    float cooldownTimer = 0;
    float cooldownTime;

    public bool active = true;
    public bool grabbable = true;

    public LayerMask targetableLayerMask;

    [SerializeField] Collider aggroCollider;
    [SerializeField] Collider attackCollider;

    public float aggroRadius;
    public float attackRadius;

    //State machine stuff
    public EnemyStateMachine stateMachine;
    public EnemyIdleState idleState;
    public EnemyTrackingState trackingState;
    public EnemyAttackingState attackingState;
    public EnemyFleeingState fleeingState;
    public EnemyGrappledState grappledState;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stateMachine = new EnemyStateMachine();
        idleState = new EnemyIdleState(this, stateMachine);
        trackingState = new EnemyTrackingState(this, stateMachine);
        attackingState = new EnemyAttackingState(this, stateMachine);
        fleeingState = new EnemyFleeingState(this, stateMachine);
        grappledState = new EnemyGrappledState(this, stateMachine);

        stateMachine.CurrentState = idleState;

        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
        cooldownTime = UnityEngine.Random.Range(0.5f, 10);
        currentHealth = maxHealth;

        healthBar?.Initialize(maxHealth);
    }

    // Update is called once per frame
    public virtual void Update()
    {
        Debug.Log("Current State: " + stateMachine.CurrentState);

        healthBar?.SetHealth(currentHealth);

        stateMachine.CurrentState.Update();
    }

    public void OnEnterDetectionRadius()
    {
        
        stateMachine.CurrentState.OnEnterDetectionRadius();
    }

    public void OnExitDetectionRadius()
    {
        stateMachine.CurrentState.OnExitDetectionRadius();
    }

    public void OnEnterAttackRadius()
    {
        stateMachine.CurrentState.OnEnterAttackRadius();
    }

    public void OnExitAttackRadius()
    {
        stateMachine.CurrentState.OnExitAttackRadius();
    }

    public void OnAnimationFinish()
    {
        stateMachine.CurrentState.OnAnimationFinish();
    }
    public virtual void FixedUpdate()
    {
        stateMachine.CurrentState.PhysicsUpdate();
    }

    private void OnTriggerEnter(Collider other)
    {
        stateMachine.CurrentState.OnTriggerEnter(other);
    }

    private void OnTriggerStay(Collider other)
    {
        stateMachine.CurrentState.OnTriggerStay(other);
    }

    public void ApproachTargetHeight()
    {
        float agentGroundHeight = transform.position.y - agent.baseOffset;
        float targetBaseOffset = target.transform.position.y - agentGroundHeight + randomHeightOffset;

        float v = 0;
        
        agent.baseOffset = Mathf.SmoothDamp(agent.baseOffset, targetBaseOffset, ref v, 0.2f);
    }

    public void ApproachTargetHeight(Vector3 targetPos)
    {
        float agentGroundHeight = transform.position.y - agent.baseOffset;
        float targetBaseOffset = targetPos.y - agentGroundHeight + randomHeightOffset;

        float v = 0;

        agent.baseOffset = Mathf.SmoothDamp(agent.baseOffset, targetBaseOffset, ref v, 0.2f);
    }

    public void Damage(float damage, Vector3 point)
    {
        currentHealth -= damage;
        if (damage > 5)
        {
            Instantiate(blood, point, Quaternion.identity);
        } else
        {
            Instantiate(bloodLight, point, Quaternion.identity);
        }
        if (currentHealth <= 0)
        {
            Die();
            
        }

        
    }

    public void Damage(float damage)
    {
        Damage(damage, transform.position);
    }



    public void Die()
    {
        Destroy(gameObject);
    }

    public void SetActive(bool active)
    {
        if (!active)
        {
            agent.enabled = false;
            aggroCollider.enabled = false;
            attackCollider.enabled = false;
        } else
        {
            agent.enabled = true;
            aggroCollider.enabled = true;
            attackCollider.enabled = true;
            transform.rotation = Quaternion.identity;
        }
        
        this.active = active;
    }

}
