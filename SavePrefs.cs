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
public class SavePrefs : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        PlayerPrefs.SetString("login",log_in_script.logins);
        PlayerPrefs.SetString("code",log_in_script.pass2);
        PlayerPrefs.Save();
        Debug.Log("Save");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
