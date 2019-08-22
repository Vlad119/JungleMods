using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Net;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.Xml;
using System.IO;

public class Report_Send : MonoBehaviour, IPointerClickHandler
{
    public static Report_Send self;
    void Awake()
    {
        self = this;
    }

    public void OnPointerClick(PointerEventData eventData)

    {

        Debug.Log(gameObject.name);
        switch (gameObject.name)
        {
            case "send":
                {
                    Fon_Report.self.Send_Report();
                    MainScript.self.ReportMessage.SetActive(true);
                    break;
                }
        }
    }
}
