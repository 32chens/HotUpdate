using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class MoveABToSA
{
    [MenuItem("AB包工具/移动资源到StreamingAssets")]
    public static void MoveABToStreamingAssets()
    {
        //获取Project窗口选择的资源信息（Selection）
        Object[] objs = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
        if (objs.Length == 0)
        {
            return;
        }
        
        if (!Directory.Exists(Application.streamingAssetsPath))
        {
            Directory.CreateDirectory(Application.streamingAssetsPath);
        }

        string abCompareInfo = "";
        //遍历选中的资源
        foreach (Object obj in objs)
        {
            string assetPath = AssetDatabase.GetAssetPath(obj);
            string fileName = assetPath.Substring(assetPath.LastIndexOf('/'));
            FileInfo fileInfo = new FileInfo(Application.streamingAssetsPath + fileName);
            if (!fileInfo.Extension.Equals("") && !fileInfo.Extension.Equals("txt"))
            {
                continue;
            }
            //将选择到的文件复制到StreamingAssets文件夹中（AssetDatabase）
            AssetDatabase.CopyAsset(assetPath, "Assets/StreamingAssets" + fileName);
            
            //拼接资源对比文件内容
            abCompareInfo += fileInfo.Name + " " + fileInfo.Length + " " +
                             CreateABCompare.GetMD5(Application.streamingAssetsPath + fileName);
            abCompareInfo += '/';
        }
        //为StreamingAssets文件夹中的AB包文件信息写入资源对比文件中（文件写入）
        abCompareInfo = abCompareInfo.Substring(0, abCompareInfo.Length - 1);
        File.WriteAllText(Application.streamingAssetsPath + "/ABCompareInfo.txt", abCompareInfo);
        AssetDatabase.Refresh();
    }
}
