using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Potion : MonoBehaviour
{
    [SerializeField] Image coverImg;
    [SerializeField] Button btn;
    [SerializeField] float cooldown;
    [SerializeField] float value;
    [SerializeField] public UnityEvent<float> OnUse;

    bool isReady = true;

    private void Start()
    {
        btn.onClick.AddListener(Use);
    }
    private void Use()
    {
        if(isReady)
        {
            StartCoroutine(CooldownCR(cooldown));
            OnUse.Invoke(value);
        }
    }

    IEnumerator CooldownCR(float cooldown)
    {
        isReady = false;
        float timer = 0;
        while(timer < cooldown)
        {
            coverImg.fillAmount = 1 - timer / cooldown;

            timer += Time.deltaTime;
            yield return null;
        }
        isReady = true;
        coverImg.fillAmount = 0;
    }
}
