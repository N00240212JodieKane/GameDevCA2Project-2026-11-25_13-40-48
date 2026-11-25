using UnityEngine;

public class Character
{
    public string name;
    public int exp = 0;


    //example of overloading 
    //Constructor 1
    public Character()
    {
        name = "not assigned";
    }

    //Constructor2
    public Character(string name)
    {
        this.name = name;
    }

    //Constructor3
    public Character(string name, int exp)
    {
        this.name = name;
        this.exp = exp;
    }


    //Polymorphism -> child class can override this
    public virtual void PrintStatsInfo()
    {
        Debug.LogFormat("Hero: {0} - {1} EXP", name, exp);
    }

    /*private void Reset()
    {
        this.name = "Not assigned";
        this.exp = 0;
    }*/


}