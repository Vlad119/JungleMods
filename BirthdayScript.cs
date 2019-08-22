using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BirthdayScript : MonoBehaviour {
    public static BirthdayScript self;
    public string s1;
    public InputField input;

    public Text output;

    private void Awake()
    {

        self = this;
    }

    public void SetValue()
    {
         s1 = input.text;
        if (s1.Length >= 2)
            s1 = s1.Insert(2, ".");
       if (s1.Length >= 5)
         s1=s1.Insert(5, ".");
        
        Debug.Log(s1);
        output.text = s1;

       
    }
    void Start()
    {
    }

    
}
