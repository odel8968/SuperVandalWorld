using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sprite_Decorator
{
    public abstract class A_Sprite
    {
        public Sprite[] _sprites;
        public Animator _animator;

        public Sprite[] getSprites()
        {
            return _sprites;
        }
        public Animator getAnimation()
        {
            return _animator;
        }
    }

    public class Basic_Sprite : A_Sprite
    {
        public Basic_Sprite()
        {
            _sprites[0] = Resources.Load<Sprite>("Sprites/VandalJoeDefault");
            _sprites[1] = Resources.Load<Sprite>("Sprites/VandalJoeJumping");
            _animator = Resources.Load<Animator>("Sprites/VandalJoeMove");
        }   
    }

    //the following classes are not ready yet; sprites not finished

    /* public class axe_Sprite : A_Sprite //player has Axe Powerup
     * {
     * *   public axe_Sprite()
     *     {
     *         _sprites[0] = Resources.Load<Sprite>("Sprites/VandalJoeDefaultAxe");
     *         _sprites[1] = Resources.Load<Sprite>("Sprites/VandalJoeJumpingAxe");
     *         _animator = Resources.Load<Animator>("Sprites/VandalJoeMoveAxe");
     *     }
     * }
     * 
     * public class jump_Sprite : A_Sprite //player has Jump Powerup
     * {
     *     public jump_Sprite()
     *     {
     *         _sprites[0] = Resources.Load<Sprite>("Sprites/VandalJoeDefaultMultiJump");
     *         _sprites[1] = Resources.Load<Sprite>("Sprites/VandalJoeJumpingMultiJump");
     *         _animator = Resources.Load<Animator>("Sprites/VandalJoeMoveMultiJump");
     *     }
     * }
     */

}
