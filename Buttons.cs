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

public class Buttons : MonoBehaviour, IPointerClickHandler
{
    void Start()
    {

    }
    void Update()
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //Request request = new Request();
        //request.api_key = "fa31b554-0cfc-4cd8-ae9f-06f6db0aa7f0";

        Debug.Log(gameObject.name);
        switch (gameObject.name)
        {

            case "reg_btn":
                {
                    MainScript.self.fon_login.SetActive(false);
                    MainScript.self.fon_reg.SetActive(true);
                    break;

                }

        }
    }









}
