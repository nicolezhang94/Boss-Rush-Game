namespace BossRush.Common
{
    public class Attack
    {
        public DamageType DamageType { get; set; }

        public float Damage { get; set; }

        public float UseTime { get; set; }

        public Timer CooldownTimer { get; set; }
    }
}