using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using JetBrains.Annotations;


public class dialog : MonoBehaviour
{
    /// <summary>
    /// �Ի��ı��ļ���CSV��ʽ
    /// </summary>
    public TextAsset dialogDataFile;

    /// <summary>
    /// ����ɫͼ��
    /// </summary>
    public SpriteRenderer spriteLeft;

    /// <summary>
    /// �Ҳ��ɫͼ��
    /// </summary>
    public SpriteRenderer spriteRight;

    /// <summary>
    /// ��ɫ�����ı�
    /// </summary>
    public TMP_Text nameText;

    /// <summary>
    /// �Ի������ı�
    /// </summary>
    public TMP_Text dialogText;

    /// <summary>
    /// ��ɫͼƬ�б�
    /// </summary>
    public List<Sprite> sprites = new List<Sprite>();

    /// <summary>
    /// ��ɫ���ֶ�ӦͼƬ���ֵ�
    /// </summary>
    Dictionary<string, Sprite> imageDic = new Dictionary<string, Sprite>();

    /// <summary>
    /// ��ǰ�ĶԻ�����ֵ
    /// </summary>
    public int dialogIndex=1;
    
    /// <summary>
    /// �Ի��ı������зָ�
    /// </summary>
    public string[] dialogRows;
    
    /// <summary>
    /// �Ի�������ť
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
    /// ����������Ϣ
    /// </summary>
    /// <param name="_name">��ɫ����</param>
    /// <param name="_atLeft">�Ƿ���������</param>
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
        Debug.Log("��ȡ�ɹ�");
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
