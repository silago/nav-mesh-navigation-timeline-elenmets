using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace NavMeshTimelineElements {
    public class NavMeshControlAsset : PlayableAsset {
       public ExposedReference<Transform> start;
       public ExposedReference<Transform> end;
       public ExposedReference<Transform> lookTarget;
       public Vector3 offset = Vector3.zero; 
       public double clipDuration = 0f;

       public override Playable CreatePlayable (PlayableGraph graph, GameObject owner)
       {
           var playable = ScriptPlayable<NavMeshBehaviour>.Create(graph);
           var navMeshBehaviour = playable.GetBehaviour();
           var x = PlayableExtensions.GetDuration(playable);
           var a = start.Resolve(  graph.GetResolver());
           var b = end.Resolve(  graph.GetResolver());
           var l = lookTarget.Resolve(graph.GetResolver());
           navMeshBehaviour.Init(a, b,l, (float)clipDuration); 
           navMeshBehaviour.offset = offset;
           return playable;   
       }
    }
}

	
