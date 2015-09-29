using UnityEngine;

// Generate unique ID
public class UniqueID : MonoBehaviour
{
    public int ID;
    private static int maxID;

    void Start()
    {
        maxID = 0;
    }

    public static int GetID()
    {
        maxID++;
        return maxID - 1;
    }

}
