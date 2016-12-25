using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Neutral_ZONE : Zone_Base
{

    public override void Refresh_Card(bool need_Ani = false)
    {
        if (m_Cards.Count > 4)
        {
            My_Debug.LogError(" Neutral count too much .  count : " + m_Cards.Count);
        }

        Card_Base card;
        int delay = 0;
        for (int i = 0; i < m_Cards.Count; i++)
        {
            card = m_Cards[i];
            card.m_Gived_Trm = new Vector3(-9f + i * 6f, 0f, 0f);

            if (card.m_Accurate_Trm != card.m_Gived_Trm ||
                card.m_AccuratePosition != card.m_GivedPosition)
            {
                card.Rotate_By_Position(delay * 0.3f);
                card.LocalMove_To_Gived(delay * 0.3f);
                delay++;
            }
        }
    }


}