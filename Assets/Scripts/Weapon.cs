using UnityEngine;

public struct Weapon
{
    public string name;
    public int damage;

    //Constructor
    public Weapon(string name, int damage)
    {
        this.name = name;
        this.damage = damage;
    }

    public void PrintWeaponStats()
    {
        Debug.LogFormat("Weapon: {0} - {1} DMG", name, damage);
    }
}
