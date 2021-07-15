using UnityEngine;

[CreateAssetMenu(fileName = "Weapon Name", menuName = "Scriptable Objects/Weapon")]
public class Weapon: ScriptableObject {
  
  [Header("Meta data")][Space(5)]
  public string weaponName;
  public Sprite icon;

  [Header("Characteristics")][Space(5)]
  public WeaponType type;
  public AmmoType ammoType;

  [Header("Stats")][Space(5)]
  public float range;
  public float damagePerShot; // damage per shot
  public float rpm; // rounds per minute
  [Range(0.0f, 1.0f)] public float chance;

  public void Fire () {
    Debug.Log(weaponName + " is firing!");
  }
}