using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private Button LeftHandButton;
    [SerializeField] private Button RightHandButton;
    [SerializeField] private GameObject LeftHandCamera;
    [SerializeField] private GameObject RightHandCamera;

    private GameObject LeftHandItem;
    private GameObject RightHandItem;

    public ItemInteraction ItemInteraction;

    bool isItemInLeftHand;
    bool isItemInRightHand;

    void Start()
    {
        isItemInLeftHand = false;
        isItemInRightHand = false;
    }

    public void PickUpAndDeposit(int value)
    {
        if (ItemInteraction.CurrentObject != null)
        {
            // Récupérer le Rigidbody de l'objet que vous souhaitez manipuler
            Rigidbody rb = ItemInteraction.CurrentObject.GetComponent<Rigidbody>();

            // Cas de prise en main d'un ingrédient (Layer 8)
            if (ItemInteraction.ClickedObjectLayer == 8)
            {
                if (value == 0 && !isItemInLeftHand && LeftHandItem == null)
                {
                    LeftHandItem = ItemInteraction.CurrentObject;
                    LeftHandItem.transform.position = LeftHandCamera.transform.position + LeftHandCamera.transform.forward * 2f;
                    LeftHandItem.transform.SetParent(LeftHandCamera.transform);

                    // Désactiver la gravité
                    if (rb != null) rb.isKinematic = true;

                    isItemInLeftHand = true;

                    // Masquer l'UI
                    ItemInteraction.ItemUI.transform.position = new Vector3(0, -100, 0);
                    ItemInteraction.isUIHere = false;
                    ItemInteraction.CurrentObject = null;
                }
                else if (value == 1 && !isItemInRightHand && RightHandItem == null)
                {
                    RightHandItem = ItemInteraction.CurrentObject;
                    RightHandItem.transform.position = RightHandCamera.transform.position + RightHandCamera.transform.forward * 2f;
                    RightHandItem.transform.SetParent(RightHandCamera.transform);

                    // Désactiver la gravité
                    if (rb != null) rb.isKinematic = true;

                    isItemInRightHand = true;

                    // Masquer l'UI
                    ItemInteraction.ItemUI.transform.position = new Vector3(0, -100, 0);
                    ItemInteraction.isUIHere = false;
                    ItemInteraction.CurrentObject = null;
                }
            }
            // Cas de prise en main d'un contenant (Layer 9)
            else if (ItemInteraction.ClickedObjectLayer == 9)
            {
                if (value == 0 && !isItemInLeftHand && LeftHandItem == null)
                {
                    LeftHandItem = ItemInteraction.CurrentObject;
                    LeftHandItem.transform.position = LeftHandCamera.transform.position + LeftHandCamera.transform.forward * 2f;
                    LeftHandItem.transform.SetParent(LeftHandCamera.transform);

                    // Désactiver la gravité
                    if (rb != null) rb.isKinematic = true;

                    isItemInLeftHand = true;

                    // Masquer l'UI
                    ItemInteraction.ItemUI.transform.position = new Vector3(0, -100, 0);
                    ItemInteraction.isUIHere = false;
                    ItemInteraction.CurrentObject = null;
                }
                else if (value == 1 && !isItemInRightHand && RightHandItem == null)
                {
                    RightHandItem = ItemInteraction.CurrentObject;
                    RightHandItem.transform.position = RightHandCamera.transform.position + RightHandCamera.transform.forward * 2f;
                    RightHandItem.transform.SetParent(RightHandCamera.transform);

                    // Désactiver la gravité
                    if (rb != null) rb.isKinematic = true;

                    isItemInRightHand = true;

                    // Masquer l'UI
                    ItemInteraction.ItemUI.transform.position = new Vector3(0, -100, 0);
                    ItemInteraction.isUIHere = false;
                    ItemInteraction.CurrentObject = null;
                }
            }
            // Cas de prise en main d'un contenant avec des ingrédients (Layer 9 + enfant Layer 8)
            else if (ItemInteraction.ClickedObjectLayer == 9 && ItemInteraction.CurrentObject.transform.childCount > 0)
            {
                if (value == 0 && !isItemInLeftHand && LeftHandItem == null)
                {
                    LeftHandItem = ItemInteraction.CurrentObject;
                    LeftHandItem.transform.position = LeftHandCamera.transform.position + LeftHandCamera.transform.forward * 2f;
                    LeftHandItem.transform.SetParent(LeftHandCamera.transform);

                    // Désactiver la gravité
                    if (rb != null) rb.isKinematic = true;

                    isItemInLeftHand = true;

                    // Masquer l'UI
                    ItemInteraction.ItemUI.transform.position = new Vector3(0, -100, 0);
                    ItemInteraction.isUIHere = false;
                    ItemInteraction.CurrentObject = null;
                }
                else if (value == 1 && !isItemInRightHand && RightHandItem == null)
                {
                    RightHandItem = ItemInteraction.CurrentObject;
                    RightHandItem.transform.position = RightHandCamera.transform.position + RightHandCamera.transform.forward * 2f;
                    RightHandItem.transform.SetParent(RightHandCamera.transform);

                    // Désactiver la gravité
                    if (rb != null) rb.isKinematic = true;

                    isItemInRightHand = true;

                    // Masquer l'UI
                    ItemInteraction.ItemUI.transform.position = new Vector3(0, -100, 0);
                    ItemInteraction.isUIHere = false;
                    ItemInteraction.CurrentObject = null;
                }
            }

            // Déposer un objet dans un contenant (Layer 9)
            if (isItemInLeftHand == true && ItemInteraction.ClickedObjectLayer == 9 && LeftHandItem != null && ItemInteraction.CurrentObject.transform.childCount == 0)
            {
                DepositInContainer(ref LeftHandItem, ref isItemInLeftHand, ItemInteraction.CurrentObject);
            }
            else if (isItemInRightHand == true && ItemInteraction.ClickedObjectLayer == 9 && RightHandItem != null && ItemInteraction.CurrentObject.transform.childCount == 0)
            {
                DepositInContainer(ref RightHandItem, ref isItemInRightHand, ItemInteraction.CurrentObject);
            }

            // Déposer un objet dans un objet du Layer 10 ou 11
            else if (isItemInLeftHand == true && (ItemInteraction.ClickedObjectLayer == 10 || ItemInteraction.ClickedObjectLayer == 12) && LeftHandItem != null)
            {
                DepositInContainer(ref LeftHandItem, ref isItemInLeftHand, ItemInteraction.CurrentObject);
            }
            else if (isItemInRightHand == true && (ItemInteraction.ClickedObjectLayer == 10 || ItemInteraction.ClickedObjectLayer == 12) && RightHandItem != null)
            {
                DepositInContainer(ref RightHandItem, ref isItemInRightHand, ItemInteraction.CurrentObject);
            }
        }
    }

    private void DepositInContainer(ref GameObject handItem, ref bool isItemInHand, GameObject container)
    {
        // Récupérer le BoxCollider du conteneur
        BoxCollider containerCollider = container.GetComponent<BoxCollider>();

        if (containerCollider != null)
        {
            // Calculer le centre de la surface haute du BoxCollider
            Vector3 containerCenter = containerCollider.bounds.center;  // Centre du BoxCollider
            float height = containerCollider.bounds.extents.y;  // Moitié de la hauteur du BoxCollider

            // Le centre de la surface haute du BoxCollider
            Vector3 containerTopSurfaceCenter = containerCenter + Vector3.up * height;

            // Déposer l'objet sur la surface haute du conteneur
            handItem.transform.position = containerTopSurfaceCenter + Vector3.up * 1f;  // Décalage léger pour s'assurer qu'il est juste au-dessus

            // Réactiver la gravité
            handItem.GetComponent<Rigidbody>().isKinematic = false;

            // Attacher l'objet au conteneur
            handItem.transform.parent = container.transform;

            // Masquer l'UI et mettre à jour l'état
            ItemInteraction.ItemUI.transform.position = new Vector3(0, -100, 0);
            ItemInteraction.isUIHere = false;
            isItemInHand = false;
            handItem = null;
        }
    }



    private void PlaceInHand(GameObject handCamera, ref bool isItemInHand, ref GameObject handItem, GameObject targetObject)
    {
        targetObject.transform.position = handCamera.transform.position + handCamera.transform.forward * 2;
        targetObject.GetComponent<Rigidbody>().isKinematic = true;
        isItemInHand = true;
        handItem = targetObject;
        ItemInteraction.ItemUI.transform.position = new Vector3(0, -100, 0);
        ItemInteraction.isUIHere = false;
        ItemInteraction.CurrentObject = null;
    }

    private void DropContainer(ref GameObject handItem, ref bool isItemInHand, GameObject surface)
    {
        handItem.transform.position = surface.transform.position + Vector3.up * 2;
        handItem.GetComponent<Rigidbody>().isKinematic = false;
        handItem.transform.parent = null;
        ItemInteraction.ItemUI.transform.position = new Vector3(0, -100, 0);
        ItemInteraction.isUIHere = false;
        isItemInHand = false;
        handItem = null;
    }

    public void PickUpAndDepositFromStorage(GameObject item, int handIndex)
    {
        if (handIndex == 0 && !isItemInLeftHand && LeftHandItem == null)
        {
            PlaceInHand(LeftHandCamera, ref isItemInLeftHand, ref LeftHandItem, item);
        }
        else if (handIndex == 1 && !isItemInRightHand && RightHandItem == null)
        {
            PlaceInHand(RightHandCamera, ref isItemInRightHand, ref RightHandItem, item);
        }
    }

}
