using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ABUpdateMgr.Instance.CheckUpdate((isOver) =>
        {
            if(isOver)
            {
                print("检测更新结束,隐藏进度条");
            }
            else
            {
                print("网络出错，可以提示玩家去检测网络或者重启游戏");
            }
        }, (str) =>
        {
            //以后可以在这里处理更新加载界面上的显示信息相关的逻辑
            print(str);
        });
        
        // ABUpdateMgr.Instance.DownLoadABCompareFile((isCompareOver) =>
        // {
        //     if (isCompareOver)
        //     {
        //         //解析AB包对比文件
        //         ABUpdateMgr.Instance.GetABCompareFileInfo();
        //         //下载AB包
        //         ABUpdateMgr.Instance.DownLoadABFile((isOver)=>
        //         {
        //             if (isOver)
        //             {
        //                 print("所有AB包下载完成，继续其他逻辑");
        //             }
        //             else
        //             {
        //                 print("AB包下载失败，自行处理");
        //             }
        //         }, (nowNum, maxNum) =>
        //         {
        //             print("下载进度："+nowNum+"/"+maxNum);
        //         });
        //     }
        //     else
        //     {
        //         print("对比文件下载失败，自行处理");
        //     }
        // });
    }
}
