using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusWindow : MonoBehaviour
{
    public GameObject window;
    public void OpenWindowGetBonus()
    {
        window.SetActive(true);
        MainScript.self.StartLoadBanch();
        MainScript.self.fon_cenopad.SetActive(true);
        transform.parent.gameObject.SetActive(false);
    }
}
