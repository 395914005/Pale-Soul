using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class Zone_Base : MonoBehaviour
{
    public List<Card_Base> m_Cards = new List<Card_Base>();

    public virtual void Remove_Card(Card_Base card)
    {
        m_Cards.Remove(card);
    }

    public virtual void Add_Card(Card_Base card)
    {
        card.transform.SetParent(this.transform);
        m_Cards.Add(card);
    }

    public virtual void Refresh_Card(bool need_Ani = false)
    {
        My_Debug.LogWarning(" Refresh_Card has no method ");
    }
    public virtual void Move_Card_By_Zone_Give_Default_Trm(Card_Base card, bool need_Ani = false, float delay = 0f)
    {
        int index = m_Cards.IndexOf(card);
        if (index < 0)
            return;

        Give_Card_Position(card, index);        // new Vector3(0, index * 0.03f, 0);
        if (need_Ani)
        {
            //if (card.m_Accurate_Trm != card.m_Gived_Trm ||
            //    card.m_AccuratePosition != card.m_GivedPosition)
            card.LocalMove_To_Gived(delay);
        }
        else
        {
            card.transform.localPosition = card.m_Gived_Trm;
            card.transform.localRotation = Quaternion.Euler(card.m_Gived_Rota);
        }
    }

    public virtual void Give_Card_Position(Card_Base card, float index)
    {
        card.m_Gived_Trm = new Vector3(0, index * 0.03f, 0);
    }
}