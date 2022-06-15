using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Data", menuName = "Item")]
public class ItemData : ScriptableObject
{
    [SerializeField] string name;
    [SerializeField] GameObject prefab;
    [SerializeField] ItemType type;
    [SerializeField] float value;

    public GameObject Prefab => prefab;
    public ItemType Type => type;
    public float Value => value;
    public string Name => name;
}
