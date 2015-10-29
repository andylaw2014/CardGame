using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DeckManager : MonoBehaviour {
	private Button EditButton;
	private ArrayList DeckList;
	private Transform[] gameObjects;

	void Start(){
		if(PhotonNetwork.connected)
			PhotonNetwork.Disconnect();
		DeckList = new ArrayList ();
		for (int i = 1; i <21; i++)
		{
			addPrefabtoChoice ("TestCard");
		}
		var S_Array = PlayerPrefsX.GetStringArray ("CardString");
		if (S_Array != null) {
			for (int i = 0; i <S_Array.Length; i++)
			{	
				int index = S_Array[i].IndexOf("(Clone)");
				S_Array[i] = S_Array[i].Substring(0,index);
				addPrefabtoChosen (S_Array[i]);
			}
		}
	}

	 void OnEnterEdit()
	{
		Application.LoadLevel("Edit");
	}

	 void BackToMenu()
	{
		getDeckList ();
		if(PhotonNetwork.connected)
			PhotonNetwork.Disconnect();
		var network = GameObject.Find("Network Manager");
		if(network!=null)
			DestroyImmediate (network);
		Application.LoadLevel("Menu");
	}

	void addPrefabtoChoice(string cardName){
		var panel = GameObject.Find("Card Choice");
		if (panel != null)  
		{
			GameObject obj = (GameObject)Instantiate(Resources.Load(cardName)) ; 
			obj.AddComponent<Draggable1>();
			obj.AddComponent<ShowDetail>();
			obj.transform.SetParent(panel.transform, false);
			if(obj!=null)
			{
				obj.GetComponent<ShowDetail>().ImageView = obj.GetComponent<CardImageController>().Front;
				obj.GetComponent<Image>().sprite = obj.GetComponent<CardImageController>().Front;
			}
		}
	}

	void addPrefabtoChosen(string cardName){
		var panel = GameObject.Find("Chosen");
		if (panel != null)  
		{
			GameObject obj = (GameObject)Instantiate(Resources.Load(cardName)) ; 
			obj.AddComponent<Draggable1>();
			obj.AddComponent<ShowDetail>();
			obj.transform.SetParent(panel.transform, false);
			if(obj!=null)
			{
				obj.GetComponent<ShowDetail>().ImageView = obj.GetComponent<CardImageController>().Front;
				obj.GetComponent<Image>().sprite = obj.GetComponent<CardImageController>().Front;
			}
		}
	}


	public string[] getDeckList(){
		var panel = GameObject.Find("Chosen");
		string[] result;
		gameObjects = panel.GetComponentsInChildren<Transform> (true);
		result=new string[gameObjects.Length-1];
		if (gameObjects != null) {
			foreach (var ob in gameObjects) {
				if(!ob.name.Equals("Chosen")){
					DeckList.Add(ob.name);
				}
			}
			result = (string[])DeckList.ToArray(typeof(string));
		}
		PlayerPrefsX.SetStringArray ("CardString",result);
		return result;
	}
}
