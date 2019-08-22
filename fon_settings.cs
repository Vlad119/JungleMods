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
using System.Text.RegularExpressions;

public class fon_settings : MonoBehaviour {
   // Request request = new Request();
    public Player player = new Player();
    public static fon_settings self;
    public Text phone_number;
    public Text login;
    public InputField fname;
    public InputField sname;
    public InputField birthday;
    public Dropdown branches;


    // Use this for initialization
    void Awake ()
    {
        


    }
    void Start()
    {
        self = this;
        fon_settings.self.phone_number.text = PlayerPrefs.GetString("login");
    }

    private bool CheckDate()
    {
        Regex dateRegex = new Regex(@"^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$");
        if (dateRegex.IsMatch(BirthdayScript.self.s1.Trim()))
            return true;
        else
            return false;
    }


    public void Save_Settings()//сохранение из inputfield
    {
        Debug.Log(MainScript.self.SelectShop.captionText.text);
        if (fon_settings.self.fname.text.Trim() != "" && fon_settings.self.sname.text.Trim() != "" && BirthdayScript.self.s1.Trim().Length == 10)
        {
            
            if (CheckDate())
            {
                MainScript.self.player.login = PlayerPrefs.GetString("login", player.login);
                MainScript.self.player.code = PlayerPrefs.GetString("code", player.code);
                MainScript.self.player.fname = fon_settings.self.fname.text;
                MainScript.self.player.sname = fon_settings.self.sname.text;
                MainScript.self.player.birthday = BirthdayScript.self.s1;
                Request request = new Request(MainScript.self.player);
                MainScript.self.player.branch = Convert.ToInt32(MainScript.self.branches.branches.Find(x => x.title.Contains(MainScript.self.SelectShop.captionText.text)).nid);
                Debug.Log(MainScript.self.servername + "api/v1/player/update?data=" + JsonUtility.ToJson(request));
                WWW www = new WWW(MainScript.self.servername + "api/v1/player/update?data=" + JsonUtility.ToJson(request));

                while (!www.isDone) ;
                Debug.Log(www.text);
                Responce responce = JsonUtility.FromJson<Responce>(www.text);

                Debug.Log(JsonUtility.ToJson(responce));
                MainScript.self.PlLoad();
                MainScript.self.fon_setting.SetActive(false);
            }

            else
            {
                MainScript.self.fon_bn.SetActive(true);
                MainScript.self.fon_bn.GetComponentInChildren<Text>().text = "Введите верную дату рождения!";
            }
            
        }
        else
        {
            MainScript.self.fon_bn.SetActive(true);
        }
    }

    public void Get_Settings()// вывод в inputfield
    {
        Debug.Log("fsd");
        return;
        fon_settings.self.fname.text = PlayerPrefs.GetString("fname", player.fname);
        fon_settings.self.sname.text = PlayerPrefs.GetString("sname", player.sname);
        fon_settings.self.birthday.text = PlayerPrefs.GetString("birthday", player.birthday);
        List<string> addresses = new List<string>();
        foreach (var branch in MainScript.self.branches.branches)
        {
            addresses.Add(branch.address);
        }
        branches.AddOptions(addresses);
        var branc = MainScript.self.branches.branches.Find(x => x.nid == MainScript.self.player.branch.ToString());
        Debug.Log(branc);
        Debug.Break();
        branches.value = MainScript.self.branches.branches.IndexOf(branc);
        Debug.LogError(branches.value);
    }
}
