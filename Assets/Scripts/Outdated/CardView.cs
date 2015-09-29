using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class CardView : MonoBehaviour, IPointerEnterHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        int count = GameObject.FindGameObjectWithTag("CardView").transform.childCount;
        for (int i = 0; i < count; i++)
            Destroy(GameObject.FindGameObjectWithTag("CardView").transform.GetChild(i).gameObject);
        GameObject clone = Instantiate(gameObject);
        clone.transform.SetParent(GameObject.FindGameObjectWithTag("CardView").transform);
        clone.transform.localScale = new Vector3(2, 2, 2);
        Destroy(clone.GetComponent<Draggable>());
    }
}
