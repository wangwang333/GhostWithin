using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
