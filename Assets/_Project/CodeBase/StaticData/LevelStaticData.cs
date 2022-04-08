using UnityEngine;

namespace _Project.CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "LandStaticData", menuName = "Static Data/Land Static Data", order = 0)]
    public class LevelStaticData : ScriptableObject
    {
        public int AmountOfPlatforms;
        
        public float Width;
        public float Length;
    }
}