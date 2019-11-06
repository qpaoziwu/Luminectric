using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Attachable/Default Item")]
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

    public SourceType source = new SourceType();

    [Serializable]
    public class SourceType 
    {
        public string elementType;
    }



    public SourceType medium = new SourceType();

    [Serializable]
    public class OutputType
    {
        public string elementType;
    }


    public SourceType output = new SourceType();

    [Serializable]
    public class SourceType
    {
        public string elementType;
    }



    public SourceType shell = new SourceType();

    [Serializable]
    public class SourceType
    {
        public string elementType;
    }



    public SourceType source = new SourceType();

    [Serializable]
    public class SourceType
    {
        public string elementType;
    }
}

