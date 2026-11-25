using UnityEngine;

public class Paladin : Character
{
    public Weapon weapon;

    //Constructor  1 of base class with name parameter
    public Paladin(string name, Weapon weapon) : base(name)
    {
        //example of Composition
        this.weapon = weapon;
    }

    //Constructor 2 of base class 
    public Paladin(string name) : base(name)
    {

    }

    //Polymorphism. This child class overrides its parent class method. 
    public override void PrintStatsInfo()
    {

        Debug.LogFormat("hail: {0} - take up your {1}!", name, weapon.name);
    }

}