/*********************************************************************************
 *Copyright(C) 2016 by Youki
 *All rights reserved.
 *Author:       Youki
 *Version:      1.0
 *UnityVersion：5.3.3f1
 *Date:         2016-10-03
 *Description:  
**********************************************************************************/
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

public static class Card_Pic_Helper
{
    public static bool m_Need_Init = true;
    public static Texture2D m_My_Back_T2D;


    private static Sprite m_My_Back_Sprite;
    public static void Init()
    {
        m_Need_Init = false;
        m_My_Back_T2D = Get_T2D_By_String("cover");
        //m_My_Back_Sprite = Sprite.Create(m_My_Back_T2D, new Rect(0, 0, m_My_Back_T2D.width, m_My_Back_T2D.height), Vector3.zero);

    }

    public static void Set_Image_Pic_By_Code(Int32 code_, Image img)
    {
        if (m_Need_Init)
        {
            Init();
        }

        string pic_path;

        bool find_pic = Get_Pic_Path(code_, out pic_path);

        if (find_pic)
        {
            try
            {
                //读取本地图片
                FileStream fileStream = new FileStream(pic_path, FileMode.Open, FileAccess.Read);
                fileStream.Seek(0, SeekOrigin.Begin);
                byte[] data = new byte[fileStream.Length];
                fileStream.Read(data, 0, (int)fileStream.Length);
                fileStream.Close();
                fileStream.Dispose();
                fileStream = null;

                Texture2D pic = new Texture2D(1200, 675);
                pic.LoadImage(data);
                img.sprite = Sprite.Create(pic, new Rect(0, 0, pic.width, pic.height), Vector3.zero);

                return;
            }
            catch { }
        }

        //最默认的情况
        img.sprite = m_My_Back_Sprite;
    }

    public static Texture2D Get_T2D_By_Code(Int32 code_, uint player_id = 0)
    {
        return Get_T2D_By_String(code_.ToString(), player_id);
    }

    public static Texture2D Get_T2D_By_String(string name, uint player_id = 0)
    {
        if (m_Need_Init)
        {
            Init();
        }

        string pic_path;

        bool find_pic = Get_Pic_Path(name, out pic_path);

        if (find_pic)
        {
            try
            {
                //读取本地图片
                FileStream fileStream = new FileStream(pic_path, FileMode.Open, FileAccess.Read);
                fileStream.Seek(0, SeekOrigin.Begin);
                byte[] data = new byte[fileStream.Length];
                fileStream.Read(data, 0, (int)fileStream.Length);
                fileStream.Close();
                fileStream.Dispose();
                fileStream = null;

                Texture2D pic = new Texture2D(1024, 600);
                pic.LoadImage(data);
                return pic;
            }
            catch {

                My_Debug.LogError("ee");
            }
        }
        return null;

    }

    private static bool Get_Pic_Path(Int32 code_, out string pic_path)
    {
        return Get_Pic_Path(code_.ToString(), out pic_path);
    }

    private static bool Get_Pic_Path(string name, out string pic_path)
    {
        pic_path = My_Config.Pics_Path + My_Config.path_diagonal + name + My_Config.png_suffix;

        bool find_pic = false;

        if (File.Exists(pic_path))
        {
            find_pic = true;
        }
        else
        {
            pic_path = My_Config.Pics_Path + My_Config.path_diagonal + name + My_Config.jpg_suffix;
            if (File.Exists(pic_path))
            {
                find_pic = true;
            }
        }

        return find_pic;
    }

}
