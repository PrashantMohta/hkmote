using System;
using System.Reflection;
using GlobalEnums;
using Modding;
using UnityEngine;

namespace Hkmote
{
    
    public class Hkmote : Mod
    {

        internal static Hkmote Instance;


        public DateTime lastAnimTime = DateTime.Now;
        public bool animating = false;
        public override string GetVersion()
        {
            return "1.0";
        }

        public override void Initialize()
        {
            Instance = this;

            ModHooks.Instance.HeroUpdateHook += update;
        }

        public void stopAnim() {
            var HeroAnimationController = HeroController.instance.gameObject.GetComponent<HeroAnimationController>();
            HeroAnimationController.StartControl();
            this.animating = false;
        }
        public void OnAnimationComplete(tk2dSpriteAnimator sprite, tk2dSpriteAnimationClip clip) {
            stopAnim();
        }
        public void playAnim(string clip) {
            var spriteAnimator = HeroController.instance.gameObject.GetComponent<tk2dSpriteAnimator>();
            //spriteAnimator.AnimationCompleted = OnAnimationComplete;
            spriteAnimator.PlayFromFrame(clip, 0);
            var HeroAnimationController = HeroController.instance.gameObject.GetComponent<HeroAnimationController>();
            HeroAnimationController.StopControl();
            this.lastAnimTime = DateTime.Now;
            this.animating = true;
        }
        public void forceAnim() {
            //Focus Get Once
            //Map Idle
            //Quake Antic
            //NA Cyclone Start
            var currentTime = DateTime.Now;
            if (this.animating && (currentTime - this.lastAnimTime).TotalMilliseconds > 2000) {
                stopAnim();
            }
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                stopAnim();
            }
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                stopAnim();
                playAnim("DN Charge");
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                stopAnim();
                playAnim("SD Charge Ground");
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                stopAnim();
                playAnim("ToProne");
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                stopAnim();
                playAnim("Challenge Start");
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                stopAnim();
                playAnim("Surface InToIdle");
            }
            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                stopAnim();
                playAnim("Exit");
            }
            if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                stopAnim();
                playAnim("Death");
            }
            if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                stopAnim();
                playAnim("Acid Death");
            }
            if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                stopAnim();
                playAnim("Roar Lock");
            }
            
        }

        public void update()
        {
            forceAnim();
        }

    }

}
