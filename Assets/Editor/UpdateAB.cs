using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class UpdateAB 
{
    [MenuItem("AB包工具/上传AB包文件")]
    public static void UpdateABFiles()
    {
        DirectoryInfo directory = Directory.CreateDirectory(Application.dataPath+"/ArtRes/AB/PC");
        FileInfo[] infos = directory.GetFiles();
        foreach (FileInfo info in infos)
        {
            //上传文件
            if (info.Extension.Equals("")||info.Extension.Equals(".txt"))
            {
                FtpUpdateFile(info.FullName, info.Name);  
            }
        }
    }

    private async static void FtpUpdateFile(string filepath, string filename)
    {
        await Task.Run(() =>
        {
            try
            {
                FtpWebRequest req = FtpWebRequest.Create(new Uri("ftp://127.0.0.1/AB/PC/"+filename)) as FtpWebRequest;
                req.Credentials = new NetworkCredential("chenlf", "123456");
                req.Proxy = null;
                req.KeepAlive = false;
                req.Method = WebRequestMethods.Ftp.UploadFile;
                req.UseBinary = true;
                Stream requestStream = req.GetRequestStream();
                using (FileStream fileStream = new FileStream(filepath, FileMode.Open))
                {
                    byte[] bytes = new byte[1024];
                    int contentLength = fileStream.Read(bytes,0, bytes.Length);
                    while (contentLength!=0)
                    {
                        requestStream.Write(bytes,0,contentLength);
                        contentLength = fileStream.Read(bytes,0, bytes.Length);
                    }
                    fileStream.Close();
                    requestStream.Close();
                }
                Debug.Log(filename+" 上传成功");
            }
            catch (Exception e)
            {
                Debug.Log(filename + " 上传失败：" + e.Message);
            }
        });
    }
}
