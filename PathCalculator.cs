using UnityEngine;
using UnityEngine.AI;

public class PathCalculator {
    AnimationCurve curveX;
    AnimationCurve curveY;
    AnimationCurve curveZ;
    float totalDistance = 0f; 
    public PathCalculator(NavMeshPath path, float time = 30f) {
       for(var i=1; i<path.corners.Length; i++) {
           totalDistance+= Vector3.Distance(path.corners[i-1], path.corners[i]);
       }

       var speed = totalDistance / time;

       curveX = new AnimationCurve();
       curveY = new AnimationCurve();
       curveZ = new AnimationCurve();
       var d = 0f;
       var t = 0f;
       Vector3 val = path.corners[0];

       curveX.AddKey(t, val.x);
       curveY.AddKey(t, val.y);
       curveZ.AddKey(t, val.z);
       for(var i=1; i<path.corners.Length; i++) {
           val = path.corners[i];
           d+= Vector3.Distance(path.corners[i-1], path.corners[i]);
           t = d/speed;
           curveX.AddKey(t, val.x);
           curveY.AddKey(t, val.y);
           curveZ.AddKey(t, val.z);
       }
    }       

    public Vector3 Evaluate(float t) {
       var result =  new Vector3(
        curveX.Evaluate(t),
        curveY.Evaluate(t),
        curveZ.Evaluate(t)
               );
       return result;
    }
}
