using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FolderTools : MonoBehaviour
{
    public string folderPath = "D:/";
    public string[] driveList, folderList, filesList;
    private Vector2 scrollPosition;

    void Start()
    {
        folderList = GetFolderList(folderPath);
        filesList = GetFilesList(folderPath);
        driveList = GetDriveList();
    }

    /// <summary>
    /// 获取指定路径所有文件夹列表
    /// </summary>
    /// <param name="pathname">路径</param>
    /// <returns>文件夹列表</returns>
    public string[] GetFolderList(string pathname)
    {
        string[] Temp = Directory.GetDirectories(pathname);//读取文件夹
        for (int i = 0; i < Temp.Length; i++)
            Temp[i] = Temp[i].Substring(Temp[i].LastIndexOf(@"\") + 1);
        return Temp;
    }

    /// <summary>
    /// 获取指定路径所有文件列表
    /// </summary>
    /// <param name="pathname">路径</param>
    /// <returns>文件列表</returns>
    public string[] GetFilesList(string pathname)
    {
        string[] Temp = Directory.GetFiles(pathname);//读取文件
        for (int i = 0; i < Temp.Length; i++)
            Temp[i] = Temp[i].Substring(Temp[i].LastIndexOf(@"\") + 1);
        return Temp;
    }

    /// <summary>
    /// 获取指定路径指定格式文件列表
    /// </summary>
    /// <param name="pathname">路径</param>
    /// <param name="fileType">文件格式</param>
    /// <returns>文件列表</returns>
    public string[] GetFilesList(string pathname,string fileType)
    {
        List<string> Temp = new List<string>();
        for (int i = 0; i < GetFilesList(pathname).Length; i++)
        {
            string filename = GetFilesList(pathname)[i];
            if (filename.Substring(filename.LastIndexOf(".") + 1)== fileType)
                Temp.Add(filename);
        }
        return Temp.ToArray();
    }

    /// <summary>
    /// 获取所有本地磁盘
    /// </summary>
    /// <returns>磁盘</returns>
    public string[] GetDriveList()
    {
        string[] letters = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        List<string> temp = new List<string>();
        for (int i = 0; i < letters.Length; i++)
        {
            string path = letters[i] + ":/";
            if (Directory.Exists(path))
                temp.Add(path);
        }
        return temp.ToArray();
    }

    /// <summary>
    /// 创建文件夹
    /// </summary>
    /// <param name="path">路径</param>
    public void CreateFolder(string path)
    {
        if (Directory.Exists(path))
            Debug.Log("存在此文件夹！");
        else
            Directory.CreateDirectory(path);
    }

    /// <summary>
    /// 判断路径是否存在
    /// </summary>
    /// <param name="path">路径</param>
    /// <returns>是否存在</returns>
    public bool IsExists(string path)
    {
        return Directory.Exists(path);
    }
}