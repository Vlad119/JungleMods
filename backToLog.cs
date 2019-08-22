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


public class backToLog : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)

    {

        Debug.Log(gameObject.name);
        switch (gameObject.name)
        {
            case "back_btn":

                {
                    MainScript.self.fon_reg.SetActive(false);
                    MainScript.self.fon_login.SetActive(true); break;
                }
        }
    }
}
