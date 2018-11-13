using System;

using CoreGraphics;
using Foundation;
using SpriteKit;

#if __IOS__
using UIKit;
#else
using AppKit;
#endif

namespace SpriteKitGame
{
    public class GameScene : SKScene
    {
        //load the images from resources folder as nodes
        SKSpriteNode card1 = SKSpriteNode.FromImageNamed(NSBundle.MainBundle.PathForResource("card1", "png"));
        SKSpriteNode card2 = SKSpriteNode.FromImageNamed(NSBundle.MainBundle.PathForResource("card2", "png"));
        SKSpriteNode card3 = SKSpriteNode.FromImageNamed(NSBundle.MainBundle.PathForResource("card3", "png"));
        SKSpriteNode card4 = SKSpriteNode.FromImageNamed(NSBundle.MainBundle.PathForResource("card4", "png"));
        SKSpriteNode start = SKSpriteNode.FromImageNamed(NSBundle.MainBundle.PathForResource("start", "png"));
        SKSpriteNode instructions = SKSpriteNode.FromImageNamed(NSBundle.MainBundle.PathForResource("Instructions", "png"));

        //arrays that correspond with each card deck
        int[] arr1 = new int[40] { 0, 0, 150, 0, 300, 0, 200, 0, 250, 350, 0, 350, 0, 250, 200, 0, 300, 150, 0, 0, 0,
            300, 0, 350, 0, 200, 250, 150, 0, 0, 350, 200, 250, 0, 0, 0, 150, 300, 0, 0 };
        int[] arr2 = new int[40] { 0, 0, 0, 0, 0, 0, 0, 0, 1250, 0, 0, 0, 0, 1250, 0, 0, 0, 0, 0, 0, 1250, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 1250, 0, 0, 0, 0, 0, 0, 0, 0 };
        int[] arr3 = new int[40] { 0, 0, 50, 0, 50, 0, 50, 0, 50, 50, 0, 25, 75, 0, 0, 0, 25, 75, 0, 50, 0, 0, 0,
            50, 25, 50, 0, 0, 75, 50, 0, 0, 0, 25, 25, 0, 75, 0, 50, 75 };
        int[] arr4 = new int[40] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 250, 0, 0, 0, 0, 0, 0, 0, 0, 0, 250,
            0, 0, 0, 0, 0, 0, 0, 0, 250, 0, 0, 0, 0, 0, 250, 0, 0, 0, 0, 0 };

        int move = 0; //flag that detects which card deck is dragged from (card1 = 1, card2 = 2, ...)

        // same card image for dragging action
        SKSpriteNode card1move = SKSpriteNode.FromImageNamed(NSBundle.MainBundle.PathForResource("card1", "png"));
        SKSpriteNode card2move = SKSpriteNode.FromImageNamed(NSBundle.MainBundle.PathForResource("card2", "png"));
        SKSpriteNode card3move = SKSpriteNode.FromImageNamed(NSBundle.MainBundle.PathForResource("card3", "png"));
        SKSpriteNode card4move = SKSpriteNode.FromImageNamed(NSBundle.MainBundle.PathForResource("card4", "png"));

        SKNode[] removelist = new SKSpriteNode[1]; //list that stores the node to be removed from scene

