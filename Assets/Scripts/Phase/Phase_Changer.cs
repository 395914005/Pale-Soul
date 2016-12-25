using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;
public class Phase_Changer : MonoBehaviour
{
    [SerializeField]
    private Image m_Phase_Img;
    [SerializeField]
    private Image m_Phase_Light_Img;

    [SerializeField]
    private Vector3 PHASE_STANDBY_eulerAngles;
    [SerializeField]
    private Vector3 PHASE_DRAW_eulerAngles;
    [SerializeField]
    private Vector3 PHASE_MAIN_eulerAngles;
    [SerializeField]
    private Vector3 PHASE_DISCARD_eulerAngles;
    [SerializeField]
    private Vector3 PHASE_END_eulerAngles;

    void Start()
    {
        m_Phase_Img.transform.localEulerAngles = PHASE_STANDBY_eulerAngles;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            Go_Next_Phase();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Hide_Phase();
        }
    }

    public void Go_Next_Phase()
    {
        Image phase = m_Phase_Img;

        if (Duel_Core._instance.m_Current_Phase != Phase.COUNT)     //  确保不溢出
            Duel_Core._instance.m_Current_Phase = Duel_Core._instance.m_Current_Phase + 1;

        if (Duel_Core._instance.m_Current_Phase == Phase.COUNT)
        {
            Duel_Core._instance.m_Current_Phase = Phase.PHASE_STANDBY;
            //Change_Player(phase);
            //return;
        }

        print(Duel_Core._instance.m_Current_Phase);

        Change_To_Next(phase);
    }

    private void Change_To_Next(Image img)
    {
        Vector3 rotation = Vector3.zero;
        switch (Duel_Core._instance.m_Current_Phase)
        {
            case Phase.PHASE_STANDBY:
                rotation = PHASE_STANDBY_eulerAngles;
                break;
            case Phase.PHASE_DRAW:
                rotation = PHASE_DRAW_eulerAngles;
                break;
            case Phase.PHASE_MAIN:
                rotation = PHASE_MAIN_eulerAngles;
                break;
            case Phase.PHASE_DISCARD:
                rotation = PHASE_DISCARD_eulerAngles;
                break;
            case Phase.PHASE_END:
                rotation = PHASE_END_eulerAngles;
                break;
            case Phase.COUNT:
                break;
            default:
                break;
        }
        img.transform.DOLocalRotate(rotation, 1f);
    }

    private void Hide_Phase()
    {
        m_Phase_Img.GetComponent<Graphic>().CrossFadeAlpha(0, 0.8f, false);
        m_Phase_Light_Img.GetComponent<Graphic>().CrossFadeAlpha(0, 0.8f, false);
    }

    private void Change_Player(Image img)
    {

    }

}