using UnityEngine;
using UnityEngine.UI;

// For ScrollView use only.
public class ScrollViewHeight : MonoBehaviour
{
    // Adjust size of ScrollView according to number of items.
    private void LateUpdate()
    {
        var mRectTransform = GetComponent<RectTransform>();
        var padding = GetComponent<VerticalLayoutGroup>().padding;
        float height = padding.top + padding.bottom; // BasicPadding
        foreach (Transform child in transform)
            height += child.GetComponent<LayoutElement>().preferredHeight;
        mRectTransform.sizeDelta = new Vector2(mRectTransform.sizeDelta.x, height);
    }
}