        protected GameScene(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        /*
         * Sets up the start scene, which contains one "start" button
         */
        public override void DidMoveToView(SKView view)
        {
            //set the button size and position
            start.ScaleTo(new CGSize(width: 150, height: 150));
            start.Position = new CGPoint(Frame.Width / 2.3, Frame.Height / 1.3);
            AddChild(start); //add the node to the scene
        }

#if __IOS__

        /*
         * When the finger is pressed down
         */
        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            SKNode sprite; //used for detecting touched object
            base.TouchesEnded(touches, evt);

            // Called when a touch begins
            foreach (var touch in touches)
            {

                var location = ((UITouch)touch).LocationInNode(this);

                // logic for start button when it is touched
                if ((sprite = GetNodeAtPoint(location)).Equals(start))
                {
                    removelist[0] = start;
                    RemoveChildren(removelist); //remove the start button and set up the instruction

                    instructions.ScaleTo(new CGSize(width: Frame.Width / 2, height: Frame.Height / 2));
                    instructions.Position = new CGPoint(Frame.Width / 2, Frame.Height / 2);

                    AddChild(instructions); //set up the instruction scene


                }
                // logic for instruction when it is touched
                else if ((sprite = GetNodeAtPoint(location)).Equals(instructions))
                {
                    removelist[0] = instructions;
                    RemoveChildren(removelist); //remove the instruction and set up the experiment

                    //set up the card decks
                    card1.ScaleTo(new CGSize(width: 150, height: 150));
                    card1.Position = new CGPoint(Frame.Width / 2.3, Frame.Height / 1.3);
                    AddChild(card1);

                    card2.ScaleTo(new CGSize(width: 150, height: 150));
                    card2.Position = new CGPoint(Frame.Width / 1.7, Frame.Height / 1.3);
                    AddChild(card2);

                    card3.ScaleTo(new CGSize(width: 150, height: 150));
                    card3.Position = new CGPoint(Frame.Width / 2.3, Frame.Height / 2.2);
                    AddChild(card3);

                    card4.ScaleTo(new CGSize(width: 150, height: 150));
                    card4.Position = new CGPoint(Frame.Width / 1.7, Frame.Height / 2.2);
                    AddChild(card4);
                }
                else // logic for card decks when they are touched
                {
                    // card deck 1 (blue)
                    if ((sprite = GetNodeAtPoint(location)).Equals(card1))
                    {
                        removelist[0] = card1move;
                        RemoveChildren(removelist);
                        move = 1; //toggle the flag to 1 for TouhesMoved and Ended
                        card1move.ScaleTo(new CGSize(width: 150, height: 150));
                        card1move.Position = new CGPoint(Frame.Width / 2.3, Frame.Height / 1.3);
                        AddChild(card1move); //add the moving card 1 
                    }
                    // card deck 2 (green)
                    else if ((sprite = GetNodeAtPoint(location)).Equals(card2))
                    {
                        removelist[0] = card2move;
                        RemoveChildren(removelist);
                        move = 2;
                        card2move.ScaleTo(new CGSize(width: 150, height: 150));
                        card2move.Position = new CGPoint(Frame.Width / 1.7, Frame.Height / 1.3);
                        AddChild(card2move);
                    }
                    // card deck 3 (blue)
                    else if ((sprite = GetNodeAtPoint(location)).Equals(card3))
                    {
                        removelist[0] = card3move;
                        RemoveChildren(removelist);
                        move = 3;
                        card3move.ScaleTo(new CGSize(width: 150, height: 150));
                        card3move.Position = new CGPoint(Frame.Width / 2.3, Frame.Height / 2.2);
                        AddChild(card3move);
                    }
                    // card deck 4 (purple)
                    else if ((sprite = GetNodeAtPoint(location)).Equals(card4))
                    {
                        removelist[0] = card4move;
                        RemoveChildren(removelist);
                        move = 4;
                        card4move.ScaleTo(new CGSize(width: 150, height: 150));
                        card4move.Position = new CGPoint(Frame.Width / 1.7, Frame.Height / 2.2);
                        AddChild(card4move);
                    }
                    else
                    {
                    }
                }
            }
        }

        /*
         * When the finger is pressed down and moving
         * (gets called every time the finger is moving)
         */
        public override void TouchesMoved(NSSet touches, UIEvent evt)
        {
            base.TouchesMoved(touches, evt);
            var touched = (UITouch)touches.AnyObject;

            // logic for different moving card based on the move flag
            switch (move)
            {
                case 1:
                    //update the moving card location to the finger's position
                    card1move.Position = touched.LocationInNode(this);
                    break;
                case 2:
                    card2move.Position = touched.LocationInNode(this);
                    break;
                case 3:
                    card3move.Position = touched.LocationInNode(this);
                    break;
                case 4:
                    card4move.Position = touched.LocationInNode(this);
                    break;
                default:
                    break;
            }
        }

        /*
         * When the finger is released
         */
        public override void TouchesEnded(NSSet touches, UIEvent evt)
        {
            var moveDest = new CGPoint(); //the destination of the movement
            foreach (var touch in touches)
            {

                // logic for different moving card based on the move flag
                switch (move)
                {
                    case 1:
                        moveDest = new CGPoint(Frame.Width / 2.3, Frame.Height / 1.3);
                        card1move.RunAction(SKAction.MoveTo(moveDest, 0.3));
                        removelist[0] = card1move;
                        break;
                    case 2:
                        moveDest = new CGPoint(Frame.Width / 1.7, Frame.Height / 1.3);
                        card2move.RunAction(SKAction.MoveTo(moveDest, 0.3));
                        removelist[0] = card2move;
                        break;
                    case 3:
                        moveDest = new CGPoint(Frame.Width / 2.3, Frame.Height / 2.2);
                        card3move.RunAction(SKAction.MoveTo(moveDest, 0.3));
                        removelist[0] = card3move;
                        break;
                    case 4:
                        moveDest = new CGPoint(Frame.Width / 1.7, Frame.Height / 2.2);
                        card4move.RunAction(SKAction.MoveTo(moveDest, 0.3));
                        removelist[0] = card4move;
                        break;
                    default:
                        break;
                }
            }

            //if(move!=0){
            //    RemoveChildren(removelist);
            //}

            move = 0; //reset the move to 0 before the next time finger is pressed
        }
#else
        public override void MouseDown(NSEvent theEvent)
        {
            // Called when a mouse click occurs

            var location = theEvent.LocationInNode(this);

            var sprite = SKSpriteNode.FromImageNamed(NSBundle.MainBundle.PathForResource("Spaceship", "png"));

            sprite.Position = location;
            sprite.SetScale(0.5f);

            var action = SKAction.RotateByAngle(NMath.PI, 1.0);

            sprite.RunAction(SKAction.RepeatActionForever(action));

            AddChild(sprite);
        }
#endif

        public override void Update(double currentTime)
        {
            // Called before each frame is rendered
        }
    }
}

