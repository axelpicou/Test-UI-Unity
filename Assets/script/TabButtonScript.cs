using UnityEngine;
using UnityEngine.UI;

public class TabButtonScript : MonoBehaviour
{
    public int TabNum;
    public StorageScript storage;

    public void Setup(int tabNumber, StorageScript storageScript)
    {
        TabNum = tabNumber;
        storage = storageScript;
        GetComponent<Button>().onClick.AddListener(() => storage.DisplayTab(TabNum));
    }
}
