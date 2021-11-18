# GirafficsEngine
An easy way for beginners like me to learn with graphics in C#

To make your first window, create a class that inherits from the Giraffic class, and then create a constructor for your class that inherits from
the Giraffics class constructor. You will decide the parameters of your window that way. Example:

class Screen : Giraffic {
        public Screen() : base(new Vector2(512, 512), "Coolest Giraffic Ever!") { }
    }
    
Next of course make an object of your "Screen" class and you have a working window.

There are several methods you can override which will be called when something happens in your window. A couple of these:

class Screen : Giraffic {
        public Screen() : base(new Vector2(512, 512), "Coolest Giraffic Ever!") { }

        public override void OnStart()
        {
            
        }

        public override void OnDraw()
        {
            
        }

        public override void OnUpdate(float dT)
        {
            
        }

        public override void OnStop()
        {
            
        }
    }
    
