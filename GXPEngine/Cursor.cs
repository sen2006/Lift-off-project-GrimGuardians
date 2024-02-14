using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine.Core;

namespace GXPEngine
{
    class Cursor : Sprite
    {
        Vector2 position;
        float speed = 0.1f;

        public Cursor(Vector2 position) : base("square.png")
        {
            this.position = position;
            scale = 1;
        }


        void Update()
        {

        }
    }
}