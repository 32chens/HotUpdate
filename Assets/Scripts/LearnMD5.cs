using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class LearnMD5 : MonoBehaviour
{
    
    void Start()
    {
        print(GetMD5(Application.dataPath+"/ArtRes/AB/PC/lua"));
    }

    public string GetMD5(string filepath)
    {
        using (FileStream fs = new FileStream(filepath, FileMode.Open))
        {
            //md5对象生成MD5码
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] md5Info = md5.ComputeHash(fs);
            fs.Close();
            
            //将字节数组形式的MD5码转成 16进制字符串
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < md5Info.Length; i++)
            {
                sb.Append(md5Info[i].ToString("x2"));
            }

            return sb.ToString();
        }
    }
}
