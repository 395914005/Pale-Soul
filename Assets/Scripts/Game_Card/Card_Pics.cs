using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Card_Pics : MonoBehaviour
{
    public Sprite m_Cover;
    public Texture2D m_Cover_Tex;
    public Texture2D m_Joker1;
    public Texture2D m_Joker2;
    public List<Texture2D> m_Pic_List = new List<Texture2D>();


    void Awake()
    {
        My_Debug._DebugEnable = true;
    }

}

