using System.Linq;
using UnityEngine;

public class CryptUIManager : MonoBehaviour
{
    [SerializeField] private Crypt crypt;
    [SerializeField] private Canvas uiCanvas;
    [SerializeField] private FloorsNavigationUIManager floorsNavigationUIManager;
    [SerializeField] private FloorDetailUIManager floorDetailUIManager;

    private void Start()
    {
        floorDetailUIManager.gameObject.SetActive(false);

        floorsNavigationUIManager.SetFloorItems(crypt.Floors.ToList());
        floorsNavigationUIManager.OnFloorSelected += FloorsNavigationUIManager_OnFloorSelected;
    }

    private void FloorsNavigationUIManager_OnFloorSelected(object sender, FloorItem floorItem)
    {
        floorDetailUIManager.SetFloor(floorItem.Floor);
        floorDetailUIManager.gameObject.SetActive(true);
    }

    void Update()
    {
        bool isUIEnabled = Input.GetKey(KeyCode.Tab);
        if (uiCanvas.gameObject.activeSelf != isUIEnabled)
        {
            uiCanvas.gameObject.SetActive(isUIEnabled);
        }
    }
}
