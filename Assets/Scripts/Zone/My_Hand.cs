using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class My_Hand : Zone_Base
{
    /// <summary> 手牌最居中的位置</summary> 
    public Vector3 m_Card_Center;

    /// <summary> 排序的圆心</summary> 
    public Vector3 m_Center;

    /// <summary> 半径</summary> 
    public float m_Radius;

    /// <summary> 扇形最大角度（总角度）</summary> 
    public float m_Max_Angle;


    public override void Refresh_Card(bool need_Ani = false)
    {
        m_Center = m_Card_Center;
        m_Center.z = m_Card_Center.z - m_Radius;

        int count = m_Cards.Count;

        if (count < 4)
        {
            print(" count < 4 !!  ");
            return;
        }
        //  步进角度
        float pre_angle = m_Max_Angle / (count - 1);

        //  第一张卡的角度
        float first_angle = m_Max_Angle * -0.5f;

        Vector3 temp_pos;
        float temp_angle;
        Card_Base card;

        for (int i = 0; i < count; i++)
        {
            temp_angle = first_angle + pre_angle * i;

            temp_pos = m_Center;
            temp_pos.x += m_Radius * Mathf.Sin(temp_angle / 180f * 3.1415f); //  切记 angle 是数值  三角函数用到时候要手动转成弧度
            temp_pos.z += m_Radius * Mathf.Cos(temp_angle / 180f * 3.1415f);
            temp_pos.y = i * 0.03f;     //  设置 Y 防遮挡

            card = m_Cards[i];
            card.m_Gived_Trm = temp_pos;

            Vector3 rota = card.m_Gived_Rota;
            rota.z = temp_angle;
            card.m_Gived_Rota = rota;

            card.Stop_All_Move();
            card.LocalMove_To_Gived(0.6f);
        }
    }

    public override void Give_Card_Position(Card_Base card, float index)
    {
        Refresh_Card(true);
        //int count = m_Cards.Count;
        //if (count < 4)
        //{
        //    print(" count < 4 !!  ");

        //    return;
        //}
        ////  步进角度
        //float pre_angle = m_Max_Angle / (count - 1);

        ////  第一张卡的角度
        //float first_angle = m_Max_Angle * -0.5f;

        //Vector3 temp_pos;
        //float temp_angle;


        //temp_angle = first_angle + pre_angle * index;

        //temp_pos = m_Center;
        //temp_pos.x += m_Radius * Mathf.Sin(temp_angle);
        //temp_pos.z += m_Radius * Mathf.Cos(temp_angle);

        //card.m_Gived_Trm = temp_pos;

        //Vector3 rota = card.m_Gived_Rota;
        //rota.z = temp_angle;
        //card.m_Gived_Rota = rota;
    }

}