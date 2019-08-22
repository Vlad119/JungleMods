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
using System.Security.Cryptography;


using ZXing;
using ZXing.QrCode;

public class QRscript : MonoBehaviour
{

    public static QRscript self;

    public static Color32[] Encode(string textForEncoding, int width, int height)
    {
        var writer = new BarcodeWriter
        {
            Format = BarcodeFormat.QR_CODE,
            Options = new QrCodeEncodingOptions
            {
                Height = height,
                Width = width
            }
        };
        return writer.Write(textForEncoding);
    }

    public Texture2D generateQR(string text)
    {
        var encoded = new Texture2D(256, 256);
        var color32 = Encode(text, encoded.width, encoded.height);
        encoded.SetPixels32(color32);
        encoded.Apply();
        return encoded;
    }


    void Awake()
    {
        self = this;
    }
    void Start()
    {
        
    }

    public void ChangeQrCode()
    {
        string s;
        Texture2D qrPic = null;
        Debug.Log(MainScript.self.player.login.ToString() + MainScript.self.player.code.ToString());
        s = Encryptor.MD5Hash(MainScript.self.player.login.ToString() + MainScript.self.player.code.ToString());
        Debug.Log(s);
        qrPic = generateQR(s);

        MainScript.self.qrcode.sprite = Sprite.Create(qrPic, new Rect(0.0f, 0.0f, qrPic.width, qrPic.height), new Vector2(0.5f, 0.5f), 100.0f);

    }


    

    public static class Encryptor
    {
        /// <returns>Возвраает хешированный пароль</returns>
        public static string MD5Hash(string text)
        {
            MD5 mD5 = new MD5CryptoServiceProvider();
            //Переводим текст в хэш код
            mD5.ComputeHash(Encoding.ASCII.GetBytes(text));
            //Записываем результат хеширования
            byte[] result = mD5.Hash;

            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                // х2 - сообщает компилятору как должна быть отформатирована строка, в 16-ой системе
                stringBuilder.Append(result[i].ToString("x2"));
            }

            return stringBuilder.ToString();
        }
    }
}