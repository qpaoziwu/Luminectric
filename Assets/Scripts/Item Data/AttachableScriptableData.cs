using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AttachableObject")]
public class AttachableScriptableData : ScriptableObject
{

    public enum ItemType
    {
        Source,
        Medium,
        Output,
        Shell,
        Protocol
    }
    public ItemType itemType;
    public string itemName;
    [Range(0, 1)]
    public int[] attachValue = new int[6];
    [Range(0, 5)]
    public int itemRarity;


    //Setup for GUI////////////////////////////////////////////////
    public SourceType source = new SourceType();
    public MediumType medium = new MediumType();
    public OutputType output = new OutputType();
    public ShellType shell = new ShellType();
    public ProtocolType protocol = new ProtocolType();


    //Edit below to add variables//////////////////////////////////

    [Serializable]
    public class SourceType 
    {
        public string elementType;
        public int damage;

    }


    [Serializable]
    public class MediumType
    {
        public string inputType;

    }


    [Serializable]
    public class OutputType
    {
        public float size;
        public int quantity;
        public string shape;
        public float projectileSpeed;
        public float fireRate;
        public int bounce;
    }


    [Serializable]
    public class ShellType
    {
        public string elementType;
        public Material material;

    }


    [Serializable]
    public class ProtocolType
    {
        public float travelSpeedMod;
        public Transform travelDirection;
        public Transform destination;
        public List<Transform> travelPath;

    }
}