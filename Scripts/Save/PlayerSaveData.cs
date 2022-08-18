using System;

[Serializable]
public class PlayerSaveData : Save
{
    public string[] Inventory = new string[0];

    public string LastPoint = "";

    public string LastSide = "";

    public string Animator = "";

    public PlayerSaveData() { }
    public PlayerSaveData(string lastPoint) : this(lastPoint, "") { }
    public PlayerSaveData(string lastPoint, string animator) : this(lastPoint, animator, "") { }
    public PlayerSaveData(string lastPoint, string animator, string lastSide) : this(lastPoint, animator, lastSide, new string[0]) { }
    public PlayerSaveData(string lastPoint, string animator, string lastSide, string[] inventory)
    {
        LastPoint = lastPoint;
        Animator = animator;
        LastSide = lastSide;
        Inventory = inventory;
    }
}
