using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;
public class Game_Mgr : MonoBehaviour
{
    public Duel_Core m_Core;
    public GameObject m_Card_Prefab;
    public GameObject m_Card_Parent;

    public static int aaaa;

    void Start()
    {
        m_Core = new Duel_Core();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            m_Core.Init_Game();

        }


        if (Input.GetKeyDown(KeyCode.D))
        {
            Singleton<DECK_ZONE>.Instance.Refresh_Deck_With_Shake();
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            m_Core.Init_Neutral(4);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            m_Core.Init_Player_Card();
        }
    }


    public static GameObject Create_Go(GameObject prefab, Vector3 position = default(Vector3), Vector3 rotation = default(Vector3), bool transition = true, GameObject parent = null, bool Params_Is_Local = true)
    {
        Vector3 init_Scale = prefab.transform.localScale;
        GameObject go = Instantiate<GameObject>(prefab);
        if (position != new Vector3())
        {
            if (Params_Is_Local) { go.transform.localPosition = position; }
            else { go.transform.position = position; }
        }
        else { go.transform.position = Vector3.zero; }

        if (rotation != new Vector3())
        {
            if (Params_Is_Local)
            { go.transform.localEulerAngles = rotation; }
            else
            { go.transform.eulerAngles = rotation; }
        }
        else { go.transform.eulerAngles = Vector3.zero; }

        go.transform.localScale = init_Scale;

        if (parent != null)
        {
            go.transform.SetParent(parent.transform, false);
            go.layer = parent.layer;
        }
        else { go.layer = 0; }

        foreach (Transform rect in go.GetComponentsInChildren<Transform>())
        {
            rect.gameObject.layer = go.layer;
        }

        ///由于是 gameobject 不是UI  所以不要scale
        //if (transition)
        //{
        //    go.transform.DOScale(Vector3.zero, m_transition_time).From().SetEase(Ease.OutBack);
        //}
        return go;
    }

}