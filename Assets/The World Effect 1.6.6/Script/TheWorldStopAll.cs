using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Video;

public class TheWorldStopAll : MonoBehaviour
{
  [Header("是否自動判別擁有的元件")]
  public bool AutoDistinguish;
  [Header("暫停範圍是否包含子物件擁有的元件")]
  public bool StopAllChildren;
  [Header("欲暫停的類別")]
  public bool ToStopRigidbody, ToStopAnimator, ToStopParticleSystem, ToStopAudioSource, ToStopNavMeshAgent, ToStopVideoPlayer;

  bool StopTime;
  bool Stop;

  Vector3 OriginalRigidbodyVelocity;//暫停Rigidbody前先記錄"速度" 方便暫停結束後恢復"速度"
  Vector3 OriginalRigidbodyAngularVelocity;//暫停Rigidbody前先記錄"旋轉速度" 方便暫停結束後恢復"旋轉速度"

  void Start()
  {
    if (!GameObject.Find("The World Effect").GetComponent<TheWorldStopOtherGameObjectsScript>().DynamicScriptMode)
    {
      Debug.Log($"當前不是DynamicScriptMode狀態 刪除{name}的TheWorldStopAll腳本元件");
      Destroy(GetComponent<TheWorldStopAll>());
    }

    //給所有子物件此腳本
    if(StopAllChildren)
    {
      for (int i = 0; i < transform.childCount; i++)
      {
        if (transform.GetChild(i).gameObject.GetComponent<TheWorldStopAll>() == null)
        {
          transform.GetChild(i).gameObject.AddComponent<TheWorldStopAll>().AutoDistinguish = true;//子彈之類物體生成時 掛載這行程式碼 即成為可被暫停物件
          if (StopAllChildren)
          {
            transform.GetChild(i).gameObject.GetComponent<TheWorldStopAll>().StopAllChildren = true;
          }
          else
          {
            transform.GetChild(i).gameObject.GetComponent<TheWorldStopAll>().StopAllChildren = false;
          }
          Debug.Log($"當前是DynamicScriptMode狀態 給予{transform.GetChild(i).gameObject.name}TheWorldStopAll腳本元件");
        }
      }
    }
  }

  void Update()
  {
    StopTime = GameObject.Find("The World Effect").GetComponent<TheWorldScript>().StopTime;

    if (AutoDistinguish)
    {
      if (GetComponent<Rigidbody>() == true)
      {
        ToStopRigidbody = true;
      }
      else
      {
        ToStopRigidbody = false;
      }

      if (GetComponent<Animator>() == true)
      {
        ToStopAnimator = true;
      }
      else
      {
        ToStopAnimator = false;
      }

      if (GetComponent<ParticleSystem>() == true)
      {
        ToStopParticleSystem = true;
      }
      else
      {
        ToStopParticleSystem = false;
      }

      if (GetComponent<AudioSource>() == true)
      {
        ToStopAudioSource = true;
      }
      else
      {
        ToStopAudioSource = false;
      }

      if (GetComponent<NavMeshAgent>() == true)
      {
        ToStopNavMeshAgent = true;
      }
      else
      {
        ToStopNavMeshAgent = false;
      }

      if (GetComponent<VideoPlayer>() == true)
      {
        ToStopVideoPlayer = true;
      }
      else
      {
        ToStopVideoPlayer = false;
      }

    }

    if (StopTime)//時間暫停時
    {
      if (!Stop)
      {
        Stop = true;//每次暫停時只執行一次

        if (GetComponent<Rigidbody>() != null && ToStopRigidbody)
        {
          OriginalRigidbodyVelocity = GetComponent<Rigidbody>().velocity;
          OriginalRigidbodyAngularVelocity = GetComponent<Rigidbody>().angularVelocity;

          GetComponent<Rigidbody>().isKinematic = true;
        }

        if (GetComponent<Animator>() != null && ToStopAnimator)
        {
          GetComponent<Animator>().speed = 0;
        }

        if (GetComponent<ParticleSystem>() != null && ToStopParticleSystem)
        {
          GetComponent<ParticleSystem>().Pause();
        }

        if (GetComponent<AudioSource>() != null && ToStopAudioSource)
        {
          GetComponent<AudioSource>().Pause();
        }

        if (GetComponent<NavMeshAgent>() != null && ToStopNavMeshAgent)
        {
          GetComponent<NavMeshAgent>().enabled = false;
        }

        if (GetComponent<VideoPlayer>() != null && ToStopVideoPlayer)
        {
          GetComponent<VideoPlayer>().Pause();
        }

      }
    }
    else//時間暫停結束
    {
      if (Stop)
      {
        Stop = false;//每次暫停結束時只執行一次

        if (GetComponent<Rigidbody>() != null && ToStopRigidbody)
        {
          GetComponent<Rigidbody>().isKinematic = false;

          GetComponent<Rigidbody>().velocity = OriginalRigidbodyVelocity;
          GetComponent<Rigidbody>().angularVelocity = OriginalRigidbodyAngularVelocity;
        }

        if (GetComponent<Animator>() != null && ToStopAnimator)
        {
          GetComponent<Animator>().speed = 1;
        }

        if (GetComponent<ParticleSystem>() != null && ToStopParticleSystem)
        {
          GetComponent<ParticleSystem>().Play();
        }

        if (GetComponent<AudioSource>() != null && ToStopAudioSource)
        {
          GetComponent<AudioSource>().Play();
        }

        if (GetComponent<NavMeshAgent>() != null && ToStopNavMeshAgent)
        {
          GetComponent<NavMeshAgent>().enabled = true;
        }

        if (GetComponent<VideoPlayer>() != null && ToStopVideoPlayer)
        {
          GetComponent<VideoPlayer>().Play();
        }

      }
    }
  }
}
