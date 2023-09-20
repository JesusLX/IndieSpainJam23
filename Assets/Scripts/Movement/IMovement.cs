using isj23.Characters;
using isj23.ST;

namespace isj23.Movement {
    internal interface IMovement {
        void Init(ICharacter player);
        void UpdateCanMove(bool can);
        void UpdateStats(Stats stats);
        void TryMove();
    } 
}