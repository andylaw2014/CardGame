using UnityEngine;
using UnityEngine.UI;

public class HandZoneSpacing : MonoBehaviour
{
    public float defaultSpacing;

    public void RearrangeLayout()
    {
        var mHorizontalLayoutGroup = GetComponent<HorizontalLayoutGroup>();
        var mImages = mHorizontalLayoutGroup.GetComponentsInChildren<LayoutElement>();
        if (mImages.Length > 0)
        {
            var width = GetComponent<RectTransform>().rect.width -
                        (GetComponent<HorizontalLayoutGroup>().padding.left +
                         GetComponent<HorizontalLayoutGroup>().padding.right);
            var calSpacing = (width - mImages.Length*mImages[0].preferredWidth)/(mImages.Length - 1);
            mHorizontalLayoutGroup.spacing = (defaultSpacing < calSpacing) ? defaultSpacing : calSpacing;
        }
    }

    private void Update()
    {
        RearrangeLayout();
    }
}