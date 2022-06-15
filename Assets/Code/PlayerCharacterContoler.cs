using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayerCharacterContoler : MonoBehaviour
{
    [SerializeField] float pickupDistanceTreshold;
    [SerializeField] float attackDistanceTreshold;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Animator anim;
    [SerializeField] AnimationEventPasser animEvents;
    [SerializeField] Inventory inventory;
    [SerializeField] HealthBar healthBar;
    [SerializeField] ParticleSystem hitParticles;
    [SerializeField] Transform hitParticlesPosition;
    public UnityEvent OnDeath = new UnityEvent();

    Enemy currentEnemy;
    Item currentItem;
    Coroutine currentCR;

    private void Start()
    {
        animEvents.OnAttackE.AddListener(AttackCallback);
        animEvents.OnPickupE.AddListener(PickupCallback);
    }
    void Update()
    {
        anim.SetFloat("Speed", agent.velocity.magnitude);
    }

    public void SetMoveTarget(Vector3 target)
    {
        anim.ResetTrigger("Attack");
        if(currentCR != null)
            StopCoroutine(currentCR);
        agent.isStopped = false;
        agent.SetDestination(target);
    }
    public void SetAttackTarget(Enemy enemy)
    {
        if (currentCR != null)
            StopCoroutine(currentCR);
        currentEnemy = enemy;
        currentCR = StartCoroutine(AttackCR());
    }
    public void SetPickupTarget(Item item)
    {
        if (currentCR != null)
            StopCoroutine(currentCR);
        currentItem = item;
        currentCR = StartCoroutine(PickupCR());
    }
    public void Damage(float value)
    {
        value *=  (30 / (30 + inventory.Armor));
        healthBar.RemoveHealth(value, out bool isDead);
        if(isDead)
        {
            OnDeath.Invoke();
        }
    }
    public void UsePotion(float value)
    {
        healthBar.AddHealth(value);
    }

    public void AttackCallback()
    {
        if(currentEnemy != null && Vector3.Distance(currentEnemy.transform.position, transform.position) <= attackDistanceTreshold)
        {
            currentEnemy.Damage(inventory.Damage);
            Instantiate(hitParticles, hitParticlesPosition.position, Quaternion.identity);
        }
    }
    public void PickupCallback()
    {
        print("Pickup");
        if(currentItem != null)
        {
            inventory.AddItem(currentItem.Data, false); //zmieniæ to po zmianie skryptu itemu do podniesienia
            Destroy(currentItem.gameObject);
        }
    }

    IEnumerator AttackCR()
    {
        while(currentEnemy != null)
        {
            if (Vector3.Magnitude(transform.position - currentEnemy.transform.position) > attackDistanceTreshold)
            {
                agent.isStopped = false;
                agent.SetDestination(currentEnemy.transform.position);
            }
            else
            {
                agent.isStopped = true;
                transform.LookAt(currentEnemy.transform);
                anim.SetTrigger("Attack");
            }

            yield return null;
        }
        anim.ResetTrigger("Attack");
    }
    IEnumerator PickupCR()
    {
        agent.SetDestination(currentItem.transform.position);
        while(Vector3.SqrMagnitude(transform.position - currentItem.transform.position) > 2 * pickupDistanceTreshold)
        {
            yield return null;
        }
        agent.SetDestination(Vector3.Lerp(transform.position, currentItem.transform.position, .5f));
        while (Vector3.SqrMagnitude(transform.position - currentItem.transform.position) > pickupDistanceTreshold)
        {
            yield return null;
        }

        anim.SetTrigger("Pickup");
    }
   
}
