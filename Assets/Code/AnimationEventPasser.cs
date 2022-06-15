using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEventPasser : MonoBehaviour
{
    public UnityEvent OnAttackE;
    public UnityEvent OnPickupE;

    public void OnPickup()
    {
        OnPickupE.Invoke();
    }
    public void OnAttack()
    {
        OnAttackE.Invoke();
    }
}
