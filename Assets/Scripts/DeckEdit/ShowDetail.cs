using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShowDetail : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,IPointerClickHandler{
	public Sprite ImageView= null;
	void start(){
		ImageView = new Sprite ();
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		var cardViewController =
			GameObject.FindGameObjectWithTag("DetailCard").GetComponent<CardViewController>();
		cardViewController.SetImage (ImageView);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		var cardViewController =
			GameObject.FindGameObjectWithTag("DetailCard").GetComponent<CardViewController>();
		cardViewController.SetImage (null);
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		GameObject d = eventData.pointerPress;
		var returnGameObject = d.transform.parent.gameObject;
		if (returnGameObject.tag.Equals("ChoiceDeck")) {
			var Chosen = GameObject.FindGameObjectWithTag ("ChosenDeck");
			d.transform.SetParent (Chosen.transform,false);
		}else if(returnGameObject.tag.Equals("ChosenDeck")){
			var Choice = GameObject.FindGameObjectWithTag ("ChoiceDeck");
			d.transform.SetParent (Choice.transform,false);
		}
	}

}


