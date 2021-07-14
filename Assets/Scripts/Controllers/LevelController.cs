using UnityEngine;

public class LevelController : MonoBehaviour {
  public Weapon[] weapons;
  public FloatConst grenadeAmmo;

  private void Start() {
    Debug.Log("grenadeAmmo: " + grenadeAmmo.Value);

    foreach (Weapon weapon in weapons) {
      Debug.Log(weapon.weaponName + " is a " + weapon.type + " weapon");
    }
  }
}