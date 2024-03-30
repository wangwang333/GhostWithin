using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using JetBrains.Annotations;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using System.Text.RegularExpressions;

[System.Serializable]

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
    public int dialogIndex;
    
    /// <summary>
    /// 对话文本，按行分割
    /// </summary>
    public string[] dialogRows;
    
    /// <summary>
    /// 对话继续按钮
    /// </summary>
    public Button nextButton;

    /// <summary>
    /// 选项按钮预制体
    /// </summary>
    public GameObject optionButton;

    /// <summary>
    /// 选项按钮父节点，用于自动排列
    /// </summary>
    public Transform buttonGroup;

    public List<Person> people= new List<Person>();

    // Start is called before the first frame update
    private void Awake()
    {
        imageDic["Player"] = sprites[0];
        imageDic["Nightfall"] = sprites[1];  
        Person person = new Person();
        person.name = "Nightfall";
        people.Add(person);
        Person player= new Person();
        player.name = "Player";
        people.Add(player);
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
        for(int i = 0; i < dialogRows.Length; i++) 
        {
            string[] cells = dialogRows[i].Split(',');
            if (cells[0] == "#" && int.Parse(cells[1]) == dialogIndex)
            {
                UpdateText(cells[2], cells[4]);
                UpdateImage(cells[2], cells[3]);

                dialogIndex = int.Parse(cells[5]);
                nextButton.gameObject.SetActive(true);
                break;
            }
            else if (cells[0] == "&" && int.Parse(cells[1]) == dialogIndex) 
            {
                nextButton.gameObject.SetActive(false);
                GenerateOption(i);
            }
            else if (cells[0]=="END"&& int.Parse(cells[1])== dialogIndex) 
            {
                Debug.Log("剧情结束");
            }
        }
    }

    public void OnClickNext()
    {
        ShowDialogRow();
    }
    public void GenerateOption(int _index)
    {
        string[] cells = dialogRows[_index].Split(',');
        if (cells[0]=="&") 
        {
            GameObject button = Instantiate(optionButton, buttonGroup);
            //绑定按钮事件
            button.GetComponentInChildren<TMP_Text>().text = cells[4];
            button.GetComponent<Button>().onClick.AddListener
                (
                    delegate 
                    {
                        OnOptionClick(int.Parse(cells[5]));
                        if (cells[6] != "")
                        {
                            Debug.Log("添加按钮附加效果");
                            string[] effect = cells[6].Split("@");

                            cells[7] = Regex.Replace(cells[7], @"[\r\n]", "");
                            OptionEffect(effect[0], int.Parse(effect[1]), cells[7]);
                        }
                    }
                 );
            GenerateOption(_index + 1);
        }
        
    }
    public void OnOptionClick(int _id) 
    {
        dialogIndex = _id;
        ShowDialogRow();
        for (int i = 0; i < buttonGroup.childCount; i++)
        { 
            Destroy(buttonGroup.GetChild(i).gameObject);
        }
    }
    public void OptionEffect(string _effect, int _param, string _target)
    {
        if (_effect == "Friendship")
        {
            foreach (var person in people)
            {
                if (person.name == _target)
                {
                    person.Friendship += _param;
                }
            }
        }
        else if (_effect == "Dark")
        {
            foreach (var person in people)
            {
                if (person.name == _target)
                {
                    person.Dark += _param;
                }
            }
        }
    }
}
