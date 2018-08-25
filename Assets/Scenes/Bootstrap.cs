using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;
using System;
using System.Diagnostics;
using System.Threading;

public class Bootstrap : MonoBehaviour
{
    private static Stopwatch updater = new Stopwatch();
    private static int ttl = 2048;
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private void Start()
    {
        UnityEngine.Debug.Log("Starting..");
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        var entityManager = World.Active.GetOrCreateManager<EntityManager>();

        var Archetype1 = entityManager.CreateArchetype(typeof(Comp1));
        var Archetype2 = entityManager.CreateArchetype(typeof(Comp1), typeof(Comp2));
        var Archetype3 = entityManager.CreateArchetype(typeof(Comp1), typeof(Comp2), typeof(Comp3));

        for (int i = 0; i < 1000000; i++)
        {
            entityManager.CreateEntity(Archetype1);
            entityManager.CreateEntity(Archetype2);
            entityManager.CreateEntity(Archetype3);
        }

        stopwatch.Stop();
        UnityEngine.Debug.Log("Entity creation: "+stopwatch.Elapsed.ToString());

        updater.Start();
    }

    private void Update()
    {
        UnityEngine.Debug.Log("Update loop: "+updater.Elapsed.ToString());
        updater.Restart();
        ttl--;
        if(ttl <= 0)
        {
            Application.Quit();
        }
    }

}