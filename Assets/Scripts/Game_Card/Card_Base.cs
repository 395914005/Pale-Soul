using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class Card_Base : MonoBehaviour
{
    public Card_Mark m_Mark;

    public Card_Point m_Point;
    public Game_Loc m_Location;
    public Zone_Seq m_Seq;
    public Card_Pos m_AccuratePosition = Card_Pos.FaceDownAttack;
    public Card_Pos m_GivedPosition = Card_Pos.FaceDownAttack;
    public Renderer m_Face_Renderer;
    public Renderer m_Cover_Renderer;

    public Vector3 m_Accurate_Trm;
    public Vector3 m_Gived_Trm;

    public Vector3 m_Accurate_Rota;
    public Vector3 m_Gived_Rota;

    /// <summary> 停下并删除所有这张卡的dotween操作</summary> 
    public void Stop_All_Move()
    {
        this.transform.DOKill(false);
    }

    public void LocalMove_To_Gived(float delay = 0f)
    {
        m_Accurate_Trm = m_Gived_Trm;
        m_Accurate_Rota = m_Gived_Rota;

        this.transform.DOLocalMove(m_Gived_Trm, 0.5f)
            .SetDelay(delay);

        this.transform.DOLocalRotate(m_Gived_Rota, 0.5f)
            .SetDelay(delay);
    }

    public void Rotate_By_Position(float delay = 0f)
    {
        //Vector3 m_Gived_Rota;
        m_AccuratePosition = m_GivedPosition;
        switch (m_GivedPosition)
        {
            case Card_Pos.FaceUpAttack:
                m_Gived_Rota = new Vector3(90, 0, 0);
                break;
            case Card_Pos.FaceDownAttack:
                m_Gived_Rota = new Vector3(-90, 0, 180);
                break;
            case Card_Pos.Attack:
                break;
            case Card_Pos.FaceUpDefence:
                m_Gived_Rota = new Vector3(90, 0, 90);
                break;
            case Card_Pos.FaceUp:
                break;
            case Card_Pos.FaceDownDefence:
                m_Gived_Rota = new Vector3(-90, 0, 90);
                break;
            case Card_Pos.FaceDown:
                break;
            case Card_Pos.Defence:
                break;
            case Card_Pos.COUNT:
                break;
            default:
                break;
        }

        this.transform.DOLocalRotate(m_Gived_Rota, 0.5f)
            .SetDelay(delay);

    }

    public void RefreshCard()
    {
        Get_Pics();
        Refresh_Zone_Parent();
        Rotate_By_Position();
    }

    private void Get_Pics()
    {
        //string pic_name = m_Point.ToString();


        //m_Face_Renderer.material.mainTexture = Card_Pic_Helper.Get_T2D_By_String(pic_name);

        int point = (int)m_Point;

        if (m_Point == Card_Point._JOKER1)
            m_Face_Renderer.material.mainTexture = Singleton<Card_Pics>.Instance.m_Joker1;
        else if (m_Point == Card_Point._JOKER2)
            m_Face_Renderer.material.mainTexture = Singleton<Card_Pics>.Instance.m_Joker2;
        else
        {
            int mark = (int)m_Mark;
            int index = point * 4 + mark;

            m_Face_Renderer.material.mainTexture = Singleton<Card_Pics>.Instance.m_Pic_List[index];
        }
    }

    private void Refresh_Zone_Parent()
    {
        Transform parent;
        Zone_Mgr mgr = Singleton<Zone_Mgr>.Instance;
        switch (m_Location)
        {
            case Game_Loc.Deck:
                parent = mgr.m_DeckZone.transform;
                break;
            case Game_Loc.MyHand:
                parent = mgr.m_MyHandZone.transform;
                break;
            case Game_Loc.OpHand:
                parent = mgr.m_OpHandZone.transform;
                break;
            case Game_Loc.Neutral:
                parent = mgr.m_NeutralZone.transform;
                break;
            case Game_Loc.MydianshuZone:
                parent = mgr.m_MydianshuZone.transform;
                break;
            case Game_Loc.MylingkeZone:
                parent = mgr.m_MylingkeZone.transform;
                break;
            case Game_Loc.OpdianshuZone:
                parent = mgr.m_OpdianshuZone.transform;
                break;
            case Game_Loc.OplingkeZone:
                parent = mgr.m_OplingkeZone.transform;
                break;
            case Game_Loc.dianshuGrave:
                parent = mgr.m_dianshuGraveZone.transform;
                break;
            case Game_Loc.lingkeGrave:
                parent = mgr.m_lingkeGraveZone.transform;
                break;
            default:
                parent = mgr.m_UnKnowZone.transform;
                break;
        }
        this.transform.SetParent(parent);
    }
}