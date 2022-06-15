using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] float damage;
    [SerializeField] float attackDistanceTreshold;
    [SerializeField] HealthBar healthBar;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Animator anim;
    [SerializeField] AnimationEventPasser animEvents;
    [SerializeField] ParticleSystem hitParticles;
    [SerializeField] Transform hitParticlesPosition;
    PlayerCharacterContoler target;
    public UnityEvent<Enemy> OnDeath = new UnityEvent<Enemy>();

    private void Start()
    {
        animEvents.OnAttackE.AddListener(AttackCallback);
    }
    private void Update()
    {
        anim.SetFloat("Speed", agent.velocity.magnitude);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerCharacterContoler player))
        {
            target = player;
            StartCoroutine(AttackCR());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent(out PlayerCharacterContoler player))
        {
            target = null;
            StopAllCoroutines();
            anim.ResetTrigger("Attack");
            agent.speed = 2;
        }
    }
    
    public void Damage(float damage)
    {
        healthBar.RemoveHealth(damage, out bool isDead);
        if (isDead)
        {
            OnDeath.Invoke(this);
            Destroy(this.gameObject);
        }
    }
    private void AttackCallback()
    {
        if(target != null && Vector3.Distance(transform.position, target.transform.position) < attackDistanceTreshold)
        {
            target.Damage(damage);
            Instantiate(hitParticles, hitParticlesPosition.position, Quaternion.identity);
        }
    }

    IEnumerator AttackCR()
    {
        while (target != null)
        {
            if (Vector3.Magnitude(transform.position - target.transform.position) > attackDistanceTreshold)
            {
                agent.isStopped = false;
                agent.SetDestination(target.transform.position);
            }
            else
            {
                agent.isStopped = true;
                transform.LookAt(target.transform);
                anim.SetTrigger("Attack");
            }

            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                agent.speed = 0;
            else
                agent.speed = 2;

            yield return null;
        }
        anim.ResetTrigger("Attack");
    }
}
