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

public class fon_reg : MonoBehaviour
{
    public static fon_reg self;
    public InputField login;
    public InputField pass;
    public InputField code;
    public Text error;
    // Use this for initialization
    void Start()
    {
        self = this;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
   