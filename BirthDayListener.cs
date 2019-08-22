using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class BirthDayListener : EventTrigger
{
    public GameObject text;

    public override void OnSelect(BaseEventData eventData)
    {
        text = this.transform.GetChild(3).gameObject;
        text.SetActive(false);
    }
}
