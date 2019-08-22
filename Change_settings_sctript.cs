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
public class Change_settings_sctript : MonoBehaviour, IPointerClickHandler
{
    public static Change_settings_sctript self;

    Player player = new Player();

    public static string logins;
    public static string pass2;

    void Awake()
    {
        self = this;
    }

    // Use this for initialization
    void Start ()
    {
       
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(gameObject.name);
        switch (gameObject.name)
        {
            case "save_changes_btn":

                {
                    fon_settings.self.Save_Settings();

                    break;
                }
   
        }
    }
}
