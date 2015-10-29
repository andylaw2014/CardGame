using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler {

	public void OnPointerEnter(PointerEventData eventData) {
		if(eventData.pointerDrag == null)
			return;

		Draggable1 d = eventData.pointerDrag.GetComponent<Draggable1>();
		if(d != null) {
			d.placeholderParent = this.transform;
		}
	}
	
	public void OnPointerExit(PointerEventData eventData) {
		if(eventData.pointerDrag == null)
			return;

		Draggable1 d = eventData.pointerDrag.GetComponent<Draggable1>();
		if(d != null && d.placeholderParent==this.transform) {
			d.placeholderParent = d.parentToReturnTo;
		}
	}
	
	public void OnDrop(PointerEventData eventData) {

		Draggable1 d = eventData.pointerDrag.GetComponent<Draggable1>();
		if(d != null) {
			d.parentToReturnTo = this.transform;
		}

	}
}
