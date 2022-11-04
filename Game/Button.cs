using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace MisjaNaMarsa
{
    public enum buttonState
    {
        BTN_IDLE = 0,
        BTN_HOVER = 1,
        BTN_PRESSED = 2
    }
    public class Button

    {
        RectangleShape shape = new RectangleShape();
        Font font;
        Text text = new Text();

        Color idleColor;
        Color hoverColor;
        Color pressedColor;

       

        buttonState state;
        public Button(float x, float y, float width, float height, string fontPath, string text, Color idleColor, Color hoverColor, Color pressedColor)
        {

            this.state = buttonState.BTN_IDLE;

            this.shape.Position = new Vector2f(x, y);
            this.shape.Size = new Vector2f(width, height);
            this.shape.Texture = new Texture("./Error.png", new IntRect(0, 0, 1920, 1080));


            this.text.Font = new Font(fontPath);
            this.text.DisplayedString = text;
            this.text.Color = Color.White;
            this.text.CharacterSize = 22;
            this.text.Position = new Vector2f(x+this.shape.GetGlobalBounds().Width/2f - this.text.GetGlobalBounds().Width / 2f, y + this.shape.GetGlobalBounds().Height/2f  - this.text.GetGlobalBounds().Height);
           // this.text.Position = new Vector2f(x,y);

            this.idleColor = idleColor;
            this.hoverColor = hoverColor;
            this.pressedColor = pressedColor;

            this.shape.FillColor = this.idleColor;


        }
        public void Update(Vector2i mousePos)
        {
            this.state = buttonState.BTN_IDLE;

            if (this.shape.GetGlobalBounds().Contains(mousePos.X, mousePos.Y))
            {
                this.state = buttonState.BTN_HOVER;
                if (Mouse.IsButtonPressed(Mouse.Button.Left))
                {
                    this.state = buttonState.BTN_PRESSED;
                }
            }

            switch (this.state)
            {
                case buttonState.BTN_IDLE:
                    this.shape.FillColor = this.idleColor;
                    break;
                case buttonState.BTN_HOVER:
                    this.shape.FillColor = this.hoverColor;
                    break;
                case buttonState.BTN_PRESSED:
                    this.shape.FillColor = this.pressedColor;
                    break;
                default:
                    this.shape.FillColor = Color.Red;
                    break;
            }
        }
        public void Draw(GameLoop gameLoop)
        {
            gameLoop.Window.Draw(this.shape);
            gameLoop.Window.Draw(this.text);
        }
        public bool IsPressed()
            {
            if(this.state == buttonState.BTN_PRESSED)
                 return true;
                 return false;
            }


    }
}
