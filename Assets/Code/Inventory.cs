using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] InventoryUI inventoryUI;
    [SerializeField] Transform helmetPoint;
    [SerializeField] Transform weaponPoint;
    [SerializeField] ItemData startingWeapon;
    GameObject equippedWeaponPrefab;
    GameObject equippedHelmetPrefab;
    ItemData equippedWeapon;
    ItemData equippedHelmet;
    List<ItemData> items = new List<ItemData>();

    public float Damage => equippedWeapon.Value;
    public float Armor => equippedHelmet != null ? equippedHelmet.Value : 0;

    private void Start()
    {
        AddItem(startingWeapon, true);
        EquipItem(startingWeapon);
        inventoryUI.OnEquippedChange.AddListener(EquipItem);
    }

    public void ToggleUI()
    {
        if (inventoryUI.IsVisible)
            inventoryUI.Hide();
        else
            inventoryUI.Show();
    }
    public void AddItem(ItemData item, bool selected)
    {
        items.Add(item);
        switch (item.Type)
        {
            case ItemType.Helmet:
                inventoryUI.AddHelmetButton(item, selected);
                break;
            case ItemType.Weapon:
                inventoryUI.AddWeaponButton(item, selected);
                break;
        }
    }
    public void EquipItem(ItemData item)
    {
        switch (item.Type)
        {
            case ItemType.Helmet:
                equippedHelmet = item;
                if (equippedHelmetPrefab != null)
                    Destroy(equippedHelmetPrefab);
                equippedHelmetPrefab = Instantiate(item.Prefab, helmetPoint);
                break;
            case ItemType.Weapon:
                equippedWeapon = item;
                if (equippedWeaponPrefab != null)
                    Destroy(equippedWeaponPrefab);
                equippedWeaponPrefab = Instantiate(item.Prefab, weaponPoint);
                break;
        }
    }
}
