using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using JetBrains.Annotations;


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

    /// <summary>
    /// 角色图片列表
    /// </summary>
    public List<Sprite> sprites = new List<Sprite>();

    /// <summary>
    /// 角色名字对应图片的字典
    /// </summary>
    Dictionary<string, Sprite> imageDic = new Dictionary<string, Sprite>();

    /// <summary>
    /// 当前的对话索引值
    /// </summary>
    public int dialogIndex=1;
    
    /// <summary>
    /// 对话文本，按行分割
    /// </summary>
    public string[] dialogRows;
    
    /// <summary>
    /// 对话继续按钮
    /// </summary>
    public Button nextButton;

    // Start is called before the first frame update
    private void Awake()
    {
        imageDic["MC"] = sprites[0];
        imageDic["Nightfall"] = sprites[1];     
    }
    void Start()
    {
        ReadText(dialogDataFile);
        ShowDialogRow();
        //UpdateText("MC", "hello");
        //UpdateImage("Nightfall", false);
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
    /// <summary>
    /// 更新立绘信息
    /// </summary>
    /// <param name="_name">角色名字</param>
    /// <param name="_atLeft">是否出现在左侧</param>
    public void UpdateImage(string _name, string _position)
    {
        if (_position == "Left")
        {
            spriteLeft.sprite = imageDic[_name];
        }
        else if (_position == "Right")
        {
            spriteRight.sprite = imageDic[_name];
        }
    }
    public void ReadText(TextAsset _textAsset)
    {
        dialogRows = _textAsset.text.Split('\n');
        //foreach (var row in rows) 
        //{
        //    string[] cell = row.Split(',');
        //}
        Debug.Log("读取成功");
    }
    public void ShowDialogRow()
    {
        foreach(var row in dialogRows) 
        {
            string[] cells = row.Split(',');
            if (cells[0] == "#" && int.Parse(cells[1]) == dialogIndex) 
            {
                UpdateText(cells[2], cells[4]);
                UpdateImage(cells[2], cells[3]);

                dialogIndex = int.Parse(cells[5]);
                break;
            }
        }
    }

    public void OnClickNext()
    {
        ShowDialogRow();
    }
}
