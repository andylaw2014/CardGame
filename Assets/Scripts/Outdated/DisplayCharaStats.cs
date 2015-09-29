using UnityEngine;
using UnityEngine.UI;

public class DisplayCharaStats : MonoBehaviour {
    
	void OnGUI () {
        Text[] texts = GetComponentsInChildren<Text>();
        foreach (Text text in texts)
        {
            if (text.name.Equals("HP"))
                text.text = GetComponent<CharaCard>().HP.ToString();
            if (text.name.Equals("Attack"))
                text.text = GetComponent<CharaCard>().Attack.ToString();
            if (text.name.Equals("Cost"))
                text.text = GetComponent<CharaCard>().Cost1+" / "+ GetComponent<CharaCard>().Cost2 + " / " + GetComponent<CharaCard>().Cost3;
        }
    }
}
