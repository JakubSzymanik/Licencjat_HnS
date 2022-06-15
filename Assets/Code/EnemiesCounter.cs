using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemiesCounter : MonoBehaviour
{
    [SerializeField] Text enemiesAmountTxt;

    public void SetAmount(int amount)
    {
        enemiesAmountTxt.text = amount.ToString();
    }
}
