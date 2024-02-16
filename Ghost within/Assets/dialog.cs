using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class dialog : MonoBehaviour
{
    /// <summary>
    /// 对话文本文件，CSV格式
    /// </summary>
    public TextAsset dialogDataFile;
    /// <summary>
    /// 左侧角色图像
    /// </summary>
    public SpriteRenderer spriteLeft;
    /// <summary>
    /// 右侧角色图像
    /// </summary>
    public SpriteRenderer spriteRight;
    /// <summary>
    /// 角色名字文本
    /// </summary>
    public TMP_Text nameText;
    /// <summary>
    /// 对话内容文本
    /// </summary>
    public TMP_Text dialogText;

    // Start is called before the first frame update
    void Start()
    {
        UpdateText("Player", "Hello World");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateText(string _name, string _text)
    {
        nameText.text = _name;
        dialogText.text = _text;
    }
}
