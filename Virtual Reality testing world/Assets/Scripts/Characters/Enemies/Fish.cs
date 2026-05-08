
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(AudioSource))]
public class Fish : MonoBehaviour, IDamageable
{
    [SerializeField] protected float maxHealth = 15;
    [SerializeField] float currentHealth;
    [SerializeField] public float moveSpeed = 3;
    [SerializeField] GameObject blood;
    [SerializeField] GameObject bloodLight;
    [SerializeField] HealthBar healthBar;
    [SerializeField] float healthBarVisibilityTime;
    Cooldown healthbarVisibilityCooldown;
    [SerializeField] public Animator animator;
    public float healthPerBite = 10;
    [HideInInspector] public NavMeshAgent agent;
    [SerializeField] LayerMask geometryLayerMask;
    [SerializeField] int doubloons = 3;

    public Vector3 damagePoint;
    public bool frozen = false;
    bool dead = false;

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

    [SerializeField] List<GameObject> meshParts = new List<GameObject>();
    GameManager manager;

    Rigidbody rb;

    public GameObject child;
    public Vector3 targetPosition;

    //State machine stuff
    public EnemyStateMachine stateMachine;
    public EnemyIdleState idleState;
    public EnemyTrackingState trackingState;
    public EnemyAttackingState attackingState;
    public EnemyFleeingState fleeingState;
    public EnemyGrappledState grappledState;
    public EnemyRecoveringState recoveringState;
    public EnemyDeathState deathState;

    [SerializeField] public List<AudioClip> hurtSounds = new List<AudioClip>();
    [SerializeField] public List<AudioClip> bigHurtSounds = new List<AudioClip>();
    [SerializeField] public List<AudioClip> attackSounds = new List<AudioClip>();
    [SerializeField] public List<AudioClip> death = new List<AudioClip>();
    [SerializeField] public List<AudioClip> grappledSounds = new List<AudioClip>();
    [HideInInspector] public AudioSource audioSource;
    public AudioSource grappleSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthbarVisibilityCooldown = new Cooldown(healthBarVisibilityTime);
        manager = FindFirstObjectByType<GameManager>();
        manager.freezeEnemies += OnFreezeEnemies;
        InstantiateStates();

        stateMachine.CurrentState = idleState;

        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
        cooldownTime = UnityEngine.Random.Range(0.5f, 10);
        maxHealth *= manager.globalDifficulty;
        currentHealth = maxHealth;

        healthBar?.Initialize(maxHealth);
        healthBar.SetVisible(false);
        agent.updateRotation = false;
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    public virtual void InstantiateStates()
    {
        stateMachine = new EnemyStateMachine();
        idleState = new EnemyIdleState(this, stateMachine);
        trackingState = new EnemyTrackingState(this, stateMachine);
        attackingState = new EnemyAttackingState(this, stateMachine);
        fleeingState = new EnemyFleeingState(this, stateMachine);
        grappledState = new EnemyGrappledState(this, stateMachine);
        recoveringState = new EnemyRecoveringState(this, stateMachine);
        deathState = new EnemyDeathState(this, stateMachine);
    }

