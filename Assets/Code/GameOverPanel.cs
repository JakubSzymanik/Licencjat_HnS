using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] string victoryText;
    [SerializeField] string defeatText;
    [SerializeField] Text text;
    [SerializeField] CanvasGroup cg;
    public UnityEvent OnRestartPressed;
    public UnityEvent OnQuitPressed;


    public void Show(bool victory)
    {
        cg.interactable = true;
        cg.blocksRaycasts = true;
        text.text = victory ? victoryText : defeatText;
        StartCoroutine(ShowPanel());
    }
    public void Restart()
    {
        OnRestartPressed.Invoke();
    }
    public void Quit()
    {
        OnQuitPressed.Invoke();
    }

    IEnumerator ShowPanel()
    {
        float timer = 0;
        while(timer < .4f)
        {
            cg.alpha = timer / .4f;

            yield return null;
            timer += Time.deltaTime;
        }
        cg.alpha = 1;
    }
}
