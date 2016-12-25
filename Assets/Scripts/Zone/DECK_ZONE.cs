using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;

public class DECK_ZONE : Zone_Base
{
    [SerializeField]
    private float m_Ramdonness = 20f;

    public override void Refresh_Card(bool need_Ani = false)
    {
        Card_Base card;

        int delay = 0;
        for (int i = 0; i < m_Cards.Count; i++)
        {
            card = m_Cards[i];

            card.m_Gived_Trm = new Vector3(0, i * 0.03f, 0);

            if (need_Ani)
            {
                if (card.m_Accurate_Trm != card.m_Gived_Trm ||
                    card.m_AccuratePosition != card.m_GivedPosition)
                {
                    card.LocalMove_To_Gived(delay * 0.2f);
                    card.Rotate_By_Position(delay * 0.2f);
                    delay++;
                }
            }
            else
            {
                card.transform.localPosition = card.m_Gived_Trm;
                card.transform.localRotation = Quaternion.Euler(card.m_Gived_Rota);
            }
        }
    }



    public void Refresh_Deck_With_Shake()
    {
        Card_Base card;
        m_Cards = Duel_Core.ListRandom<Card_Base>(m_Cards);

        for (int i = 0; i < m_Cards.Count; i++)
        {
            card = m_Cards[i];
            card.m_Gived_Trm = new Vector3(0, i * 0.03f, 0);

            card.transform.localPosition = card.m_Gived_Trm;
            card.transform.DOShakePosition(0.8f, 1.5f, 10, m_Ramdonness);
        }
    }
}