using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Property",menuName ="Monopoly/Property",order = 1)]
public class Property : ScriptableObject
{
    [Header("General Values")]
    public string name;
    public Color propertyColor;

    [Header("Financials")]
    public float value;
    public float rentAmount;

    [HideInInspector]
    public string owner = "None"; //Name of Player that Owns the Property

    //Reset
    private void Awake()
    {
        owner = "None";
    }

}
