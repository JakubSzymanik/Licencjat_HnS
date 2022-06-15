using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] float buttonHeight;
    [SerializeField] Button buttonPrefab;
    [SerializeField] RectTransform helmetsContent;
    [SerializeField] RectTransform weaponsContent;
    [SerializeField] CanvasGroup cg;

    List<Button> helmetButtons = new List<Button>();
    List<Button> weaponButtons = new List<Button>();
    Button selectedWeapon;
    Button selectedHelmet;
    bool isVisible = false;
    public bool IsVisible => isVisible;

    public UnityEvent<ItemData> OnEquippedChange;

    private void OnDestroy()
    {
        foreach(Button btn in helmetButtons)
        {
            btn.onClick.RemoveAllListeners();
        }
        foreach(Button btn in weaponButtons)
        {
            btn.onClick.RemoveAllListeners();
        }
    }

    public void Show()
    {
        cg.alpha = 1;
        cg.interactable = true;
        cg.blocksRaycasts = true;
        isVisible = true;
    }
    public void Hide()
    {
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;
        isVisible = false;
    }
    public void AddHelmetButton(ItemData itemData, bool selected)
    {
        Button btn = Instantiate(buttonPrefab, helmetsContent);
        helmetButtons.Add(btn);
        btn.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, (helmetButtons.Count - 1) * -buttonHeight);
        helmetsContent.sizeDelta = new Vector2(helmetsContent.sizeDelta.x, helmetButtons.Count * buttonHeight);
        btn.onClick.AddListener(() => ItemButtonClicked(itemData, btn));
        btn.transform.GetChild(0).GetComponent<Text>().text = itemData.Name;

        if (selected)
            SelectButton(itemData, btn);
    }
    public void AddWeaponButton(ItemData itemData, bool selected)
    {
        Button btn = Instantiate(buttonPrefab, weaponsContent);
        weaponButtons.Add(btn);
        btn.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, (weaponButtons.Count - 1) * -buttonHeight);
        weaponsContent.sizeDelta = new Vector2(weaponsContent.sizeDelta.x, weaponButtons.Count * buttonHeight);
        btn.onClick.AddListener(() => ItemButtonClicked(itemData, btn));
        btn.transform.GetChild(0).GetComponent<Text>().text = itemData.Name;

        if (selected)
            SelectButton(itemData, btn);
    }

    private void ItemButtonClicked(ItemData data, Button btn)
    {
        OnEquippedChange.Invoke(data);
        SelectButton(data, btn);
    }
    private void SelectButton(ItemData data, Button btn)
    {
        if (data.Type == ItemType.Helmet)
        {
            if (selectedHelmet != null)
                selectedHelmet.image.color = Color.white;
            selectedHelmet = btn;
            selectedHelmet.image.color = Color.green;
        }
        else
        {
            if (selectedWeapon != null)
                selectedWeapon.image.color = Color.white;
            selectedWeapon = btn;
            selectedWeapon.image.color = Color.green;
        }
    }
}
