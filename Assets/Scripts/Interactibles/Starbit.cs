using TMPro;

namespace Interactibles
{
    public class Starbit : Collectible
    {
        private GameManager _gm;
        private TMP_Text _starbitCounter;

        void Start()
        {
            _gm = FindFirstObjectByType<GameManager>();
            _starbitCounter = _gm.starbitCounter;
            _starbitCounter.text = _gm.starbits.ToString();
        }
        public override void Collect()
        {
            _gm.starbits++;
            _starbitCounter.text = _gm.starbits.ToString();
            Destroy(this.gameObject.transform.parent.gameObject);
        }
    }
}