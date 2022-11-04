using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

using SFML.Audio;
using SFML.Graphics;
using SFML.Window;
using SFML.System;


namespace MisjaNaMarsa
{
    public class MarsGame : GameLoop
    {

        Dictionary <string, int> stats = new Dictionary<string, int>();

        public Font font;

        uint width, height;
        string windowTitle, consoleFont;
        int startingStatsValue;


        RectangleShape[] shapes = new RectangleShape[3];
        RectangleShape background = new RectangleShape();
        Text[] statTexts = new Text[3];
        Text[] statLabels = new Text[3];

        QuestionManager qm = new QuestionManager();

        List<Button> buttons = new List<Button>();

        Vector2i mousePosWindow = new Vector2i();

        public bool added = false;

        public MarsGame()
        {
        }
        private void WindowClosed(object? sender, EventArgs e)
        {
            Window.Close();
        }

        public override void Draw(GameTime gameTime)
        {
            this.Window.Draw(background);
            DebugUtility.DrawPerformenceData(this, Color.White);
            qm.DrawQuestionsData(this, Color.White);
            //DrawErrors();
            DrawStats();

 
        }

        public void DrawStats()
        {
            foreach (Text text in statTexts)
            {
                this.Window.Draw(text);
            }
        }
        public void DrawErrors()
        {
            foreach (Button btn in buttons)
            {
                btn.Draw(this);
            }
        }

        public override void Initialize()
        {
            
            using (StreamReader sr = new StreamReader("./config.txt"))
            {
                string line = sr.ReadLine();
                width = Convert.ToUInt16(sr.ReadLine());
                line = sr.ReadLine();
                height = Convert.ToUInt16(sr.ReadLine());
                line = sr.ReadLine();
                windowTitle = sr.ReadLine();
                line = sr.ReadLine();
                consoleFont = "./fonts/" + sr.ReadLine();
                line = sr.ReadLine();
                qm.timeBetweenQuestions = Int16.Parse(sr.ReadLine());
                line = sr.ReadLine();
                startingStatsValue = Int16.Parse(sr.ReadLine());
            }
            Window = new RenderWindow(new VideoMode(width, height), windowTitle, Styles.Fullscreen);
            Window.Closed += WindowClosed;
            background.Size = new Vector2f(Convert.ToInt16(width), Convert.ToInt16(height));
            background.Texture = new Texture("./Dashboard.png", new IntRect(0, 0, 1920, 1080));

            this.stats["sprawnosc"] = startingStatsValue;
            this.stats["morale"] = startingStatsValue;
            this.stats["zapasy"] = startingStatsValue;

            this.font = new Font(consoleFont);


            statTexts[0] = new Text();
            statTexts[1] = new Text();
            statTexts[2] = new Text();



            foreach(Text text in statTexts)
            {
                text.Font = new Font(consoleFont);
                text.Color = new Color(134, 222, 242);
                text.CharacterSize = 62;
            }


        }
        

        public override void LoadContent()
        {
            qm.LoadQuestions();
            DebugUtility.LoadFont();
            qm.LoadFont();
            
        }
        public void UpdateButtons(GameTime gameTime)
        {
            if (Math.Floor(gameTime.TotalTimeElapsed) % qm.timeBetweenQuestions == 0 && Math.Floor(gameTime.TotalTimeElapsed) > 0 && added == false && qm.activeQuestions.Count > buttons.Count)
            {
                buttons.Add(new Button(200 + 550 * buttons.Count,750, 500, 305, consoleFont, "question " + (buttons.Count + 1).ToString(), new Color(134, 222, 242), Color.White, Color.White));
                added = true;
            }
            if (Math.Floor(gameTime.TotalTimeElapsed) % qm.timeBetweenQuestions == 1)
            {
                added = false;
            }
            foreach (Button btn in buttons)
            {
                btn.Update(mousePosWindow);
                if(btn.IsPressed())
                {
                    stats["sprawnosc"] --;
                    stats["morale"] --;
                    stats["zapasy"] --;
                }
            }
        }

        public void UpdateBars()
        {
            statTexts[0].DisplayedString = stats["sprawnosc"].ToString();
            statTexts[1].DisplayedString = stats["morale"].ToString();
            statTexts[2].DisplayedString = stats["zapasy"].ToString();

            statTexts[0].Position = new Vector2f(389-statTexts[0].GetGlobalBounds().Width/2, 520-statTexts[0].GetGlobalBounds().Height / 2);
            statTexts[1].Position = new Vector2f(955-statTexts[1].GetGlobalBounds().Width/2, 520-statTexts[1].GetGlobalBounds().Height / 2);
            statTexts[2].Position = new Vector2f(1521-statTexts[2].GetGlobalBounds().Width/2, 520-statTexts[2].GetGlobalBounds().Height / 2);
        }
        void CheckAnswer(Question q, string answer, string[] answers)
        {
            if (q.type == "zamk")
            {

            }
            else
            {
                for (int i = 0; i < q.answers.Count; i++)
                {
                    if (q.answers.ContainsKey(answer))
                    {
                        stats[q.answers[answer].Item1] += q.answers[answer].Item2;
                    }
                }
            }
        }
        public override void Update(GameTime gameTime)
        {
            this.mousePosWindow = Mouse.GetPosition(this.Window);
            qm.Update(gameTime);
            UpdateButtons(gameTime);
            UpdateBars();

            
        }
    }
}
