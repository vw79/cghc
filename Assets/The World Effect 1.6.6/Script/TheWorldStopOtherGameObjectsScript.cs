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

  #region 以下是要並用的(DynamicScriptMode == false)&&(StopRigidbody/StopAnimator/StopParticleSystem/StopAudioSource/NavMeshAgent/VideoPlayer 掛載欲暫停的物體)

  [Header("欲暫停的Rig元件")]
  [Header("DynamicScriptMode == false 使用以下")]
  [SerializeField]
  Rigidbody[] StopRigidbody;//在Inspector設定大小 並拖曳有Rigidbody的物件到這裡
  Vector3[] OriginalRigidbodyVelocity;//暫停Rigidbody前先記錄"速度" 方便暫停結束後恢復"速度"
  Vector3[] OriginalRigidbodyAngularVelocity;//暫停Rigidbody前先記錄"旋轉速度" 方便暫停結束後恢復"旋轉速度"
  [Header("欲暫停的Anim元件")]
  [SerializeField]
  Animator[] StopAnimator;//在Inspector設定大小 並拖曳有Animator的物件到這裡
  [Header("欲暫停的PS元件")]
  [SerializeField]
  ParticleSystem[] StopParticleSystem;//在Inspector設定大小 並拖曳有ParticleSystem的物件到這裡
  [Header("欲暫停的AS元件")]
  [SerializeField]
  AudioSource[] StopAudioSource;//在Inspector設定大小 並拖曳有AudioSource的物件到這裡
  [Header("欲暫停的NMA元件")]
  [SerializeField]
  NavMeshAgent[] StopNavMeshAgent;//在Inspector設定大小 並拖曳有NavMeshAgent的物件到這裡
  [Header("欲暫停的VP元件")]
  [SerializeField]
  VideoPlayer[] StopVideoPlayer;//在Inspector設定大小 並拖曳有VideoPlayer的物件到這裡
  #endregion

  bool Stop;

  // Start is called before the first frame update
  void Start()
  {

    if (DynamicScriptMode)
    {
      //動態設定時 自動給StopAll的gameobject TheWorldStopAll腳本元件
      for (int i = 0; i < StopAll.Length; i++)
      {
        if (StopAll[i] != null && StopAll[i].gameObject.GetComponent<TheWorldStopAll>() == null)
        {
          StopAll[i].gameObject.AddComponent<TheWorldStopAll>().AutoDistinguish = true;//子彈之類物體生成時 掛載這行程式碼 即成為可被暫停物件
          if(StopAllChildren)
          {
            StopAll[i].gameObject.GetComponent<TheWorldStopAll>().StopAllChildren = true;
          }
          else
          {
            StopAll[i].gameObject.GetComponent<TheWorldStopAll>().StopAllChildren = false;
          }
          Debug.Log($"當前是DynamicScriptMode狀態 給予{ StopAll[i].gameObject.name}TheWorldStopAll腳本元件");
        }
        else if (StopAll[i] == null)
        {
          Debug.Log($"StopAll[{i}] is not exist.");
        }
      }
    }
    else
    {
      //非動態設定時 清空StopAll
      Debug.Log("當前不是DynamicScriptMode狀態 清空StopAll的陣列");
      StopAll = new GameObject[0];
    }
  }

  // Update is called once per frame
  void Update()
  {
    if (DynamicScriptMode)
    {

    }
    else if (!DynamicScriptMode)
    {
      if (GetComponent<TheWorldScript>().StopTime)//時間暫停時
      {
        if (!Stop)
        {
          Stop = true;//每次暫停時只執行一次

          for (int i = 0; i < StopRigidbody.Length; i++)
          {
            if (StopRigidbody[i] != null)
            {
              OriginalRigidbodyVelocity = new Vector3[StopRigidbody.Length];//設定需要被記錄的"速度"陣列大小
              OriginalRigidbodyAngularVelocity = new Vector3[StopRigidbody.Length];//設定需要被記錄的"旋轉速度"陣列大小

              OriginalRigidbodyVelocity[i] = StopRigidbody[i].velocity;
              OriginalRigidbodyAngularVelocity[i] = StopRigidbody[i].angularVelocity;

              StopRigidbody[i].isKinematic = true;
            }
            else if (StopRigidbody[i] == null)
            {
              Debug.Log($"StopRigidbody[{i}] is not exist.");
            }
          }

          for (int i = 0; i < StopAnimator.Length; i++)
          {
            if (StopAnimator[i] != null)
            {
              StopAnimator[i].speed = 0;
            }
            else if (StopAnimator[i] == null)
            {
              Debug.Log($"StopAnimator[{i}] is not exist.");
            }
          }

          for (int i = 0; i < StopParticleSystem.Length; i++)
          {
            if (StopParticleSystem[i] != null)
            {
              StopParticleSystem[i].Pause();
            }
            else if (StopParticleSystem[i] == null)
            {
              Debug.Log($"StopParticleSystem[{i}] is not exist.");
            }
          }

          for (int i = 0; i < StopAudioSource.Length; i++)
          {
            if (StopAudioSource[i] != null)
            {
              StopAudioSource[i].Pause();
            }
            else if (StopAudioSource[i] == null)
            {
              Debug.Log($"StopAudioSource[{i}] is not exist.");
            }
          }

          for (int i = 0; i < StopNavMeshAgent.Length; i++)
          {
            if (StopNavMeshAgent[i] != null)
            {
              StopNavMeshAgent[i].enabled = false;
            }
            else if (StopNavMeshAgent[i] == null)
            {
              Debug.Log($"StopNavMeshAgent[{i}] is not exist.");
            }
          }

          for (int i = 0; i < StopVideoPlayer.Length; i++)
          {
            if (StopVideoPlayer[i] != null)
            {
              StopVideoPlayer[i].Pause();
            }
            else if (StopVideoPlayer[i] == null)
            {
              Debug.Log($"StopVideoPlayer[{i}] is not exist.");
            }
          }

        }
      }
      else//時間暫停結束
      {
        if (Stop)
        {
          Stop = false;//每次暫停結束時只執行一次

          for (int i = 0; i < StopRigidbody.Length; i++)
          {
            if (StopRigidbody[i] != null)
            {
              StopRigidbody[i].isKinematic = false;

              StopRigidbody[i].velocity = OriginalRigidbodyVelocity[i];
              StopRigidbody[i].angularVelocity = OriginalRigidbodyAngularVelocity[i];
            }
            else if (StopRigidbody[i] == null)
            {
              Debug.Log($"StopRigidbody[{i}] is not exist.");
            }
          }

          for (int i = 0; i < StopAnimator.Length; i++)
          {
            if (StopAnimator[i] != null)
            {
              StopAnimator[i].speed = 1;
            }
            else if (StopAnimator[i] == null)
            {
              Debug.Log($"StopAnimator[{i}] is not exist.");
            }
          }

          for (int i = 0; i < StopParticleSystem.Length; i++)
          {
            if (StopParticleSystem[i] != null)
            {
              StopParticleSystem[i].Play();
            }
            else if (StopParticleSystem[i] == null)
            {
              Debug.Log($"StopParticleSystem[{i}] is not exist.");
            }
          }

          for (int i = 0; i < StopAudioSource.Length; i++)
          {
            if (StopAudioSource[i] != null)
            {
              StopAudioSource[i].Play();
            }
            else if (StopAudioSource[i] == null)
            {
              Debug.Log($"StopAudioSource[{i}] is not exist.");
            }
          }

          for (int i = 0; i < StopNavMeshAgent.Length; i++)
          {
            if (StopNavMeshAgent[i] != null)
            {
              StopNavMeshAgent[i].enabled = true;
            }
            else if (StopNavMeshAgent[i] == null)
            {
              Debug.Log($"StopNavMeshAgent[{i}] is not exist.");
            }
          }
          
          for (int i = 0; i < StopVideoPlayer.Length; i++)
          {
            if (StopVideoPlayer[i] != null)
            {
              StopVideoPlayer[i].Play();
            }
            else if (StopVideoPlayer[i] == null)
            {
              Debug.Log($"StopVideoPlayer[{i}] is not exist.");
            }
          }

        }
      }
    }
  }
}
