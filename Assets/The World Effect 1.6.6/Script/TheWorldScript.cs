using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheWorldScript : MonoBehaviour
{
  #region 顯示狀態
  [Header("顯示時間暫停狀態")]
  public bool StopTime;
  [Header("顯示剩餘暫停秒數")]
  public int time;
  #endregion

  #region 設定
  [Header("設定是否用無台詞音效(可在執行中動態調整)")]
  public bool WithoutSerifu;
  bool WithoutSerifuSetting;
  bool WithSerifuSetting;
  [Header("放入自定義的TheWorldResources(第一個放沒台詞的)(最大2個)")]
  [SerializeField]
  TheWorldResources[] TheWorldResource = new TheWorldResources[2];

  [Header("設定是否要啟用替身能量的ParticleSystem(可在執行中動態調整)")]
  public bool StandPowerParticleSystem;

  int MaxStopTime;
  int EndSoundEffectPlayTime;
  AudioClip[] TheWorldSoundEffects = new AudioClip[2];
  #endregion

  void Start()
  {
    
  }

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Z))
    {
      TheWorld();
    }
    //顯示關閉Sound
    if (WithoutSerifu)
    {
      if (!WithoutSerifuSetting)
      {
        WithoutSerifuSetting = true;
        WithSerifuSetting = false;

        MaxStopTime = TheWorldResource[0].MaxStopTime;
        EndSoundEffectPlayTime = TheWorldResource[0].EndSoundEffectPlayTime;
        TheWorldSoundEffects[0] = TheWorldResource[0].TheWorldSoundEffects[0];
        TheWorldSoundEffects[1] = TheWorldResource[0].TheWorldSoundEffects[1];
      }
    }
    else
    {
      if(!WithSerifuSetting)
      {
        WithoutSerifuSetting = false;
        WithSerifuSetting = true;

        MaxStopTime = TheWorldResource[1].MaxStopTime;
        EndSoundEffectPlayTime = TheWorldResource[1].EndSoundEffectPlayTime;
        TheWorldSoundEffects[0] = TheWorldResource[1].TheWorldSoundEffects[0];
        TheWorldSoundEffects[1] = TheWorldResource[1].TheWorldSoundEffects[1];
      }
    }
    //顯示關閉替身能量ParticleSystem
    if (StandPowerParticleSystem)
    {
      transform.GetChild(2).gameObject.SetActive(true);
    }
    else
    {
      transform.GetChild(2).gameObject.SetActive(false);
    }
  }

  void TheWorld()
  {
    if (!StopTime)
    {
      GetComponent<AudioSource>().PlayOneShot(TheWorldSoundEffects[0]);
      for (int i = 0; i < transform.GetChild(0).childCount; i++)
      {
        ParticleSystem PS;
        PS = transform.GetChild(0).GetChild(i).GetComponent<ParticleSystem>();
        PS.Play();
      }

      if (StandPowerParticleSystem)
      {
        for (int i = 0; i < transform.GetChild(2).childCount; i++)//stand power
        {
          ParticleSystem PS;
          PS = transform.GetChild(2).GetChild(i).GetComponent<ParticleSystem>();
          var main = PS.main;
          main.duration = MaxStopTime;
          main.startLifetime = MaxStopTime;

          PS.Play();
        }
      }

      StopTime = true;
      //Time.timeScale = 0;

      Debug.Log("ザ・ワールド！ 時よ止まれ！");

      time = MaxStopTime;
      StartCoroutine("StartStopTime");
    }
  }

  public IEnumerator StartStopTime()
  {
    while (time > 0)
    {
      time--;
      if (time <= 0)
      {
        StopTime = false;
        //Time.timeScale = 1;

        yield break;
      }
      else if (time > 0)
      {
        if (time == EndSoundEffectPlayTime)
        {
          TokiWaUgokidasu();
          Debug.Log("時は動き出す");
        }

        if (time == 1)
        {
          EndParticleSystemEffect();
        }
        yield return new WaitForSecondsRealtime(1);
      }
    }
  }

  void TokiWaUgokidasu()
  {
    GetComponent<AudioSource>().PlayOneShot(TheWorldSoundEffects[1]);
  }

  void EndParticleSystemEffect()
  {
    for (int i = 0; i < transform.GetChild(1).childCount; i++)
    {
      ParticleSystem PS;
      PS = transform.GetChild(1).GetChild(i).GetComponent<ParticleSystem>();
      PS.Play();
    }
  }
}
