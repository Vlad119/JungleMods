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

public class log_in_script : MonoBehaviour
{
    public static log_in_script self;
    public static string logins;
    public static string pass2;

    void Awake()
    {
        self = this;
    }

    
    public void Button_authorization()
    {
        fon_login.self.BtnLogin();
    }

  
    
}

