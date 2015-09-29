using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HandZoneSpacing : MonoBehaviour
{
    public float defaultSpacing;

    public void RearrangeLayout()
    {
        HorizontalLayoutGroup mHorizontalLayoutGroup = GetComponent<HorizontalLayoutGroup>();
        LayoutElement[] mImages = mHorizontalLayoutGroup.GetComponentsInChildren<LayoutElement>();
        if (mImages.Length > 0)
        {
            float width = GetComponent<RectTransform>().rect.width - (GetComponent<HorizontalLayoutGroup>().padding.left + GetComponent<HorizontalLayoutGroup>().padding.right);
            float calSpacing = (width - mImages.Length * mImages[0].preferredWidth) / (mImages.Length - 1);
            mHorizontalLayoutGroup.spacing = (defaultSpacing < calSpacing) ? defaultSpacing : calSpacing;
        }
    }

    void Update()
    {

        RearrangeLayout();
    }
}
