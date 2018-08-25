/*using Unity.Entities;
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
}*/


using Unity.Entities;
using UnityEngine;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
 
public class Sys1 : JobComponentSystem
{
    [BurstCompile]
    private struct Job : IJobProcessComponentData<Comp1>
    {
        public int C;
 
        public void Execute([ReadOnly] ref Comp1 data)
        {
            C += data.data;
        }
    }
 
    protected override JobHandle OnUpdate(JobHandle jobHandle)
    {
        return new Job().Schedule(this, jobHandle);
    }
}
 
 
public class Sys2 : JobComponentSystem
{
    [BurstCompile]
    private struct Job : IJobProcessComponentData<Comp1, Comp2>
    {
        public int C;
 
        public void Execute([ReadOnly] ref Comp1 data0, [ReadOnly] ref Comp2 data1)
        {
            C += data0.data;
        }
    }
 
    protected override JobHandle OnUpdate(JobHandle jobHandle)
    {
        return new Job().Schedule(this, jobHandle);
    }
}
 
public class Sys3 : JobComponentSystem
{
 
    [BurstCompile]
    private struct Job : IJobProcessComponentData<Comp2, Comp3>
    {
        public int C;
 
        public void Execute([ReadOnly] ref Comp2 data1, [ReadOnly] ref Comp3 data2)
        {
            C += data1.data;
        }
    }
 
    protected override JobHandle OnUpdate(JobHandle jobHandle)
    {
        return new Job().Schedule(this, jobHandle);
    }
}