using UnityEngine;

namespace SaladChef.Core
{
    public class BasketUI : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer[] _sprites = new SpriteRenderer[2];

        private void OnEnable()
        {
            foreach (var sprite in _sprites)
                sprite.GetComponent<SpriteRenderer>().enabled = false;
        }

        public void OnAdd(Vegetable veggie, int count)
        {
            var sprite = _sprites[count - 1];
            sprite.enabled = true;
            sprite.sprite = veggie.GetComponent<SpriteRenderer>().sprite;
        }

        public void OnRemove(Vegetable veggie, int count)
        {
            if (!_sprites[1].enabled)
                _sprites[0].enabled = false;
            else
            {
                _sprites[0].sprite = _sprites[1].sprite;
                _sprites[1].enabled = false;
            }
        }
    }
}