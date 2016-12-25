using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;

public class Duel_Core
{
    #region 单例
    /// <summary> 核心逻辑的单例</summary> 
    public static Duel_Core _instance;
    public Duel_Core()
    {
        if (null == _instance)
            _instance = this;
    }
    #endregion

    public Phase m_Current_Phase;
    public ushort m_Current_Player;

    public List<Card_Base> m_DeckCards;
    public List<Card_Base> m_MyHandCards;
    public List<Card_Base> m_OpHandCards;
    public List<Card_Base> m_NeutralCards;
    public List<Card_Base> m_MydianshuZoneCards;
    public List<Card_Base> m_MylingkeZoneCards;
    public List<Card_Base> m_OpdianshuZoneCards;
    public List<Card_Base> m_OplingkeZoneCards;
    public List<Card_Base> m_dianshuGraveCards;
    public List<Card_Base> m_lingkeGraveCards;

    public Game_Mgr m_Mgr;
    /// <summary> 回合计数</summary> 
    public int m_Turn = 1;

    public void Init_Game()
    {
        m_Mgr = Singleton<Game_Mgr>.Instance;

        Set_Card_List();

        Create_Cards();

        m_DeckCards = ListRandom<Card_Base>(m_DeckCards);

        Refresh_All_Zone();

    }

