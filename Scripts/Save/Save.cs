using System;

[Serializable]
public class Save
{
    public static implicit operator bool(Save save) => save != null;
}
