using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteraction : MonoBehaviour
{
    public GameObject CurrentObject;
    private GameObject ClickedObject;
    private GameObject ThisGameObject;

    [SerializeField] public GameObject ItemUI;

    public bool isUIHere;
    public int ClickedObjectLayer;

    Color m_MouseOverColor;
    Color m_OriginalColor;

    MeshRenderer m_Renderer;
    Rigidbody m_Rigidbody;
    BoxCollider m_BoxCollider;

    void Start()
    {
        m_Renderer = GetComponent<MeshRenderer>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_BoxCollider = GetComponent<BoxCollider>();
        ItemUI = GameObject.Find("itemUI");

        m_OriginalColor = m_Renderer.material.color;
        m_MouseOverColor = m_OriginalColor * 2;
        isUIHere = false;
        ClickedObject = null;
        CurrentObject = null;
    }

    void OnMouseOver()
    {
        m_Renderer.material.color = m_MouseOverColor;
    }

    void OnMouseExit()
    {
        m_Renderer.material.color = m_OriginalColor;
    }

    private void OnMouseDown()
    {
        if (!isUIHere)
        {
            ClickedObject = gameObject;
            ItemUI.GetComponent<InputHandler>().ItemInteraction = this;
            ClickedObjectLayer = gameObject.layer;

            // Calcul des bounds combinés de l'objet et de ses enfants
            Bounds combinedBounds = CalculateCombinedBounds(gameObject);

            // Positionner l'UI au-dessus du point le plus haut
            Vector3 uiPosition = new Vector3(combinedBounds.center.x, combinedBounds.max.y + 1.0f, combinedBounds.center.z); // +1.0f pour écarter l'UI

            if (ItemUI != null)
            {
                ItemUI.transform.position = uiPosition;
            }

            isUIHere = true;
            CurrentObject = ClickedObject;
        }
        else
        {
            // Cache l'UI
            ItemUI.transform.position = new Vector3(0, -100, 0);
            isUIHere = false;
            CurrentObject = null;
        }
    }

    /// <summary>
    /// Calcule les Bounds combinés de l'objet actuel et de tous ses enfants.
    /// </summary>
    private Bounds CalculateCombinedBounds(GameObject rootObject)
    {
        BoxCollider[] colliders = rootObject.GetComponentsInChildren<BoxCollider>();
        if (colliders.Length == 0)
        {
            // Aucun BoxCollider trouvé, retourne un bound par défaut
            return new Bounds(rootObject.transform.position, Vector3.zero);
        }

        Bounds combinedBounds = colliders[0].bounds;

        foreach (var collider in colliders)
        {
            combinedBounds.Encapsulate(collider.bounds);
        }

        return combinedBounds;
    }

}
