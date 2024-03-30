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
    public int dialogIndex;
    
    /// <summary>
    /// �Ի��ı������зָ�
    /// </summary>
    public string[] dialogRows;
    
    /// <summary>
    /// �Ի�������ť
    /// </summary>
    public Button nextButton;

    /// <summary>
    /// ѡ�ťԤ����
    /// </summary>
    public GameObject optionButton;

    /// <summary>
    /// ѡ�ť���ڵ㣬�����Զ�����
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
                Debug.Log("�������");
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
            //�󶨰�ť�¼�
            button.GetComponentInChildren<TMP_Text>().text = cells[4];
            button.GetComponent<Button>().onClick.AddListener
                (
                    delegate 
                    {
                        OnOptionClick(int.Parse(cells[5]));
                        if (cells[6] != "")
                        {
                            Debug.Log("��Ӱ�ť����Ч��");
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
