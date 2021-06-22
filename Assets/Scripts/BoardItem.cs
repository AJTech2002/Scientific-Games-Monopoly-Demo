using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BoardItem : MonoBehaviour
{
    [Header("General Properties")]
    [Range(1, 40)]
    public int boardItemLocation; //1 (GO) -> 10 (Jail) etc. in Circular
    public ItemType type;

    [Header("Property Information")]
    public Property property;

    [Header("UI")]
    public TextMeshPro title;
    public TextMeshPro value;

    private void Awake()
    {
        if (property != null)
        {
            gameObject.GetComponent<MeshRenderer>().material.SetColor("_PropertyColor", property.propertyColor);
            title.text = property.name;
            value.text = $"${property.value}";
        }
    }

    public void OnLand (int player)
    {
        switch (type)
        {
            case ItemType.Go:
                print("Landed On Go");
                break;
            case ItemType.FreeParking:
                print("Free Parking");
                break;
            default:
                print("Type doesn't exist");
                break;
        }
    }
        
}

public enum ItemType
{
    Place,
    Go,
    Jail,
    FreeParking,
    GotoJail,
    IncomeTax
}