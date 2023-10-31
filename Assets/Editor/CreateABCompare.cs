using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEditor;
using UnityEngine;

public class CreateABCompare 
{
    [MenuItem("AB包工具/生成对比文件")]
    public static void CreateABCompareFile()
    {
        DirectoryInfo directoryInfo = Directory.CreateDirectory(Application.dataPath+"/ArtRes/AB/PC/");
        FileInfo[] fileInfos = directoryInfo.GetFiles();
        string abCompare = "";
        foreach (FileInfo fileInfo in fileInfos)
        {
            if (fileInfo.Extension .Equals(""))
            {
                abCompare += fileInfo.Name + " " + fileInfo.Length + " " +
                             GetMD5(fileInfo.FullName) + "|";
            }
        }

        abCompare = abCompare.Substring(0, abCompare.Length - 1);
        File.WriteAllText(Application.dataPath+"/ArtRes/AB/PC/ABCompareInfo.txt", abCompare);
        AssetDatabase.Refresh();
    }
    
    public static string GetMD5(string filepath)
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
