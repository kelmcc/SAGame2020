using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

[Serializable]
public struct EnemyComponentRuntime : IComponentData
{
   public float3 LocalPosition;
   public float Offset;
}
