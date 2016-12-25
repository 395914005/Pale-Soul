/*********************************************************************************
 *Copyright(C) 2016 by Youki
 *All rights reserved.
 *Author:       Youki
 *Version:      1.0
 *UnityVersion：5.3.3f1
 *Date:         2016-09-27
 *Description:  
**********************************************************************************/
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Debug_Flag : MonoBehaviour
{
    public const bool debug_Flag = true;

    private void Awake()
    {
        My_Debug._DebugEnable = debug_Flag;
    }
}
