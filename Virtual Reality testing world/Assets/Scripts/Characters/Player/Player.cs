using UnityEngine;
using System;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using System.Collections;
public class Player : MonoBehaviour, IDamageable
{
    [Header("Healing")] 
    [SerializeField] float maxHealth;
    [SerializeField] float currentHealth;
    [SerializeField] float overhealDepletion = 10;
    [SerializeField] HealthBar playerHealthBar;
    [SerializeField] bool debugMode = false;
    bool overhealed = false;

    [Header("Fish Eating")]
    [SerializeField] float fishEatRate = 0.5f;
    [SerializeField] float fishEatDamage = 5;
    [SerializeField] Harpoon harpoon;
    [SerializeField] EatHitBox eatHitbox;

    Cooldown eatCooldown;
    public Rigidbody rb;


    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerHealthBar.Initialize(maxHealth);
        eatCooldown = new Cooldown(fishEatRate);
        currentHealth = maxHealth;


    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth * 2);
        HandleOverheal();
        EatFish();
        playerHealthBar.SetHealth(currentHealth);

        if (debugMode)
        {
            TestPlayerHealth();
        }
    }

    public void HandleOverheal()
    {
        if (currentHealth > maxHealth)
        {
            overhealed = true;
            currentHealth -= overhealDepletion * Time.deltaTime;
        } else
        {
            if (overhealed)
            {
                overhealed = false ;
                currentHealth = maxHealth;
            }
        }
    }

    public void Damage(float damage)
    {
        currentHealth -= damage;


        if (currentHealth < 0)
        {
            Die();
        }
    }

    public void Damage(float damage, Vector3 point)
    {
        Damage(damage);
    }

    public void Heal(float health)
    {
        currentHealth += health;

    }

    public void Die()
    {
        throw new NotImplementedException();
    }

    public void TestPlayerHealth()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            currentHealth -= 10;

        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            currentHealth += 10;

        }
    }

    public void EatFish()
    {
        if (harpoon.grabbedObject != null && harpoon.grabbedObject.GetComponent<Fish>() != null)
        {
            if (eatHitbox.obj != null)
            {
                Fish fish = harpoon.grabbedObject.GetComponent<Fish>();
                if (eatHitbox.obj == fish.gameObject)
                {
                    if (!eatCooldown.isCoolingDown)
                    {
                        eatCooldown.StartCooldown();
                        fish.Damage(fishEatDamage);
                        Heal(fish.healthPerBite);
                        //play eat noise
                    }
                }
            }
        }
    }


}
