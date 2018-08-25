using Unity.Entities;
using UnityEngine;
using Unity.Transforms;
using System.Runtime.CompilerServices;

public class Sys1 : ComponentSystem
{
    public struct CompGroup
    {
        public readonly ComponentDataArray<Comp1> comp1;
        public readonly int Length;
    }

    [Inject] CompGroup compGroup;

    protected override void OnUpdate()
    {
        var c = 0;
        for (int i = 0; i < compGroup.Length; i++)
        {
            //c++;
            c += compGroup.comp1[i].data;
        }
        if (c != 0)
        {
            Debug.Log("ERROR1");
            return;
        }
    }
}


public class Sys2 : ComponentSystem
{
    public struct CompGroup
    {
        public readonly ComponentDataArray<Comp1> comp1;
        public readonly ComponentDataArray<Comp2> comp2;
        public readonly int Length;
    }

    [Inject] CompGroup compGroup;
    protected override void OnUpdate()
    {
        var c = 0;
        for (int i = 0; i < compGroup.Length; i++)
        {
            c += compGroup.comp1[i].data;
        }
        if (c != 0)
        {
            Debug.Log("ERROR1");
            return;
        }
    }
}

public class Sys3 : ComponentSystem
{
    public struct CompGroup
    {
        public readonly ComponentDataArray<Comp2> comp2;
        public readonly ComponentDataArray<Comp3> comp3;
        public readonly int Length;
    }

    [Inject] CompGroup compGroup;
    protected override void OnUpdate()
    {
        var c = 0;
        for (int i = 0; i < compGroup.Length; i++)
        {
            c += compGroup.comp2[i].data;
        }
        if (c != 0)
        {
            Debug.Log("ERROR1");
            return;
        }
    }
}