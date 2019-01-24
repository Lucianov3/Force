using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class LevelContent
{
    public int Object = 0;
    public int Rotation = 0;

    public void SetObjectAndRotation(int obj, int rot)
    {
        Object = obj;
        Rotation = rot;
    }
}