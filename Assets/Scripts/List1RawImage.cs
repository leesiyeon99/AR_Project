using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class List1RawImage : MonoBehaviour
{
    [SerializeField] RawImage rawImage;
    [SerializeField] GameObject chairPrefab;
    [SerializeField] GameObject bedPrefab;
    [SerializeField] GameObject deskPrefab;
    [SerializeField] GameObject drawerPrefab;
    [SerializeField] GameObject lightPrefab;
    [SerializeField] GameObject mirrorPrefab;
    [SerializeField] GameObject sofaPrefab;

    [SerializeField] ARSessionOrigin arSessionOrigin;
    [SerializeField] ARRaycastManager arRaycastManager;
    [SerializeField] ARAnchorManager arAnchorManager;
    [SerializeField] ARPlaneManager arPlaneManager;

    [SerializeField] GameObject placementButton;
    [SerializeField] Button confirmButton;
    [SerializeField] Button cancelButton;

    [SerializeField] public PlacementButtonUI placementButtonUI;

    private Vector3 targetPosition;
    private GameObject currentPreview;

    private void Start()
    {
        placementButton.SetActive(false);

        if (rawImage != null)
        {
            Button button = rawImage.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.AddListener(ActivatePreview);
            }
        }

        if (confirmButton != null)
        {
            confirmButton.gameObject.SetActive(false);
            confirmButton.onClick.AddListener(ConfirmPlacement);
        }

        if (cancelButton != null)
        {
            cancelButton.gameObject.SetActive(false);
            cancelButton.onClick.AddListener(PreviewCancel);
        }
    }

    private void Update()
    {
        if (currentPreview != null)
        {
            UpdatePreviewPosition();
        }
    }

    private void UpdatePreviewPosition()
    {
        Ray ray = new Ray(arSessionOrigin.camera.transform.position, arSessionOrigin.camera.transform.forward);
        List<ARRaycastHit> hitResults = new List<ARRaycastHit>();

        if (arRaycastManager.Raycast(ray, hitResults))
        {
            Pose hitPose = hitResults[0].pose;
            currentPreview.transform.position = hitPose.position;

            confirmButton.interactable = true;
        }
        else
        {
            Vector3 newPosition = arSessionOrigin.camera.transform.position + arSessionOrigin.camera.transform.forward * 5f;
            newPosition.y -= 0.6f; 

            currentPreview.transform.position = newPosition;


            confirmButton.interactable = false;
        }
    }

    private void ActivatePreview()
    {
        if (UIManager.Instance.CurTarget != null)
        {
            GameObject prefabToInstantiate = GetPrefabToInstantiate();
            if (prefabToInstantiate != null)
            {
                UIManager.Instance.animator.Play("CloseUIList");
                UIManager.Instance.CloseList();
                UIManager.Instance.isListOpen = false;
                UIManager.Instance.curTarget = null;
                UIManager.Instance.Listinterct(false);

                currentPreview = Instantiate(prefabToInstantiate, Vector3.zero, Quaternion.identity);
                placementButton.SetActive(true);
                cancelButton.gameObject.SetActive(true);
                confirmButton.gameObject.SetActive(true);

                if (placementButtonUI != null)
                {
                    placementButtonUI.targetObject = currentPreview;
                }
            }
        }
    }

    private GameObject GetPrefabToInstantiate()
    {
        if (UIManager.Instance.CurTarget.CompareTag("Chair")) return chairPrefab;
        if (UIManager.Instance.CurTarget.CompareTag("Bed")) return bedPrefab;
        if (UIManager.Instance.CurTarget.CompareTag("Desk")) return deskPrefab;
        if (UIManager.Instance.CurTarget.CompareTag("Drawer")) return drawerPrefab;
        if (UIManager.Instance.CurTarget.CompareTag("Light")) return lightPrefab;
        if (UIManager.Instance.CurTarget.CompareTag("Mirror")) return mirrorPrefab;
        if (UIManager.Instance.CurTarget.CompareTag("Sofa")) return sofaPrefab;

        return null;
    }


    public void ConfirmPlacement()
    {
        if (currentPreview != null)
        {
            GameObject placedObject = Instantiate(currentPreview, currentPreview.transform.position, currentPreview.transform.rotation);

            ARAnchor anchor = arAnchorManager.AddAnchor(new Pose(currentPreview.transform.position, currentPreview.transform.rotation));
            if (anchor != null)
            {
                placedObject.transform.parent = anchor.transform;
            }

            Destroy(currentPreview);
            currentPreview = null;
            placementButton.SetActive(false);
            confirmButton.gameObject.SetActive(false);
            cancelButton.gameObject.SetActive(false);
            UIManager.Instance.Listinterct(true);
        }
    }



    private void PreviewCancel()
    {
        if (currentPreview != null)
        {
            Destroy(currentPreview);
            currentPreview = null;
        }
        UIManager.Instance.Listinterct(true);
        placementButton.SetActive(false);
        cancelButton.gameObject.SetActive(false);
    }


}