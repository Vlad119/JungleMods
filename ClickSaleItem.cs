using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickSaleItem : MonoBehaviour
{
    public string linkImg;
    public GameObject ImgObj;
    public void ShowImg()
    {
        ImgObj = MainScript.self.ImgSale;
        StartCoroutine(LoadImg());
    }
    public IEnumerator LoadImg()
    {
        WWW www = new WWW(linkImg);
        yield return www;
        ImgObj.SetActive(true);
        ImgObj.GetComponent<RawImage>().texture = www.texture;
    }
}
