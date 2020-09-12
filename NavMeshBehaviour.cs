using System.Collections;
using Unity.EditorCoroutines.Editor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;

[System.Serializable]
public class NavMeshBehaviour : PlayableBehaviour {
   public Transform start;
   public Transform end ;//= new Vector3();
   public float duration;
   public Transform lookTarget = null;
   PathCalculator pathCalculator;
   NavMeshPath path;

   public Vector3 offset;

   public override void OnBehaviourPlay(Playable playable, FrameData info) {
       if (ready) {
          for (int i =1; i<path.corners.Length; i++) {
              Debug.DrawLine(path.corners[i-1], path.corners[i], Color.red, 5f);
          }
       }
   }


   bool ready = false;

   public void Init(Transform startT, Transform endT,Transform look, float time) {
      start = startT;
      end = endT;
      duration = time;
      lookTarget = look;

      path = new NavMeshPath();
      var result = NavMesh.CalculatePath(startT.position, endT.position,    NavMesh.AllAreas, path);  
      pathCalculator = new PathCalculator(path, time);
      ready = true;
   }

   public override void ProcessFrame(Playable playable, FrameData info, object playerData)
   {
       if (ready == false) {
        return;
       }
       Transform item = playerData as Transform;
       var time = (float)playable.GetTime(); 
       if (time == 0) {
          return;
       }

       Vector3 pos = Vector3.zero;
       if (pathCalculator != null ) {
           pos = pathCalculator.Evaluate(time);
           var prevPos = pathCalculator.Evaluate(time-info.deltaTime);
           var nextPos = pos;
           var dir =    nextPos - prevPos;
           var r = Quaternion.LookRotation(dir, Vector3.up);

           nextPos += r*offset;

           Debug.DrawLine(prevPos, nextPos); 
           if (lookTarget == null) {
             item.rotation = r;
           } else {
             Debug.DrawRay(item.transform.position, dir , Color.green, 5f);
             item.LookAt(lookTarget);
           }

           item.transform.position = nextPos;
       }
   }
}


