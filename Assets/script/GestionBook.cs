using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;

public class GestionBook : MonoBehaviour
{
    [Header("Gestion Panel1")]
    [SerializeField] private TextMeshProUGUI titleText1;
    [SerializeField] private TextMeshProUGUI descriptionText1;
    [SerializeField] private TextMeshProUGUI ingrediantText1;

    [Header("Book")]
    [SerializeField] private Book recipeBook;

    [Header("Recipe Creation UI")]
    [SerializeField] private TMP_InputField titleInputField;
    [SerializeField] private TMP_InputField descriptionInputField;
    [SerializeField] private TMP_InputField ingredientsInputField;
    [SerializeField] private Button createRecipeButton;

    private readonly string _defaultText = "Aucune recette disponible";
    private int _currentPage;

    private void Start()
    {
        UpdateUI();
        createRecipeButton.onClick.AddListener(CreateNewRecipe);

        // Connectez les événements onSelect et onDeselect
        titleInputField.onSelect.AddListener(OnInputFieldSelected);
        descriptionInputField.onSelect.AddListener(OnInputFieldSelected);
        ingredientsInputField.onSelect.AddListener(OnInputFieldSelected);

        titleInputField.onDeselect.AddListener(OnInputFieldDeselected);
        descriptionInputField.onDeselect.AddListener(OnInputFieldDeselected);
        ingredientsInputField.onDeselect.AddListener(OnInputFieldDeselected);
    }

    private void OnInputFieldSelected(string text)
    {
        // Empêche le mouvement du joueur
        FindObjectOfType<PlayerMovement>().canmove = false;
    }

    private void OnInputFieldDeselected(string text)
    {
        // Autorise le mouvement du joueur
        FindObjectOfType<PlayerMovement>().canmove = true;
    }

    public void UpdateUI()
    {
        if (recipeBook == null || recipeBook.recipes.Count == 0)
        {
            titleText1.text = _defaultText;
            descriptionText1.text = "";
            ingrediantText1.text = "";
            return;
        }

        if (_currentPage < recipeBook.recipes.Count)
        {
            Recipe recipe1 = recipeBook.recipes[_currentPage];
            titleText1.text = recipe1.title;
            descriptionText1.text = recipe1.description;
            ingrediantText1.text = FormatIngredients(recipe1.ingredients);
        }
        else
        {
            titleText1.text = _defaultText;
            descriptionText1.text = "";
            ingrediantText1.text = "";
        }
    }

    private string FormatIngredients(List<string> ingredients)
    {
        if (ingredients == null || ingredients.Count == 0)
            return "Aucun ingrédient spécifié.";

        return $"Ingrédients : {string.Join(", ", ingredients)}";
    }

    public void ShowNextPage()
    {
        if (recipeBook != null && _currentPage + 1 < recipeBook.recipes.Count)
        {
            _currentPage += 1;
            UpdateUI();
        }
    }

    public void ShowPagePrevious()
    {
        if (_currentPage - 1 >= 0)
        {
            _currentPage -= 1;
            UpdateUI();
        }
    }

    public void CreateNewRecipe()
    {
        string title = titleInputField.text.Trim();
        string description = descriptionInputField.text.Trim();
        string ingredientsText = ingredientsInputField.text.Trim();

        // Validation des champs
        if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(description) || string.IsNullOrWhiteSpace(ingredientsText))
        {
            Debug.LogWarning("Tous les champs doivent être remplis pour créer une recette.");
            return;
        }

        // Parse les ingrédients (séparés par des virgules)
        List<string> ingredients = new List<string>(ingredientsText.Split(','));

        // Crée un nouvel objet ScriptableObject Recipe
        Recipe newRecipe = ScriptableObject.CreateInstance<Recipe>();
        newRecipe.title = title;
        newRecipe.description = description;
        newRecipe.ingredients = ingredients;

        // Ajoute la recette au livre via la méthode `AddRecipe`
        recipeBook.AddRecipe(newRecipe);

        Debug.Log($"Nouvelle recette ajoutée : {title}");

        // Réinitialise les champs d’entrée
        titleInputField.text = "";
        descriptionInputField.text = "";
        ingredientsInputField.text = "";
        UpdateUI();
    }
}
