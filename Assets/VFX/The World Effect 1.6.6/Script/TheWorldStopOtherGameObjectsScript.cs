using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Video;
using UnityEditor;

#region 繼承設定

#endregion

public class TheWorldStopOtherGameObjectsScript : MonoBehaviour
{
  [Header("設定是否使用動態設定 遊戲中不要更改此設定")]
  public bool DynamicScriptMode = false;//在開始遊戲前就設定好


  #region 以下是要並用的(DynamicScriptMode == true)&&(StopAll掛載欲暫停的物體)

  [Header("暫停範圍是否包含子物件擁有的元件")]
  [Header("DynamicScriptMode == true 使用以下")]
  public bool StopAllChildren;

  [Header("欲暫停的Rig/Anim/PS/AS/NMA元件(系統會自動判斷以下物件的Rig/Anim/PS/AS/NMA/VP元件)")]
  [SerializeField]
  GameObject[] StopAll;//在Inspector設定大小 並拖曳物件到這裡 系統會自動判斷該物件的Rigidbody/Animator/ParticleSystem/AudioSource/NavMeshAgent/VideoPlayer
  #endregion


  bool Stop;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
