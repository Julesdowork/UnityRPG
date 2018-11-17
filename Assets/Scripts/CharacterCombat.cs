using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour {

    public float attackSpeed = 1f;
    public float attackDelay = 0.6f;
    public bool InCombat { get; private set; }

    public event System.Action OnAttack;

    CharacterStats myStats;
    CharacterStats opponentStats;
    private float attackCooldown = 0;
    const float combatCooldown = 5f;
    private float lastAttackTime;

    void Start()
    {
        myStats = GetComponent<CharacterStats>();
    }

    void Update()
    {
        attackCooldown -= Time.deltaTime;

        if (Time.time - lastAttackTime >= combatCooldown)
        {
            InCombat = false;
        }
    }

    public void Attack(CharacterStats targetStats)
    {
        if (attackCooldown <= 0)
        {
            opponentStats = targetStats;
            if (OnAttack != null)
                OnAttack();

            attackCooldown = 1f / attackSpeed;
            InCombat = true;
            lastAttackTime = Time.time;
        }
    }

    public void AttackHit_AnimationEvent()
    {
        opponentStats.TakeDamage(myStats.damage.GetValue());
        if (opponentStats.currentHealth <= 0)
        {
            InCombat = false;
        }
    }
}
