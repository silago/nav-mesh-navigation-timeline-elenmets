using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace NavMeshTimelineElements {
    [TrackClipType(typeof(NavMeshControlAsset))]
    [TrackBindingType(typeof(Transform))]
    public class NavMeshTrack : TrackAsset
    {
        public override Playable CreateTrackMixer(UnityEngine.Playables.PlayableGraph graph, GameObject go, int inputCount) {

            foreach(var clip in GetClips()) {
                    var asset = clip.asset as NavMeshControlAsset;
                    asset.clipDuration =    clip.duration;
            }
            return base.CreateTrackMixer    (graph,go,  inputCount);
        }
    }
}

        
