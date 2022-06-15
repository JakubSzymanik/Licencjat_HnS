using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class Selectable : MonoBehaviour
{
    [SerializeField] SelectableType type;
    [SerializeField] Outline outline;
    bool isHighlighted;
    public SelectableType Type { get { return type; } }

    public void Highlight(bool value)
    {
        if(value && !isHighlighted)
        {
            StopAllCoroutines();
            StartCoroutine(HighlightCR());
        }
        else if(!value && isHighlighted)
        {
            StopAllCoroutines();
            StartCoroutine(FadeCR());
        }
    }

    IEnumerator HighlightCR()
    {
        isHighlighted = true;
        while(outline.OutlineColor.a < 1)
        {
            outline.OutlineColor += new Color(0, 0, 0, 3f * Time.deltaTime);
            yield return null;
        }
    }
    IEnumerator FadeCR()
    {
        isHighlighted = false;
        while (outline.OutlineColor.a > 0)
        {
            outline.OutlineColor -= new Color(0, 0, 0, 3f * Time.deltaTime);
            yield return null;
        }
    }
}
