using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateFolder : MonoBehaviour
{
    private FolderTools folderTools;
    public RectTransform content;
    public GameObject folderObj;

    void Start()
    {
        if (folderTools == null)
        {
            var folderController = new GameObject("FolderController", typeof(FolderTools));
            folderTools = folderController.GetComponent<FolderTools>();
        }
        CreateDrives();
    }

    void Update()
    {

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

        InitContent(content, driveList.Length);

        for (int i = 0; i < driveList.Length; i++)
        {
            CreateObj(folderObj, content, driveList[i]);
        }
    }

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
        return obj;
    }

    /// <summary>
    /// 初始化容器尺寸
    /// </summary>
    /// <param name="content">容器</param>
    /// <param name="num">容器里物体的数量</param>
    private void InitContent(RectTransform content, int num)
    {
        content.sizeDelta = new Vector2(content.sizeDelta.x, (content.GetComponent<GridLayoutGroup>().cellSize.y + content.GetComponent<GridLayoutGroup>().spacing.y) * num);
    }
}