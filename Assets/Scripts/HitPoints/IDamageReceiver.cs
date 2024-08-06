    using UnityEngine.Events;

    public interface IDamageReceiver
    {
        public UnityAction<float> OnApplyDamage { get; set; }
        public void ApplyDamage(float damageValue);
    }
