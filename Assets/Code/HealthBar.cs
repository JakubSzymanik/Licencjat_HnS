using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] float maxHp;
    [SerializeField] float barSpeed;
    [SerializeField] Image image;
    float currentHp;
    float barTarget = 1;

    void Start()
    {
        currentHp = maxHp;
    }
    private void Update()
    {
        if(Mathf.Abs(barTarget - image.fillAmount) > .01f)
        {
            image.fillAmount = Mathf.Lerp(image.fillAmount, barTarget, barSpeed * Time.deltaTime);
        }
    }

    public void RemoveHealth(float damage, out bool isDead)
    {
        currentHp -= damage;
        isDead = currentHp <= 0;
        currentHp = Mathf.Clamp(currentHp, 0, maxHp);

        barTarget = currentHp / maxHp;
    }
    public void AddHealth(float heal)
    {
        currentHp += heal;
        currentHp = Mathf.Clamp(currentHp, 0, maxHp);

        barTarget = currentHp / maxHp;
    }
}
