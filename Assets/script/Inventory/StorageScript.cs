using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class StorageScript : MonoBehaviour
{
    [SerializeField] private GameObject storagePanel;
    [SerializeField] private GameObject ingredientButtonPrefab;
    [SerializeField] private GameObject tabButtonPrefab;
    [SerializeField] private Transform tabButtonContainer;
    [SerializeField] private Transform ingredientButtonContainer;
    [SerializeField] private GameObject player;

    [SerializeField] private Transform ingredientSpawnPoint;
    [SerializeField] private TMP_Text descriptionText;

    private int currentTab = 0;

    [System.Serializable]
    public struct ItemSlot
    {
        public string tabName;
        public List<KitchenObject> itemList;
    }

    public List<ItemSlot> storageTabs = new List<ItemSlot>();

    [Header("Random Generation Settings")]
    [SerializeField] private bool generateRandomTabsAndIngredients = true;
    [SerializeField] private int minTabs = 1;
    [SerializeField] private int maxTabs = 5;
    [SerializeField] private int minIngredientsPerTab = 1;
    [SerializeField] private int maxIngredientsPerTab = 10;
    [SerializeField] private List<KitchenObject> availableKitchenObjects;

    [Header("Camera Culling Mask Settings")]
    [SerializeField] private LayerMask interactableLayer;
    private LayerMask defaultLayerMask;

    private void Start()
    {
        storagePanel.SetActive(false);
        defaultLayerMask = Camera.main.cullingMask;
    }

    private void OnMouseDown()
    {
        ShowUI();
    }

    public void ShowUI()
    {
        storagePanel.SetActive(true);
        player.GetComponent<PlayerMovement>().canmove = false;

        // Masquer les objets non interactables dans la caméra
        Camera.main.cullingMask = interactableLayer;

        if (generateRandomTabsAndIngredients)
        {
            GenerateRandomTabsAndIngredients();
        }

        InitializeTabs();
    }

    public void CloseUI()
    {
        storagePanel.SetActive(false);
        player.GetComponent<PlayerMovement>().canmove = true;

        // Restaurer le LayerMask par défaut
        Camera.main.cullingMask = defaultLayerMask;
    }

    private void InitializeTabs()
    {
        foreach (Transform child in tabButtonContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in ingredientButtonContainer)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < storageTabs.Count; i++)
        {
            GameObject tabButton = Instantiate(tabButtonPrefab, tabButtonContainer);
            TMP_Text buttonText = tabButton.GetComponentInChildren<TMP_Text>();

            if (buttonText != null)
            {
                buttonText.text = storageTabs[i].tabName;
            }

            int tabIndex = i;
            tabButton.GetComponent<Button>().onClick.AddListener(() => DisplayTab(tabIndex));
        }

        if (storageTabs.Count > 0)
        {
            DisplayTab(0);
        }
    }

    public void DisplayTab(int tabIndex)
    {
        currentTab = tabIndex;

        foreach (Transform child in ingredientButtonContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (var kitchenObject in storageTabs[tabIndex].itemList)
        {
            GameObject ingredientButton = Instantiate(ingredientButtonPrefab, ingredientButtonContainer);

            Image buttonImage = ingredientButton.GetComponentInChildren<Image>();
            if (buttonImage != null)
            {
                buttonImage.sprite = kitchenObject.Image;
            }

            ingredientButton.GetComponent<Button>().onClick.AddListener(() => OnIngredientClicked(kitchenObject));
            AddHoverEvents(ingredientButton, kitchenObject);
        }
    }

    private void AddHoverEvents(GameObject button, KitchenObject kitchenObject)
    {
        EventTrigger trigger = button.AddComponent<EventTrigger>();

        // Event : Pointer Enter
        EventTrigger.Entry pointerEnter = new EventTrigger.Entry();
        pointerEnter.eventID = EventTriggerType.PointerEnter;
        pointerEnter.callback.AddListener((eventData) => ShowDescription(kitchenObject.description));
        trigger.triggers.Add(pointerEnter);

        // Event : Pointer Exit
        EventTrigger.Entry pointerExit = new EventTrigger.Entry();
        pointerExit.eventID = EventTriggerType.PointerExit;
        pointerExit.callback.AddListener((eventData) => ClearDescription());
        trigger.triggers.Add(pointerExit);
    }

    private void ShowDescription(string description)
    {
        if (descriptionText != null)
        {
            descriptionText.text = description;
        }
    }

    private void ClearDescription()
    {
        if (descriptionText != null)
        {
            descriptionText.text = string.Empty;
        }
    }

    private void OnIngredientClicked(KitchenObject kitchenObject)
    {
        if (currentTab >= 0 && currentTab < storageTabs.Count)
        {
            storageTabs[currentTab].itemList.Remove(kitchenObject);
        }

        if (ingredientSpawnPoint != null)
        {
            GameObject ingredient = Instantiate(kitchenObject.Prefab, ingredientSpawnPoint.position, Quaternion.identity);
            
        }

        CloseUI();
    }

    private void GenerateRandomTabsAndIngredients()
    {
        storageTabs.Clear();

        int numberOfTabs = Random.Range(minTabs, maxTabs + 1);

        for (int i = 0; i < numberOfTabs; i++)
        {
            ItemSlot newTab = new ItemSlot
            {
                tabName = $"Tab {i + 1}",
                itemList = new List<KitchenObject>()
            };

            int numberOfIngredients = Random.Range(minIngredientsPerTab, maxIngredientsPerTab + 1);

            for (int j = 0; j < numberOfIngredients; j++)
            {
                KitchenObject randomIngredient = GenerateRandomIngredient();
                if (randomIngredient != null)
                {
                    newTab.itemList.Add(randomIngredient);
                }
            }

            storageTabs.Add(newTab);
        }
    }

    private KitchenObject GenerateRandomIngredient()
    {
        if (availableKitchenObjects == null || availableKitchenObjects.Count == 0)
        {
            Debug.LogWarning("No available kitchen objects to select from.");
            return null;
        }

        return availableKitchenObjects[Random.Range(0, availableKitchenObjects.Count)];
    }
}
