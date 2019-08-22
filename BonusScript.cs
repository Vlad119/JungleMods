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

public class BonusScript : MonoBehaviour
{

    public GameObject bonus_pref;
    public GameObject TargetContent;
    public static BonusScript fon_bonus;
    public Text count_bonus;

    public List<Bonus_pref> Elems = new List<Bonus_pref>();
    public GameObject yet;

    // Use this for initialization
    void Start ()
    {
		fon_bonus = this;
	}
    
    /*
    public void ChangeBonus()
    {
        Debug.Log("ChangeBonus");
        StartCoroutine(MainScript.self.ChangeBonus(ChangeBonuscontinue));
    }

    public void ChangeBonuscontinue()
    {
        count_bonus.text = MainScript.self.bonus.sum.ToString();
    }

    */


    // выгрузка количества бонусов
    public void ShowBonus()
    {
        Debug.Log("ShowBonus");
        StartCoroutine(MainScript.self.LoadBonus(ShowBonuscontinue));
    }
    public Coroutine BonusIE = null;
    public void ShowBonuscontinue()
    {
        if (BonusIE != null)
        {
            StopCoroutine(BonusIE);
        }
        BonusIE = StartCoroutine(bonusAnim());
    }

    public IEnumerator bonusAnim()
    {
        count_bonus.text =""+ 0;
        //yield return new WaitForSeconds(5f);
        for (int i = 0; i  < 10; i++)
        {
            count_bonus.text = ""+(i*(int)MainScript.self.bonus.sum) / 10 ;
            yield return new WaitForSeconds(0.1f);
        }
        count_bonus.text = "" + MainScript.self.bonus.sum;
    }


    void AllClose()
    {
        foreach (Bonus_pref val in Elems)
        {
            Destroy(val.gameObject);
        }
        Elems.Clear();
    }

    public void Loadcontinue()
    {
        AllClose();
        if (MainScript.self.fon_bonus!= null)
        {
            for (int j = 0; j < MainScript.self.news.news.ToArray().Length; j++)
            {
                GameObject InstMis = Instantiate(bonus_pref);
                InstMis.transform.SetParent(TargetContent.transform, false);
                Elems.Add(InstMis.GetComponent<Bonus_pref>());
                InstMis.GetComponent<Bonus_pref>().SetValue(j);
                yet.transform.SetAsLastSibling();
            }
        }
    }
}
