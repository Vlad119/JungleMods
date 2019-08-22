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


public class closeButton : MonoBehaviour, IPointerClickHandler
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(gameObject.name);
        switch (gameObject.name)
        {
            case "close_btn":
                {
                    MainScript.self.fon_nav.SetActive(false);
                    MainScript.self.fon_setting.SetActive(false);
                    MainScript.self.fon_report.SetActive(false);
                    MainScript.self.fon_cenopad.SetActive(true);
                    break;
                }
            case "log_out_btn":
                {

                    MainScript.self.fon_nav.SetActive(false);
                    MainScript.self.fon_setting.SetActive(false);
                    MainScript.self.fon_login.SetActive(true);
                    break;
                }

        
            case "ok_btn":
                {

            MainScript.self.fon_bn.SetActive(false);
            
            break;
        }
    }
    }
}
