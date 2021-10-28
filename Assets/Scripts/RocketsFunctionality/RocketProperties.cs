namespace Planetarity.RocketsFunctionality
{
    // Theoretical range of parameter values [1, 5]
    [System.Serializable]
    public enum RocketProperties
    {
        Weight,
        Acceleration,
        Cooldown,
        Damage,
        FuelCapacity
    }

    [System.Serializable]
    public struct RocketPropertyEntry
    {
        public RocketProperties property;
        public float value;
    }
}
