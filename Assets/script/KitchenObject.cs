using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "food" , menuName = "Scriptableobject/Food")]
public class KitchenObject : ScriptableObject
{
    public string description;
    public GameObject Prefab;
    public Sprite Image;
}
