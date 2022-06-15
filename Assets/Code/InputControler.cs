using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputControler : MonoBehaviour
{
    [SerializeField] PlayerCharacterContoler player;
    [SerializeField] GameObject clickParticles;
    Selectable lastHit;

    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool didHit = Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, Physics.AllLayers, QueryTriggerInteraction.Ignore);
        if (!didHit)
            return;
        Selectable selectable = hit.transform.GetComponent<Selectable>();

        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(clickParticles, hit.point, Quaternion.identity);

            if(selectable != null)
            {
                if(selectable.Type == SelectableType.Enemy)
                {
                    player.SetAttackTarget(selectable.GetComponent<Enemy>());
                }
                else if(selectable.Type == SelectableType.Item)
                {
                    player.SetPickupTarget(selectable.GetComponent<Item>());
                }
            }
        }

        //Movement
        if(Input.GetMouseButton(0) && selectable == null)
        {
            player.SetMoveTarget(hit.point);
        }

        //Highlights
        if (didHit && selectable != null)
        {
                if (lastHit != null && selectable != lastHit)
                    lastHit.Highlight(false);

                lastHit = selectable;
                lastHit.Highlight(true);
        }
        else if(lastHit != null)
        {
            lastHit.Highlight(false);
            lastHit = null;
        }
    }
}
