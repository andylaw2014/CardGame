using System.Collections;
using UnityEngine;

// ChangeMenu Class triggers parent's Animator to "Disappear" and triggers another menu's Animator to "Appear"
public class ChangeMenu : MonoBehaviour
{
    // New menu to be appeared
    public GameObject Menu;

    // Trigger change menu
    public void Change()
    {
        GetComponentInParent<Animator>().SetTrigger("Disappear");
        StartCoroutine(Wait());
        Menu.SetActive(true);
        Menu.GetComponentInParent<Animator>().SetTrigger("Appear");
    }

    // Wait and set parent GameObject inactive.
    // Allow UI animation to be completed before it disappears.
    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f);
        transform.parent.gameObject.SetActive(false);
    }
}