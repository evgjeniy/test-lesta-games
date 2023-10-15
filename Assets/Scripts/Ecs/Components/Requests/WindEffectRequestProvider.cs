using UnityEngine;

namespace Ecs.Components.Requests
{
    public class WindEffectRequestProvider : Voody.UniLeo.MonoProvider<WindEffectRequest> {}

    [System.Serializable]
    public struct WindEffectRequest
    {
        public Vector3 windDirection;
        public float windSpeedValue;
    }
}