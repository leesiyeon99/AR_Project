using UnityEngine;
using UnityEngine.UI;

public class PlacementButtonUI : MonoBehaviour
{
    public GameObject targetObject;

    public void ScaleUp()
    {
        if (targetObject != null)
        {
            targetObject.transform.localScale += new Vector3(0.03f, 0.03f, 0.03f);
        }
    }

    public void ScaleDown()
    {
        if (targetObject != null)
        {
            Vector3 currentScale = targetObject.transform.localScale;
            currentScale -= new Vector3(0.05f, 0.05f, 0.05f);
            if (currentScale.x > 0 && currentScale.y > 0 && currentScale.z > 0)
            {
                targetObject.transform.localScale = currentScale;
            }
        }
    }

    public void RotatePreview()
    {
        if (targetObject != null)
        {
            targetObject.transform.Rotate(Vector3.up, 20f);
        }
    }
}
