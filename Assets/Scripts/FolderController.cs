using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FolderController : MonoBehaviour
{
    private FolderTools folderTools;    //文件夹工具管理器
    public RectTransform content;       //根节点
    public GameObject folderObj;        //文件夹样式
    public Button last;                 //上一步按钮

    private void Awake()
    {
        if (folderTools == null)
        {
            var folderController = new GameObject("FolderController", typeof(FolderTools));
            folderTools = folderController.GetComponent<FolderTools>();
        }
        CreateDrives();
    }

    private void Start()
    {
        last.onClick.AddListener(delegate { Last(); });         //给上一步按钮添加事件
    }

    /// <summary>
    /// 创建磁盘驱动
    /// </summary>
    private void CreateDrives()
    {
        var driveList = folderTools.GetDriveList();
        if (driveList == null || driveList.Length == 0)
        {
            Debug.LogError("获取本地磁盘失败！");
            return;
        }

        ClearAll(content);

        for (int i = 0; i < driveList.Length; i++)
            CreateObj(folderObj, content, driveList[i]);
    }

    string currentPath = "";
    /// <summary>
    /// 实例化创建预制体
    /// </summary>
    /// <param name="go"></param>
    /// <param name="parent"></param>
    private GameObject CreateObj(GameObject go, Transform parent, string str)
    {
        var obj = Instantiate(go, parent) as GameObject;
        obj.transform.localPosition = new Vector3(obj.transform.position.x, obj.transform.position.y, 0);
        obj.transform.localRotation = Quaternion.Euler(Vector3.zero);
        obj.transform.localScale = Vector3.one;
        obj.transform.GetComponentInChildren<Text>().text = str;
        obj.GetComponent<Button>().onClick.AddListener(delegate { CreateFolderAndFiles(obj); });
        return obj;
    }

    /// <summary>
    /// 清空之前所有
    /// </summary>
    /// <param name="content">容器</param>
    private void ClearAll(RectTransform content)
    {
        for (int i = 0; i < content.childCount; i++)
            Destroy(content.GetChild(i).gameObject);
    }

    List<string> allPath = new List<string>();
    /// <summary>
    /// 创建文件夹和文件
    /// </summary>
    /// <param name="sender"></param>
    private void CreateFolderAndFiles(GameObject sender)
    {
        string path = sender.GetComponentInChildren<Text>().text;
        if (path.Length > 3)
        {
            string temp = path.Substring(0, 3);
            if (Array.IndexOf(folderTools.GetDriveList(), temp) != -1)
                path = path.Substring(3, path.Length - 3);
        }

        if (currentPath != "")
            path = currentPath + "/" + path;
        allPath.Add(path);
        CreateFolder(path);
        CreateFiles(path);
        currentPath = path;
    }

    /// <summary>
    /// 上一步
    /// </summary>
    private void Last()
    {
        if (allPath == null || allPath.Count == 0)
            return;
        else if (allPath.Count == 1)
        {
            CreateDrives();
            currentPath = "";
            return;
        }
        string lastPath = "";
        for (int i = 0; i < allPath.Count; i++)
            lastPath = allPath[allPath.Count - 2];
        allPath.Remove(allPath[allPath.Count - 1]);
        CreateFolder(lastPath);
        CreateFiles(lastPath);
        currentPath = lastPath;
    }

    int index = 0;
    /// <summary>
    /// 创建文件夹
    /// </summary>
    /// <param name="path">路径</param>
    private void CreateFolder(string path)
    {
        var folderList = folderTools.GetFolderList(path);
        if (folderList == null)
        {
            Debug.LogError("获取" + path + "下文件夹失败！");
            return;
        }

        index = folderList.Length;
        ClearAll(content);

        for (int i = 0; i < folderList.Length; i++)
            CreateObj(folderObj, content, folderList[i]);
    }

    /// <summary>
    /// 创建文件
    /// </summary>
    /// <param name="path">路径</param>
    private void CreateFiles(string path)
    {
        var filesList = folderTools.GetFilesList(path);
        if (filesList == null)
        {
            Debug.LogError("获取" + path + "下文件失败！");
            return;
        }

        for (int i = 0; i < filesList.Length; i++)
            CreateObj(folderObj, content, filesList[i]);
    }
}