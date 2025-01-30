using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe" , menuName = "Scriptableobject/Recipe")]
public class Recipe : ScriptableObject
{
    public string title;
    public string description;
    public List<string> ingredients;
}