    /// <summary> 从卡组发4张牌到中立野怪区</summary> 
    public void Init_Neutral(int count = 4)
    {
        List<Card_Base> cards = Get_DECKSHF_Card(0, count);

        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].m_GivedPosition = Card_Pos.FaceDownDefence;
        }

        MoveCards(Game_Loc.Deck, Game_Loc.Neutral, cards.ToArray());
    }

    /// <summary> 移动卡片组</summary> 
    public void MoveCards(Game_Loc from, Game_Loc to, params Card_Base[] cards)
    {
        Zone_Base zone_from = GetZoneByLoc(from);
        Zone_Base zone_to = GetZoneByLoc(to);

        Card_Base card;
        for (int i = 0; i < cards.Length; i++)
        {
            card = cards[i];

            zone_from.Remove_Card(card);

            zone_to.Add_Card(card);
        }

        zone_from.Refresh_Card();
        zone_to.Refresh_Card();

    }

    public void Move_SingleCard(Game_Loc from, Game_Loc to, Card_Base card, bool need_Ani = false, float delay = 0f)
    {
        Zone_Base zone_from = GetZoneByLoc(from);
        Zone_Base zone_to = GetZoneByLoc(to);

        zone_from.Remove_Card(card);
        //zone_from.Refresh_Card();

        zone_to.Add_Card(card);
        zone_to.Move_Card_By_Zone_Give_Default_Trm(card, need_Ani, delay);
    }

    /// <summary> 返回卡组底部第几张卡</summary> 
    public List<Card_Base> Get_DECKBOT_Card(int index = 0, int count = 1)
    {
        List<Card_Base> cards = new List<Card_Base>();

        for (int i = 0; i < m_DeckCards.Count; i++)
        {
            cards.Add(m_DeckCards[i]);
            count--;
            if (count < 1)
                break;
        }
        return cards;
    }
    /// <summary> 返回卡组顶部第几张卡</summary> 
    public List<Card_Base> Get_DECKSHF_Card(int index = 0, int count = 1)
    {
        List<Card_Base> cards = new List<Card_Base>();

        for (int i = m_DeckCards.Count - 1; i > 0; i--)
        {
            cards.Add(m_DeckCards[i]);
            count--;
            if (count < 1)
                break;
        }
        return cards;

    }

    /// <summary> 刷新所有区域。感觉有点多余</summary> 
    public void Refresh_All_Zone()
    {
        Singleton<Zone_Mgr>.Instance.m_DeckZone.Refresh_Card();
        Singleton<Zone_Mgr>.Instance.m_MyHandZone.Refresh_Card();
        Singleton<Zone_Mgr>.Instance.m_OpHandZone.Refresh_Card();
        Singleton<Zone_Mgr>.Instance.m_NeutralZone.Refresh_Card();
        Singleton<Zone_Mgr>.Instance.m_MydianshuZone.Refresh_Card();
        Singleton<Zone_Mgr>.Instance.m_MylingkeZone.Refresh_Card();
        Singleton<Zone_Mgr>.Instance.m_OpdianshuZone.Refresh_Card();
        Singleton<Zone_Mgr>.Instance.m_OplingkeZone.Refresh_Card();
        Singleton<Zone_Mgr>.Instance.m_dianshuGraveZone.Refresh_Card();
        Singleton<Zone_Mgr>.Instance.m_lingkeGraveZone.Refresh_Card();
    }

    /// <summary> 初始化一次List列表 </summary> 
    private void Set_Card_List()
    {
        m_DeckCards = GetZoneByLoc(Game_Loc.Deck).m_Cards;
        m_MyHandCards = GetZoneByLoc(Game_Loc.MyHand).m_Cards;
        m_OpHandCards = GetZoneByLoc(Game_Loc.OpHand).m_Cards;
        m_NeutralCards = GetZoneByLoc(Game_Loc.Neutral).m_Cards;
        m_MydianshuZoneCards = GetZoneByLoc(Game_Loc.MydianshuZone).m_Cards;
        m_MylingkeZoneCards = GetZoneByLoc(Game_Loc.MylingkeZone).m_Cards;
        m_OpdianshuZoneCards = GetZoneByLoc(Game_Loc.OpdianshuZone).m_Cards;
        m_OplingkeZoneCards = GetZoneByLoc(Game_Loc.OplingkeZone).m_Cards;
        m_dianshuGraveCards = GetZoneByLoc(Game_Loc.dianshuGrave).m_Cards;
        m_lingkeGraveCards = GetZoneByLoc(Game_Loc.lingkeGrave).m_Cards;
    }

    /// <summary> 创建卡片</summary> 
    private void Create_Cards()
    {
        GameObject go;
        Card_Base card;
        for (int point = 0; point < 12; point++)    //  A ~ K
        {
            for (int mark = 0; mark < 4; mark++)    //  4花色
            {
                go = Create_Card_Go();
                card = go.GetComponent<Card_Base>();
                card.m_Point = (Card_Point)point;
                card.m_Mark = (Card_Mark)mark;
                m_DeckCards.Add(card);
            }
        }

        //  joker 1
        go = Create_Card_Go();
        card = go.GetComponent<Card_Base>();
        card.m_Point = Card_Point._JOKER1;
        card.m_Mark = Card_Mark.COUNT;
        m_DeckCards.Add(card);

        //  joker 2
        go = Create_Card_Go();
        card = go.GetComponent<Card_Base>();
        card.m_Point = Card_Point._JOKER2;
        card.m_Mark = Card_Mark.COUNT;
        m_DeckCards.Add(card);

        //  初始刷新一遍卡图
        for (int i = 0; i < m_DeckCards.Count; i++)
        {
            card = m_DeckCards[i];
            card.m_Location = Game_Loc.Deck;
            card.m_AccuratePosition = Card_Pos.FaceDownAttack;
            card.RefreshCard();

        }
    }

    private GameObject Create_Card_Go()
    {
        GameObject go = Game_Mgr.Create_Go(m_Mgr.m_Card_Prefab, Vector3.zero, Vector3.zero, false, m_Mgr.m_Card_Parent, true);
        return go;
    }

    /// <summary> 打乱List排序</summary> 
    public static List<T> ListRandom<T>(List<T> myList)
    {

        int index = 0;
        T temp;
        for (int i = 0; i < myList.Count; i++)
        {

            index = Random.Range(0, myList.Count - 1);
            if (index != i)
            {
                temp = myList[i];
                myList[i] = myList[index];
                myList[index] = temp;
            }
        }
        return myList;
    }

    private Zone_Base GetZoneByLoc(Game_Loc loc)
    {
        Zone_Base zone = null;

        switch (loc)
        {
            case Game_Loc.Deck:
                zone = Singleton<Zone_Mgr>.Instance.m_DeckZone;
                break;
            case Game_Loc.MyHand:
                zone = Singleton<Zone_Mgr>.Instance.m_MyHandZone;
                break;
            case Game_Loc.OpHand:
                zone = Singleton<Zone_Mgr>.Instance.m_OpHandZone;
                break;
            case Game_Loc.Neutral:
                zone = Singleton<Zone_Mgr>.Instance.m_NeutralZone;
                break;
            case Game_Loc.MydianshuZone:
                zone = Singleton<Zone_Mgr>.Instance.m_MydianshuZone;
                break;
            case Game_Loc.MylingkeZone:
                zone = Singleton<Zone_Mgr>.Instance.m_MylingkeZone;
                break;
            case Game_Loc.OpdianshuZone:
                zone = Singleton<Zone_Mgr>.Instance.m_OpdianshuZone;
                break;
            case Game_Loc.OplingkeZone:
                zone = Singleton<Zone_Mgr>.Instance.m_OplingkeZone;
                break;
            case Game_Loc.dianshuGrave:
                zone = Singleton<Zone_Mgr>.Instance.m_dianshuGraveZone;
                break;
            case Game_Loc.lingkeGrave:
                zone = Singleton<Zone_Mgr>.Instance.m_lingkeGraveZone;
                break;
            default:
                break;
        }

        return zone;
    }
    /// <summary> 初始发牌，参数是先手玩家和后手玩家要发的牌数</summary> 
    public void Init_Player_Card(int fp_count = 5, int sp_count = 5)
    {
        int delay = 0;
        Card_Base card;

        int loop = Mathf.Max(fp_count, sp_count);

        for (int i = 0; i < loop; i++)
        {
            if (fp_count > 0)
            {
                card = Get_DECKSHF_Card()[0];

                Move_SingleCard(Game_Loc.Deck, Game_Loc.MyHand, card, true, delay * 0.35f);

                fp_count--;
                delay++;
            }
            if (sp_count > 0)
            {
                card = Get_DECKSHF_Card()[0];

                Move_SingleCard(Game_Loc.Deck, Game_Loc.OpHand, card, true, delay * 0.35f);

                sp_count--;
                delay++;
            }
        }
    }


}