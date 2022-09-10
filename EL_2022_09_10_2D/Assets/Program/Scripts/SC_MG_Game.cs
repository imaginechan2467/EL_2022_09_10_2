using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class SC_MG_Game : MonoBehaviour
{
    private async void Update()
    {
        //開始前アップデート
        await BeforStartUpdate();

        //ゲームアップデート
        await GameUpdate();

        //リザルト前
        await PreResultUpdate();
    }

    async UniTask BeforStartUpdate()
    {


    }
    async UniTask GameUpdate()
    {
        
    }
    
    async UniTask PreResultUpdate()
    {
    
    }
}
