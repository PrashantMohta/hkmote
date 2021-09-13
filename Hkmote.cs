using System;
using Modding;
using UnityEngine;

namespace Hkmote
{
    public class Hkmote : Mod
    {

        internal static Hkmote Instance;

        public DateTime lastAnimTime = DateTime.Now;
        public bool animating = false;

        public bool isModifier(){
            return (
                 Input.GetKey(KeyCode.LeftControl) || 
                 Input.GetKey(KeyCode.RightControl)|| 
                 Input.GetKey(KeyCode.RightAlt)|| 
                 Input.GetKey(KeyCode.LeftAlt)|| 
                 Input.GetKey(KeyCode.RightCommand)|| 
                 Input.GetKey(KeyCode.LeftCommand) 
                 ) ;
        }

        public KeyCode[] codes = {  
                                    KeyCode.Alpha0,
                                    KeyCode.Alpha1,
                                    KeyCode.Alpha2,
                                    KeyCode.Alpha3,
                                    KeyCode.Alpha4,
                                    KeyCode.Alpha5,
                                    KeyCode.Alpha6,
                                    KeyCode.Alpha7,
                                    KeyCode.Alpha8,
                                    KeyCode.Alpha9
                                };

        public string[] anims = {
            "Focus Get Once",
            "Collect Magical Fall",
            "Prostrate",//
            "Collect Magical 1",//
            "DN Charge",
            "SD Charge Ground",//
            "Map Idle",//
            "ToProne",//
            "Collect Heart Piece",//
            "Challenge Start",//
            "Collect SD 1",//
            "Surface InToIdle",//
            "Collect SD 2",//
            "Exit",//
            "Collect SD 3",//
            "Death",//
            "DN Slash Antic",//
            "Acid Death",//
            "Collect Shadow",//
            "Roar Lock",//
            "LookUp",
            "LookDown"
        };

        public override string GetVersion()
        {
            return "1.0";
        }

        public override void Initialize()
        {
            Instance = this;
            ModHooks.HeroUpdateHook += update;
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
            if(spriteAnimator != null){
                spriteAnimator.PlayFromFrame(clip, 0);
                var HeroAnimationController = HeroController.instance.gameObject.GetComponent<HeroAnimationController>();
                HeroAnimationController.StopControl();
                this.lastAnimTime = DateTime.Now;
                this.animating = true;
            }
        }
        public void forceAnim() {

            var currentTime = DateTime.Now;
            if (this.animating && (currentTime - this.lastAnimTime).TotalMilliseconds > 2000) {
                stopAnim();
            }

            for(var i=0;i < codes.Length ; i+=1){
                if(Input.GetKeyDown(codes[i])){
                    stopAnim();
                    if(!isModifier()){
                        playAnim(anims[i]);
                    } else {
                        playAnim(anims[i+10]);
                    }
                }
            }
            
        }

        public void update()
        {
            forceAnim();
        }

    }

}
