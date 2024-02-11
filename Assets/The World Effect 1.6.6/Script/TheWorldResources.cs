using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TheWorldResources", menuName = "Create TheWorldResources", order = 1)]
public class TheWorldResources : ScriptableObject
{
  public string Name;
  [Header("設定最長暫停時間")]
  public int MaxStopTime;
  [Header("設定播放結束音效的時間(時停結束前幾秒播放 不能=0)")]
  public int EndSoundEffectPlayTime;
  [Header("設定音效(最大2個)")]
  public AudioClip[] TheWorldSoundEffects = new AudioClip[2];
}