    // Update is called once per frame
    public virtual void Update()
    {
        Debug.Log("Current State: " + stateMachine.CurrentState);

        healthBar?.SetHealth(currentHealth);

        stateMachine.CurrentState.Update();

        healthBar.SetVisible(healthbarVisibilityCooldown.isCoolingDown);
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

    public void OnSoundEvent()
    {
        stateMachine.CurrentState.OnSoundEvent();
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

    public void SetBaseOffsetToProperHeight()
    {
        float agentGroundHeight = transform.position.y - agent.baseOffset;
        float targetBaseOffset = target.transform.position.y - agentGroundHeight + randomHeightOffset;
    }

    public void ApproachTargetHeight(Vector3 targetPos)
    {
        float agentGroundHeight = transform.position.y - agent.baseOffset;
        float targetBaseOffset = targetPos.y - agentGroundHeight + randomHeightOffset;
        float upperBounds = 100;
        float lowerBounds = -100;
        RaycastHit hit;
        Physics.Raycast(transform.position, transform.up, out hit, 100, geometryLayerMask);

        if (hit.collider != null)
        {
            upperBounds = hit.point.y - agentGroundHeight + randomHeightOffset;
        }

        Physics.Raycast(transform.position, -transform.up, out hit, 100, geometryLayerMask);

        if (hit.collider != null)
        {
            lowerBounds = hit.point.y - agentGroundHeight + randomHeightOffset;
        }

        targetBaseOffset = Mathf.Clamp(targetBaseOffset, lowerBounds, upperBounds);

        float v = 0;

        agent.baseOffset = Mathf.SmoothDamp(agent.baseOffset, targetBaseOffset, ref v, 0.2f);
    }

    public float GetTargetHeight(Vector3 targetPos)
    {
        return targetPos.y;
    }

    public virtual void Damage(float damage, Vector3 point)
    {
        if (!dead)
        {
            audioSource.clip = hurtSounds[UnityEngine.Random.Range(0, hurtSounds.Count)];
            audioSource.Play();
            currentHealth -= damage;
            manager.damageDealt.IncreaseStat(damage);
            healthbarVisibilityCooldown.StartCooldown();
            if (damage > 5)
            {
                Instantiate(blood, point, Quaternion.identity);
            }
            else
            {
                Instantiate(bloodLight, point, Quaternion.identity);
            }
            if (currentHealth <= 0)
            {
                Die();
                dead = true;
            }

        }
    }

    public void Damage(float damage)
    {
        Damage(damage, this.transform.position);
    }



    public void Die()
    {
        manager.freezeEnemies -= OnFreezeEnemies;
        manager.enemiesKilled.IncreaseStat(1);
        manager.doubloons += (int) (doubloons * manager.globalDifficulty);
        manager.doubloonsEarned.IncreaseStat((int)(doubloons * manager.globalDifficulty));
        stateMachine.ChangeState(deathState);
        stateMachine.locked = true;
        manager.DespawnEnemy(this);
        StartCoroutine(DeathDissolve(3));
    }

    public void Knockback(Vector3 direction, float strength)
    {
        UnconstrainRigidbody();
        rb.AddForce(direction * strength, ForceMode.Impulse);
        rb.AddTorque(UnityEngine.Random.insideUnitSphere.normalized * strength / 3, ForceMode.Impulse);
        stateMachine.ChangeState(recoveringState);
    }

    /*IEnumerator knockback (float time)
    {
        yield return new WaitForSeconds(time);
        ConstrainRigidbody();
        stateMachine.ChangeState(idleState);
        transform.eulerAngles = Vector3.zero;
    } */

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

    public void OnFreezeEnemies(object sender, EventArgs args)
    {
        agent.speed = 0;
        agent.enabled = false;
        animator.speed = 0;
        frozen = true;
    }


    public void ConstrainRigidbody()
    {
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    public void UnconstrainRigidbody()
    {
        rb.constraints = RigidbodyConstraints.None;
    }

    public IEnumerator DeathDissolve(float time)
    {
        float t = 0;
        yield return new WaitForSeconds(2);
        while (t < time)
        {
            t += Time.deltaTime;

            for (int i = 0; i < meshParts.Count; i++)
            {
                
                meshParts[i].GetComponent<Renderer>().material.SetFloat("_DissolveAmount", t / time);
            }
            yield return null;
            
        }
        Destroy(gameObject);
    }

    public void AlignToTargetDirection()
    {
        float reference = 0;
        var targetRotation = Quaternion.LookRotation(targetPosition - child.transform.position);
        var delta = Quaternion.Angle(child.transform.rotation, targetRotation);

        if (delta > 0)
        {
            var t = Mathf.SmoothDampAngle(delta, 0.0f, ref reference, 0.1f);
            t = 1.0f - t / delta;
            child.transform.rotation = Quaternion.Slerp(child.transform.rotation, targetRotation, t);
        }

    }

}
