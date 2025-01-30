using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    [Header("Liste des recettes")]
    public List<Recipe> recipes = new List<Recipe>(); // Liste de recettes

    /// <summary>
    /// Ajoute une recette au livre.
    /// </summary>
    /// <param name="recipe">L'objet Recipe � ajouter</param>
    public void AddRecipe(Recipe recipe)
    {
        if (recipe != null && !recipes.Contains(recipe))
        {
            recipes.Add(recipe);
            Debug.Log($"Recette ajout�e : {recipe.title}");
        }
        else
        {
            Debug.LogWarning("Recette d�j� existante ou invalide !");
        }
    }

    /// <summary>
    /// R�cup�re une recette par son titre.
    /// </summary>
    /// <param name="title">Titre de la recette � r�cup�rer</param>
    /// <returns>La recette correspondante, ou null si elle n'existe pas</returns>
    public Recipe GetRecipe(string title)
    {
        foreach (Recipe recipe in recipes)
        {
            if (recipe.title == title)
            {
                return recipe;
            }
        }
        Debug.LogWarning($"Recette non trouv�e : {title}");
        return null;
    }

    /// <summary>
    /// Liste toutes les recettes dans le livre (debug uniquement).
    /// </summary>
    public void ListAllRecipes()
    {
        Debug.Log("Liste des recettes dans le livre :");
        foreach (Recipe recipe in recipes)
        {
            Debug.Log($"- {recipe.title}");
        }
    }
}
