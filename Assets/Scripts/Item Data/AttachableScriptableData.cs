using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Attachable/Default Item")]
public class AttachableScriptableData : ScriptableObject
{
    [System.Serializable]
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
    [Range(0,1)]
    public int[] attachValue = new int[6];

}
