using UnityEngine;
using System.Collections;

public class AppleManager : MonoBehaviour
{
    // Singleton
    private static AppleManager instance;

    public static AppleManager Ins {
        get { return instance; }
    }

    // Apples
    [SerializeField]
    private int collectedApples;

    public int CollectedApples {
        get { return collectedApples; }
        set {
            collectedApples = value;

            if (collectedApples == apples.childCount)
                GameFlowManager.Ins.WinGame ();
        }
    }


    public Transform apples;

    void Awake ()
    {
        // Singleton assignment
        instance = this;
    }

    public void ActivateAllApples ()
    {
        foreach (Transform apple in apples) {
            apple.gameObject.SetActive (true);
        }
    }

    public void DeactivateAllApples ()
    {
        foreach (Transform apple in apples) {
            apple.gameObject.SetActive (false);
        }
    }
}
