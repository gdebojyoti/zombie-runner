using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
  public FloatReference maxHp;
  public FloatReference hp;
  public GameObject pivotGo;

  void Update() {
    pivotGo.transform.localScale = new Vector2(Mathf.Clamp(hp.value / maxHp.value, 0, 1), 1);
  }
}
