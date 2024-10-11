using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeleteButton : MonoBehaviour
{
    [SerializeField] private Button deleteButton;
    private bool isDeleteMode = false;
    private Color originalColor;
    private Color deleteColor = Color.red;

    private void Start()
    {
        originalColor = deleteButton.GetComponent<Image>().color;

        deleteButton.onClick.AddListener(ToggleDeleteMode);
    }

    private void Update()
    {
        if (isDeleteMode && Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (hit.collider.CompareTag("DefaultPlane"))
                    {
                        return;
                    }

                    GameObject targetObject = hit.collider.gameObject;
                    GameObject parentObject = targetObject.transform.parent?.gameObject;

                    if (parentObject != null)
                    {
                        DestroyAllChildren(parentObject);
                    }
                    else
                    {
                        DestroyAllChildren(targetObject);
                    }
                }
            }
        }
    }

    private void ToggleDeleteMode()
    {
        isDeleteMode = !isDeleteMode;

        if (isDeleteMode)
        {
            deleteButton.GetComponent<Image>().color = deleteColor;
        }
        else
        {
            deleteButton.GetComponent<Image>().color = originalColor;
        }
    }

    private void DestroyAllChildren(GameObject parent)
    {
        Destroy(parent);

        foreach (Transform child in parent.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
