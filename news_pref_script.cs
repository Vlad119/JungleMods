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

public class news_pref_script : MonoBehaviour {
    public Text date;
    public Text month;
    public Text news_text;
    public Dictionary<string,string> months = new Dictionary<string,string>();
    public LayoutElement layout;

    // Use this for initialization
    void Awake ()
    {
        months.Add("01","января");
        months.Add("02", "февраля");
        months.Add("03", "марта");
        months.Add("04", "апреля");
        months.Add("05", "мая");
        months.Add("06", "июня");
        months.Add("07", "июля");
        months.Add("08", "августа");
        months.Add("09", "сентября");
        months.Add("10", "октября");
        months.Add("11", "ноября");
        months.Add("12", "декабря");
        layout.preferredWidth = MainScript.self.canvas.GetComponent<RectTransform>().sizeDelta.x;
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
    public void SetValue(int value) 
    {
        news_text.text = MainScript.self.news.news[value].body;
        string[] AAAZ = MainScript.self.news.news[value].date.Split('.');
        Debug.Log(AAAZ[0]+"    "+AAAZ[1]);
        date.text = AAAZ[0];
        month.text = months[AAAZ[1]].ToString();
        StartCoroutine(CNt());
    }
    IEnumerator CNt()
    {
        yield return null;
        layout.preferredHeight = news_text.rectTransform.sizeDelta.y*1.35f;
    }

}
